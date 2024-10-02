using System.ComponentModel.DataAnnotations;

namespace AcademiaDHub.Models
{
    public class Hostel
    {
        [Key]
        public int HostelId { get; set; }
        public string? HostelName { get; set; }
        public string? HostelGender { get; set; }
    }
}
