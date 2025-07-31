using Data.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Appointment : AuditableEntity
    {
        public DateTime ScheduledAt { get; set; }
        public string Description { get; set; }
        public string? CancellationReason { get; set; }
        public string? CancelledByUserId { get; set; }
        public AppointmentStatus Status { get; set; }
        public int AddressId { get; set; }
        public virtual Address Address { get; set; }
        public string UserId { get; set; }
        public virtual User User { get; set; }
        public int ServiceId { get; set; }
        public virtual Service Service { get; set; }
    }
}
