using Data.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Address : BaseEntity
    {
        public string? UserId { get; set; }
        public virtual User User { get; set; }
        [Required]
        public string Street { get; set; }
        [Required]
        public string Ward { get; set; }
        [Required]
        public string District { get; set; }
        [Required]
        public string Province { get; set; }
        [Required]
        public string ZipCode { get; set; }
        public  LocationType LocationType { get; set; } 
        public string? OpeningHours { get; set; }
        public string? Notes { get; set; }
        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    }
    public class OpeningTimeSlot
    {
        public DayOfWeek Day { get; set; }
        public TimeSpan Start { get; set; }
        public TimeSpan End { get; set; }
    }
}
