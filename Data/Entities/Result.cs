using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Result : AuditableEntity
    {
        public string Summary { get; set; } // e.g., Test result name
        public string Notes { get; set; } 
        public int ServiceId { get; set; } 
        public virtual Service Service { get; set; }
        public virtual ICollection<Sample> Samples { get; set; } = new List<Sample>();
    }
}
