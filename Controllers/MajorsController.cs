using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using testxueji.Models;
using vuexueji.DAL;

namespace vuexueji.Controllers
{
    public class MajorsController : Controller
    {
        // GET: Majors
        public ViewResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult Get()
        {
            return Json(MajorsDAL.List(), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Add(Majors m)
        {
            MajorsDAL.Add(m);
            return Json(MajorsDAL.List());
        }

        [HttpPost]
        public JsonResult Edit(Majors m)
        {
            MajorsDAL.Edit(m);
            return Json(MajorsDAL.List());
        }

        [HttpPost]
        public JsonResult Del(int id)
        {
            MajorsDAL.Del(id);
            return Json(MajorsDAL.List());
        }
    }
}