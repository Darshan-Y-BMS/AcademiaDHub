using System.ComponentModel.DataAnnotations;

namespace AcademiaDHub.Models
{
    public class Class
    {
        [Key]
        public int ClassId { get; set; }
        public string? ClassName { get; set; }
    }
}
