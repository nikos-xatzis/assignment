using System.ComponentModel.DataAnnotations;

namespace BackEnd_CVManagement.Core.Entities
{
    public class Candidate : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string Mobile { get; set; }
        public string ResumeUrl { get; set; }
        //Relations
        public long DegreeId { get; set; }
        public Degree Degree { get; set; } 

    }
}
