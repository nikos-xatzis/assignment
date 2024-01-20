using System.ComponentModel.DataAnnotations;

namespace BackEnd_CVManagement.Core.Entities
{
    public abstract class BaseEntity
    {
        [Key]
        public long ID { get; set; }
        public DateTime CreationTime { get; set; } = DateTime.Now;

    }
}
