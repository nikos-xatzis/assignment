namespace BackEnd_CVManagement.Core.DTOs.Degree
{
    public class DegreeGetDto
    {
        public long ID { get; set; }
        public DateTime CreationTime { get; set; }
        public bool IsAssociated { get; set; }
        public string DegreeName { get; set; }

    }
}
