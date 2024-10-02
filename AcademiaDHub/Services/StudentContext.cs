using AcademiaDHub.Models;
using Microsoft.EntityFrameworkCore;

namespace AcademiaDHub.Services
{
    public class StudentContext : DbContext

    {
        public StudentContext(DbContextOptions options):base (options) 
        {
            
        }
        public DbSet<Student> Students { get; set; }
        public DbSet<Hostel> Hostels { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<Section> Sections { get; set; }

    }
}
