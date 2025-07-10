using Data.Enum;
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
        public AnalysisType Type { get; set; } // e.g., Paternity, Ancestry, etc.
        public int ServiceId { get; set; }
        public string UserId { get; set; }
        public virtual User User { get; set; }
        public virtual Service Service { get; set; }
        public virtual ICollection<Sample> Samples { get; set; } = new List<Sample>();
    }
    
}
