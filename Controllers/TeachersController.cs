using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using vuexueji.DAL;

namespace vuexueji.Controllers
{
    public class TeachersController : Controller
    {
        // GET: Teachers
        public JsonResult Get()
        {
            return Json(TeachersDAL.List(), JsonRequestBehavior.AllowGet);
        }
    }
}