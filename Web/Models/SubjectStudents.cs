using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class SubjectStudents
    {
        [Key]
        public int Id { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; }
        public int SubjectId { get; set; }
        public Subject Subject { get; set; }
        public int? Grade { get; set; }
    }
}