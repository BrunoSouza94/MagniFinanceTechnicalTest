using Newtonsoft.Json;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Web.Data;
using Web.Models;

namespace Web.Controllers
{
    public class CourseController : Controller
    {
        private readonly AppDbContext context = new AppDbContext();
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetCourses()
        {
            List<Course> courses = context.Courses
                                    .ToList();

            courses.ForEach((course) =>
            {
                decimal? sumGrades = 0.0m;
                decimal count = 0.0m;

                course.CourseSubjects = context.Subjects
                                            .Where(s => s.CourseId == course.Id)
                                            .ToList();

                course.CourseStudents = new List<Student>();
                course.CourseTeachers = new List<Teacher>();

                course.CourseSubjects.ForEach((subject) =>
                {
                    if (course.CourseTeachers.Find(t => t.Id.Equals(subject.TeacherId)) == null) course.CourseTeachers.Add(context.Teachers.FirstOrDefault(s => s.Id == subject.TeacherId));

                    var subjectStudents = context.SubjectStudents
                                            .Where(ss => ss.SubjectId == subject.Id)
                                            .ToList();
                    
                    subjectStudents.ForEach((subjectStudent) =>
                    {
                        if (course.CourseStudents.Find(s => s.Id.Equals(subjectStudent.StudentId)) == null)
                        {
                            course.CourseStudents.Add(context.Students.FirstOrDefault(s => s.Id == subjectStudent.StudentId));
                        }

                        if (subjectStudent.Grade != null)
                        {
                            sumGrades += subjectStudent.Grade;
                            count++;
                        }
                    });
                });

                if (count > 0) course.AverageGrade = sumGrades / count;
            });

            var rtrn = JsonConvert.SerializeObject(courses, Formatting.None, new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });

            return Json(data: rtrn, behavior: JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Insert(Course course)
        {
            context.Courses.Add(course);
            context.SaveChanges();

            return new HttpStatusCodeResult(200);
        }

        [HttpPut]
        public ActionResult Edit(Course course)
        {
            var entry = context.Entry(course);
            context.Set<Course>().Attach(course);
            entry.State = EntityState.Modified;

            context.SaveChanges();

            return new HttpStatusCodeResult(200);
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            Course course = context.Courses.FirstOrDefault(c => c.Id == id);

            context.Courses.Remove(course);
            context.SaveChanges();

            return new HttpStatusCodeResult(200);
        }
    }
}