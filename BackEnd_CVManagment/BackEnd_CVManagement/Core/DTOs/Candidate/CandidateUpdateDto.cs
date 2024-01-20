namespace BackEnd_CVManagement.Core.DTOs.Candidate
{
    public class CandidateUpdateDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string Mobile { get; set; }
        public string ResumeUrl { get; set; }
        public long DegreeId { get; set; }

    }
}
