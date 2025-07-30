using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IAppointmentService
    {
        Task<Appointment?> GetAppointmentByIdAsync(int id);
        Task<List<Appointment>> GetAllAppointmentsAsync();
        Task<List<Appointment>> GetAppointmentsByUserAsync(string userId);
        Task<bool> BookAppointmentAsync(Appointment appointment, Address address);
        Task<bool> CancelAppointmentAsync(int appointmentId, string userId, string reason);
        Task<bool> RescheduleAppointmentAsync(int appointmentId, DateTime newDate, string newTime);
        Task<bool> MarkAsCompletedAsync(int appointmentId);
        Task<int> AutoMarkNoShowAppointmentsAsync();
    }
}