using System.Data.Entity;
using Web.Models;

namespace Web.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() : base("DefaultConnection")
        {

        }

        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Teacher> Teachers { get; set; }
        public virtual DbSet<Subject> Subjects { get; set; }
        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<SubjectStudents> SubjectStudents { get; set; }
    }
}