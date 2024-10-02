using System.ComponentModel.DataAnnotations;

namespace AcademiaDHub.Models
{
    public class Section
    {
        [Key]
        public int SectionId { get; set; }
        public string? SectionName { get; set; }
    }
}
