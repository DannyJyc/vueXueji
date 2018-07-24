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
            if ((string)Session["Power"] != "1") return Json("0");

            return Json(CoursesDal.List(),JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Add(Courses co)
        {
            if ((string)Session["Power"] != "1") return Json("0");

            CoursesDal.Add(co);
            return Json(CoursesDal.List());
        }

        [HttpPost]
        public JsonResult Edit(Courses co)
        {
            if ((string)Session["Power"] != "1") return Json("0");

            CoursesDal.Edit(co);
            return Json(CoursesDal.List());
        }

        [HttpPost]
        public JsonResult Del(int id)
        {
            if ((string)Session["Power"] != "1") return Json("0");

            CoursesDal.Del(id);
            return Json(CoursesDal.List());
        }
    }
}