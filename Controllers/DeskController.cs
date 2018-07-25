using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using vuexueji.DAL;

namespace vuexueji.Controllers
{
    public class DeskController : Controller
    {
        // GET: Desk
        
        public ActionResult Teachers()
        {
            ViewBag.exams = DeskDAL.Exams(Convert.ToInt16(Session["Id"]));
            return View();
        }

        public ActionResult Students()
        {
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