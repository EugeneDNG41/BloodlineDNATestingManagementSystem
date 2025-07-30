using Data.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Sample : AuditableEntity
    {
        public SampleCollectionMethod CollectionMethod { get; set; }
        public SampleType Type { get; set; }
        public SampleStatus Status { get; set; }       
        public DateTime CollectionDate { get; set; }
        public DateTime? ReceivedDate { get; set; }         
        public string Notes { get; set; }
        public string CollectorId { get; set; } //ID nhân viên thu thập
        public virtual User Collector { get; set; }
        public string DonorId { get; set; } //ID khách hàng hiến mẫu
        public virtual User Donor { get; set; }

        // Quan hệ với Result
        public int? ResultId { get; set; }
        public virtual Result Result { get; set; }
    }
    
}
