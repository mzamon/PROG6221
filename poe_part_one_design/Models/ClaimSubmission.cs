namespace poe_part_one_design.Models
{
    public class ClaimSubmission
    {
        public string LectureID { get; set; } = "";
        public int HoursWorked { get; set; }
        public decimal HourlyRate { get; set; }
        public string AdditionalNotes { get; set; } = "";
        public string SupportingDocuments { get; set; } = "";
    }
}
