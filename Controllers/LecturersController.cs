using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using vuexueji.DAL;

namespace vuexueji.Controllers
{
    public class LecturersController : Controller
    {
        // GET: Lecturers
        public JsonResult Get()
        {
            return Json(LecturersDAl.List(),JsonRequestBehavior.AllowGet);
        }
    }
}