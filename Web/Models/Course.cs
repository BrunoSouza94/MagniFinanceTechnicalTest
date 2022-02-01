using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Web.Models
{
    public class Course
    {
        [Key]
        [DataMember]
        public int Id { get; set; }

        [Required]
        public string CourseName { get; set; }

        [NotMapped]
        public decimal? AverageGrade { get; set; }

        [NotMapped]
        public List<Subject> CourseSubjects { get; set; }

        [NotMapped]
        public List<Student> CourseStudents { get; set; }
        
        [NotMapped]
        public List<Teacher> CourseTeachers { get; set; }
    }
}