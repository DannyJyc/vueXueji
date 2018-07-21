using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using testxueji.Models;
using vuexueji.DAL;

namespace vuexueji.Controllers
{
    public class ClasstimeController : Controller
    {
        // GET: Classtime
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Classtime()
        {
            return View();
        }

        public ActionResult AllClasstime()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Edit()
        {
            return View();
        }

        public ActionResult Add(CoursesArranging ca)
        {
            ClasstimeDAL.Add(ca);
            return Json(ClasstimeDAL.List());
        }

        [HttpPost]
        public ActionResult Edit(CoursesArranging ca)
        {
            ClasstimeDAL.Edit(ca);
            return Json(ClasstimeDAL.List());
        }

        public JsonResult Classroom()
        {
            return Json(ClasstimeDAL.Classrooms(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult WeekArranging()
        {
            return Json(ClasstimeDAL.WeekList(), JsonRequestBehavior.AllowGet);//写死班级ID=2
        }

        public ActionResult AddWeek(string DateStart,string DateEnd)
        {            
            return Content(ClasstimeDAL.AddWeekarranging(DateStart, DateEnd));
        }

        public JsonResult SingleJsonResult()
        {
            return Json(ClasstimeDAL.SingleCoursesArrangingNames(2), JsonRequestBehavior.AllowGet);
        }

        public JsonResult Get()
        {
            return Json(ClasstimeDAL.ClassesList(2), JsonRequestBehavior.AllowGet);//写死班级ID=2
        }

        public JsonResult GetAll()
        {
            return Json(ClasstimeDAL.List(), JsonRequestBehavior.AllowGet);
        }
    }
}