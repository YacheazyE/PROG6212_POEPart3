namespace CMCS_v3.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Claim
    {
        public System.Guid ClaimID { get; set; }
        public Nullable<System.DateTime> SubmittedDate { get; set; }
        public Nullable<int> HoursWorked { get; set; }
        public Nullable<decimal> HourlyRate { get; set; }
        public string ClaimDocumentPath { get; set; }
        public string Status { get; set; }

    }
}
