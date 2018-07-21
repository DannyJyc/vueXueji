using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using testxueji.Models;
using vuexueji.DAL;

namespace vuexueji.Controllers
{
    public class StudentsController : Controller
    {
        // GET: Students

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult Get()
        {
            return Json(StudentsDAL.List(), JsonRequestBehavior.AllowGet);
        }

        //public ContentResult SingleName(int id)
        //{
        //    return Content(StudentsDAL.SingleName(id));
        //}

        [HttpPost]
        public JsonResult Add(Students s)
        {
            StudentsDAL.Add(s);
            return Json(StudentsDAL.List());
        }
        [HttpPost]
        public JsonResult Edit(Students s)
        {
            StudentsDAL.Edit(s,s.Id);
            return Json(StudentsDAL.List());

        }

        [HttpPost]
        public JsonResult Delete(int id)
        {
            StudentsDAL.Del(id);
            return Json(StudentsDAL.List());
        }
    }
}