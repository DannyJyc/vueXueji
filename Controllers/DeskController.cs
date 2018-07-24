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
            return View();
        }

        public ActionResult Students()
        {
            ViewBag.gross = DeskDAL.GrossCount(3);
            ViewBag.check = DeskDAL.Check(3);
            return View();
        }

        public JsonResult StudentsCount()
        {
            return Json(DeskDAL.Count(3), JsonRequestBehavior.AllowGet);
        }

        public JsonResult StudentsScore()
        {
            return Json(DeskDAL.Score(3), JsonRequestBehavior.AllowGet);
        }
    }
}