using AutoMapper;
using Data.Entities;
using Repositories.UnitOfWork;
using Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Data.Enum;

namespace Services.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AppointmentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Appointment?> GetAppointmentByIdAsync(int id)
        {
            return await _unitOfWork.Repository<Appointment>()
                                    .AsQueryable()
                                    .Include(a => a.Service)
                                    .Include(a => a.Address)
                                    .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<List<Appointment>> GetAppointmentsByUserAsync(string userId)
        {
            var appointmentRepo = _unitOfWork.Repository<Appointment>();
            return await appointmentRepo
                .Where(a => a.UserId == userId)
                .Include(a => a.Service)
                .Include(a => a.Address)
                .ToListAsync();
        }

        public async Task<bool> BookAppointmentAsync(Appointment appointment)
        {
            try
            {
                var appointmentRepo = _unitOfWork.Repository<Appointment>();

                // Kiểm tra trùng lịch (đã có lịch khác cùng thời gian)
                bool isConflict = await appointmentRepo
                    .AsQueryable()
                    .AnyAsync(a =>
                        a.ScheduledAt == appointment.ScheduledAt &&
                        a.Status != AppointmentStatus.Cancelled);

                if (isConflict)
                {
                    return false; // Trả về false để báo hiệu lỗi trùng lịch
                }

                // Lưu Appointment
                await appointmentRepo.CreateAsync(appointment);
                await _unitOfWork.CommitAsync();

                return true;
            }
            catch (Exception ex)
            {
                // TODO: log lỗi nếu cần
                return false;
            }
        }


        public async Task<bool> CancelAppointmentAsync(int appointmentId, string userId, string reason)
        {
            var appointmentRepo = _unitOfWork.Repository<Appointment>();
            var appointment = await appointmentRepo.GetByIdAsync(appointmentId);

            if (appointment == null || appointment.Status == AppointmentStatus.Cancelled)
                return false;

            appointment.Status = AppointmentStatus.Cancelled;
            appointment.CancellationReason = reason;
            appointment.CancelledByUserId = userId;

            await appointmentRepo.UpdateAsync(appointment);
            await _unitOfWork.CommitAsync();

            return true;
        }


        public async Task<List<Appointment>> GetAllAppointmentsAsync()
        {
            return await _unitOfWork.Repository<Appointment>()
                .AsQueryable()
                .Include(a => a.Service)
                .Include(a => a.Address)
                .Include(a => a.User)
                .OrderByDescending(a => a.ScheduledAt)
                .ToListAsync();
        }

        public async Task<bool> RescheduleAppointmentAsync(int appointmentId, DateTime newDate, string newTime)
        {
            var appointmentRepo = _unitOfWork.Repository<Appointment>();
            var appointment = await appointmentRepo
                .AsQueryable()
                .FirstOrDefaultAsync(a => a.Id == appointmentId);

            if (appointment == null || appointment.Status == AppointmentStatus.Cancelled)
                return false;

            DateTime newDateTime = newDate.Date.Add(TimeSpan.Parse(newTime));

            var conflictingAppointment = await appointmentRepo
                .AsQueryable()
                .Where(a => a.Id != appointmentId
                            && a.Status != AppointmentStatus.Cancelled
                            && a.ScheduledAt == newDateTime)
                .FirstOrDefaultAsync();

            if (conflictingAppointment != null)
                return false;

            appointment.ScheduledAt = newDateTime;
            appointment.Status = AppointmentStatus.Scheduled;

            await appointmentRepo.UpdateAsync(appointment);
            await _unitOfWork.CommitAsync();

            return true;
        }

        public async Task<bool> MarkAsCompletedAsync(int appointmentId)
        {
            var appointmentRepo = _unitOfWork.Repository<Appointment>();
            var appointment = await appointmentRepo.GetByIdAsync(appointmentId);

            if (appointment == null || appointment.Status != AppointmentStatus.Scheduled)
                return false;

            appointment.Status = AppointmentStatus.Completed;
            await appointmentRepo.UpdateAsync(appointment);
            await _unitOfWork.CommitAsync();
            return true;
        }


        public async Task<int> AutoMarkNoShowAppointmentsAsync()
        {
            var appointmentRepo = _unitOfWork.Repository<Appointment>();
            var now = DateTime.Now;
            var threshold = now.AddSeconds(-1);

            var appointments = await appointmentRepo
                .Where(a => a.Status == AppointmentStatus.Scheduled && a.ScheduledAt < threshold)
                .ToListAsync();

            foreach (var appointment in appointments)
            {
                appointment.Status = AppointmentStatus.NoShow;
                await appointmentRepo.UpdateAsync(appointment);
            }

            if (appointments.Any())
                await _unitOfWork.CommitAsync();

            return appointments.Count;
        }

    }
}
