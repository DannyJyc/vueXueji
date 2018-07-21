using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using testxueji.Models;
using vuexueji.DAL;

namespace vuexueji.Controllers
{
    public class ClassesController : Controller
    {
        // GET: Classes
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult Get()
        {
            return Json(ClassesDAL.List(), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Add(Classes c)
        {
            ClassesDAL.Add(c);
            return Json(ClassesDAL.List());
        }

        [HttpPost]
        public JsonResult Edit(Classes c)
        {
            ClassesDAL.Edit(c);
            return Json(ClassesDAL.List());
        }

        [HttpPost]
        public JsonResult Del(int id)
        {
            ClassesDAL.Del(id);
            return Json(ClassesDAL.List());
        }
    }
}