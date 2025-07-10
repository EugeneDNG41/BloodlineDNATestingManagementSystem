using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Enum
{
    public enum SampleCollectionMethod
    {
        Appointment,
        HomeVisit,
        WalkIn,
        MailIn
    }
    public enum SampleStatus
    {
        Collected,
        Received,
        Processing,
        Completed,
        Rejected

    }
    public enum SampleType
    {
        Blood,
        Urine,
        Tissue,
        Saliva,
        Other
    }
}
