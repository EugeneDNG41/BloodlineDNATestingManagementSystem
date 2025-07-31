using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class User : IdentityUser
    {
        public string FullName { get; set; }
        public DateTime DateOfBirth { get; set; }        
        public string? ProfilePictureUrl { get; set; }
        public string? Gender { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public virtual ICollection<Address> Addresses { get; set; } = new List<Address>();
        public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
        public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
        public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();
        public virtual ICollection<Post> Posts { get; set; } = new List<Post>();
        public virtual ICollection<Sample> SamplesCollected { get; set; } = new List<Sample>();
        public virtual ICollection<Sample> SamplesDonated { get; set; } = new List<Sample>();
        public virtual ICollection<Result> Results { get; set; } = new List<Result>();
    }
}
