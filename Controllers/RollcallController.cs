using System;
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
            return Json(RollcallDal.LeList(1), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Addrollcall(int id)
        {
            Session["tempid"] = id;
            ViewBag.students = RollcallDal.StudentsList(id);
            if (RollcallDal.StudentsList(id) == null)
            {
                return Content(RollcallDal.ErroeWhenNull());
            }
            return View();
        }

        [HttpPost]
        public ActionResult Addrollcall(StudentState sr)
        {
            
            return Content(RollcallDal.StudentsStatus(sr, Convert.ToInt16(Session["tempid"]), Convert.ToString(Session["Name"])));
        }

        [HttpPost]
        public ActionResult Editrollcall(StudentState sr)
        {
            return Content(RollcallDal.EditStudentsStatus(sr));
        }

        public ActionResult ListRollcall()
        {
            ViewBag.rollcall = RollcallDal.List(Convert.ToInt16(Session["tempid"]));
            return View();
        }

        public ActionResult StudentsStatus(int id)
        {
            ViewBag.id = id;
            return View();
        }

        public ActionResult StudentsJson(int id)
        {
            return Content(RollcallDal.StringJson(id));
        }

        [HttpGet]
        public ActionResult AllList()
        {
            ViewBag.rollcall = RollcallDal.AllList();
            return View();
        }
    }
}