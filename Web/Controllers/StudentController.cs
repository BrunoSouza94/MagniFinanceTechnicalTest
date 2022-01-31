using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Web.Data;
using Web.Models;

namespace Web.Controllers
{
    public class StudentController : Controller
    {
        private readonly AppDbContext context = new AppDbContext();

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetStudents()
        {
            List<Student> students = context.Students.ToList();
            return Json(students, JsonRequestBehavior.AllowGet);
        }

        

        [HttpPost]
        public ActionResult Insert(Student student)
        {
            context.Students.Add(student);
            context.SaveChanges();

            return new HttpStatusCodeResult(200);
        }
        
        [HttpPut]
        public ActionResult Edit(Student student)
        {
            var entry = context.Entry(student);
            context.Set<Student>().Attach(student);
            entry.State = EntityState.Modified;

            context.SaveChanges();

            return new HttpStatusCodeResult(200);
        }
        
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            Student student = context.Students.FirstOrDefault(s => s.Id == id);

            context.Students.Remove(student);
            context.SaveChanges();

            return new HttpStatusCodeResult(200);
        }

        [HttpPost]
        public ActionResult EnrollStudent(SubjectStudents subjectStudents)
        {
            context.SubjectStudents.Add(subjectStudents);
            context.SaveChanges();

            return new HttpStatusCodeResult(200);
        }
    }
}