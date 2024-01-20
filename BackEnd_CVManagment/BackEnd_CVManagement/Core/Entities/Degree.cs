namespace BackEnd_CVManagement.Core.Entities
{
    public class Degree : BaseEntity 
    {
        public string DegreeName { get; set; }

        public bool IsAssociated { get; set; } = false;

        //Relations 
        public ICollection<Candidate> Candidates { get; set; }

    }
}
