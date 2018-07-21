using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using testxueji.Models;
using vuexueji.DAL;

namespace vuexueji.Controllers
{
    public class CoursesController : Controller
    {
        // GET: Courses
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Get()
        {
            return Json(CoursesDAL.List(),JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Add(Courses co)
        {
            CoursesDAL.Add(co);
            return Json(CoursesDAL.List());
        }

        [HttpPost]
        public JsonResult Edit(Courses co)
        {
            CoursesDAL.Edit(co);
            return Json(CoursesDAL.List());
        }

        [HttpPost]
        public JsonResult Del(int id)
        {
            CoursesDAL.Del(id);
            return Json(CoursesDAL.List());
        }
    }
}