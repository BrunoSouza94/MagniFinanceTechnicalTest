using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Web.Data;
using Web.Models;

namespace Web.Controllers
{
    public class SubjectController : Controller
    {
        private readonly AppDbContext context = new AppDbContext();
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetSubjects()
        {
            List<Subject> subjects = context.Subjects.ToList();

            subjects.ForEach((subject) =>
            {
                subject.Teacher = context.Teachers.FirstOrDefault(t => t.Id == subject.TeacherId);
                subject.Course = context.Courses.FirstOrDefault(c => c.Id == subject.CourseId);

                List<Student> students = new List<Student>();

                var subjectStudents = context.SubjectStudents.Where(ss => ss.SubjectId == subject.Id).ToList();
                subjectStudents.ForEach((subjectStudent) =>
                {
                    students.Add(context.Students.FirstOrDefault(s => s.Id == subjectStudent.Id));
                });

                subject.Students = students;
            });

            return Json(subjects, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Insert(Subject subject)
        {
            context.Subjects.Add(subject);
            context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpPut]
        public ActionResult Edit(Subject subject)
        {
            var entry = context.Entry(subject);
            context.Set<Subject>().Attach(subject);
            entry.State = EntityState.Modified;

            context.SaveChanges();

            return new HttpStatusCodeResult(200);
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            Subject subject = context.Subjects.FirstOrDefault(s => s.Id == id);

            context.Subjects.Remove(subject);
            context.SaveChanges();

            return new HttpStatusCodeResult(200);
        }
    }
}