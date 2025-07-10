using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Review : AuditableEntity
    {
        public int Rating { get; set; } // e.g., 1 to 5 stars
        public string Comment { get; set; }
        public string UserId { get; set; } // Foreign key to User
        public virtual User User { get; set; } // Navigation property to User
        public int ServiceId { get; set; } // Foreign key to Service
        public virtual Service Service { get; set; } // Navigation property to Service

    }
}
