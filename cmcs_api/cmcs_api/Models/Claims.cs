using System.ComponentModel.DataAnnotations;

namespace cmcs_api.Models
{
    public class Claims
    {
        public int ClaimID { get; set; }
        public DateTime SubmittedDate { get; set; }

        //inserting range limit so users 
        [Range(0, 1, ErrorMessage = "Hourly rate must be a positive value within the Range of 0.0 - 1")]
        public double HourlyRate { get; set; }
        public string? SubmittedDocxPath { get; set; }
        public bool? Status { get; set; }

        public Claims()
        {
            Status = false; //default value
        }

    }
}
