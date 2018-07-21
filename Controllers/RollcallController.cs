using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using testxueji.Models;
using vuexueji.DAL;

namespace vuexueji.Controllers
{
    public class RollcallController : Controller
    {
        // GET: Rollcall

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult Get()
        {
            return Json(RollcallDAL.LeList(1), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Addrollcall(int id)
        {
            Session["tempid"] = id;
            ViewBag.students = RollcallDAL.StudentsList(id);
            if (RollcallDAL.StudentsList(id) == null)
            {
                return Content(RollcallDAL.erroeWhenNull());
            }
            return View();
        }

        [HttpPost]
        public ActionResult Addrollcall(StudentState sr)
        {
            
            return Content(RollcallDAL.StudentsStatus(sr, Convert.ToInt16(Session["tempid"])));
        }

        public ActionResult ListRollcall()
        {
            ViewBag.rollcall = RollcallDAL.List(Convert.ToInt16(Session["tempid"]));
            return View();
        }

        public ActionResult StudentsStatus(int id)
        {
            ViewBag.id = id;
            return View();
        }

        public ActionResult StudentsJson(int Id)
        {
            return Content(RollcallDAL.StringJson(Id));
        }
    }
}