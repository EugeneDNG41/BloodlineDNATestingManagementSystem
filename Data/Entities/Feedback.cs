using Data.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Feedback : AuditableEntity
    {
        public string Message { get; set; }
        public FeedbackType Type { get; set; }
        public FeedbackStatus Status { get; set; } 
        public string UserId
        { get; set; } // Foreign key to User
        public virtual User User { get; set; } // Navigation property to User

    }
}
