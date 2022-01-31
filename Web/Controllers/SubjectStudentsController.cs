using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Data;
using Web.Models;

namespace Web.Controllers
{
    public class SubjectStudentsController : Controller
    {
        private readonly AppDbContext context = new AppDbContext();

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetSubjectStudents()
        {
            List<SubjectStudents> enrollments = context.SubjectStudents.ToList();

            enrollments.ForEach((enrollment) =>
            {
                enrollment.Student = context.Students.FirstOrDefault(s => s.Id == enrollment.StudentId);
                enrollment.Subject = context.Subjects.FirstOrDefault(s => s.Id == enrollment.SubjectId);
            });

            enrollments.OrderBy(e => e.SubjectId);

            return Json(enrollments, JsonRequestBehavior.AllowGet);
        }

        [HttpPut]
        public ActionResult Edit(SubjectStudents gradeToApply)
        {
            var entry = context.Entry(gradeToApply);
            context.Set<SubjectStudents>().Attach(gradeToApply);
            entry.State = EntityState.Modified;

            context.SaveChanges();

            return new HttpStatusCodeResult(200);
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            SubjectStudents subjectStudents = context.SubjectStudents.FirstOrDefault(ss => ss.Id == id);

            context.SubjectStudents.Remove(subjectStudents);
            context.SaveChanges();

            return new HttpStatusCodeResult(200);
        }
    }
}