using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using testxueji.Models;
using vuexueji.DAL;

namespace vuexueji.Controllers
{
    public class ExamsController : Controller
    {
        // GET: Exams
        public ActionResult Index(int id)
        {
            ViewBag.id = id;
            ViewBag.students = RollcallDAL.StudentsList(id);
            return View();
        }

        public ActionResult Addexams(ExamsBox eb)
        {
            
            return Content(ExamsDAL.Add(eb));
        }

        public ActionResult List(int id)
        {
            ViewBag.list = ExamsDAL.List(id);
            return View();
        }

        public ActionResult Score(int id)
        {
            ViewBag.id = id;
            return View();
        }

        public ActionResult StudentsJson(int Id)
        {
            return Content(ExamsDAL.StringJson(Id));
        }
    }
}