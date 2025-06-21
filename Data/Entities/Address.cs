using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Address : BaseEntity
    {
        public string UserId { get; set; }
        public virtual User User { get; set; }
        public string Street { get; set; }
        public string Ward { get; set; }
        public string Disttrict { get; set; }
        public string Province { get; set; }
        public string City { get; set; }            
        public string ZipCode { get; set; }
        public  LocationType LocationType { get; set; } 
        public string? OpeningHours { get; set; }
        public string? Notes { get; set; }
        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    }
    public enum LocationType
    {
        Home,
        Lab,
        Clinic
    }
}
