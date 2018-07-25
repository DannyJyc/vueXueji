using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using testxueji.Models;
using vuexueji.DAL;

namespace vuexueji.Controllers
{
    public class DeskController : Controller
    {
        // GET: Desk
        
        public ActionResult Teachers()
        {
            var db = new XuejiContext();
            var item = Convert.ToInt16(Session["Id"]);
            var single = db.Lectureres.SingleOrDefault(l => l.Id ==item);
            ViewBag.Name = single.Name;
            ViewBag.exams = DeskDAL.Exams(Convert.ToInt16(Session["Id"]));
            return View();
        }

        public ActionResult Students()
        {
            var db = new XuejiContext();
            var item = Convert.ToInt16(Session["Id"]);
            var single = db.Studentses.SingleOrDefault(s => s.Id == item);
            ViewBag.Name = single.Name;
            ViewBag.gross = DeskDAL.GrossCount(Convert.ToInt16(Session["Id"]));
            ViewBag.check = DeskDAL.Check(Convert.ToInt16(Session["Id"]));
            return View();
        }

        public JsonResult StudentsCount()
        {
            return Json(DeskDAL.Count(Convert.ToInt16(Session["Id"])), JsonRequestBehavior.AllowGet);
        }

        public JsonResult StudentsScore()
        {
            return Json(DeskDAL.Score(Convert.ToInt16(Session["Id"])), JsonRequestBehavior.AllowGet);
        }

        public JsonResult LecturerScore()
        {
            return Json(DeskDAL.TeachersCount(Convert.ToInt16(Session["Id"])), JsonRequestBehavior.AllowGet);
        }
    }
}