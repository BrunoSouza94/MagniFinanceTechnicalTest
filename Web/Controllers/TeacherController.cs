using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Web.Data;
using Web.Models;

namespace Web.Controllers
{
    public class TeacherController : Controller
    {
        private readonly AppDbContext context = new AppDbContext();

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetTeachers()
        {
            List<Teacher> teachers = context.Teachers.ToList();

            return Json(teachers, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Insert(Teacher teacher)
        {
            context.Teachers.Add(teacher);
            context.SaveChanges();

            return new HttpStatusCodeResult(200);
        }

        [HttpPut]
        public ActionResult Edit(Teacher teacher)
        {
            var entry = context.Entry(teacher);
            context.Set<Teacher>().Attach(teacher);
            entry.State = EntityState.Modified;

            context.SaveChanges();

            return new HttpStatusCodeResult(200);
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            Teacher teacher = context.Teachers.FirstOrDefault(s => s.Id == id);

            context.Teachers.Remove(teacher);
            context.SaveChanges();

            return new HttpStatusCodeResult(200);
        }
    }
}