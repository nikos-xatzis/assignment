namespace BackEnd_CVManagement.Core.DTOs.Candidate
{
    public class CandidateGetDto
    {
        public long ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string Mobile { get; set; }
        public string ResumeUrl { get; set; }
        public DateTime CreationTime { get; set; }
        //Relations
        public long DegreeId { get; set; }
        public string DegreeName { get; set; }
    }
}
