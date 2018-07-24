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
            if ((string)Session["Power"] != "1") return Json("0");

            return Json(MajorsDal.List(), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Add(Majors m)
        {
            if ((string)Session["Power"] != "1") return Json("0");

            MajorsDal.Add(m);
            return Json(MajorsDal.List());
        }

        [HttpPost]
        public JsonResult Edit(Majors m)
        {
            if ((string)Session["Power"] != "1") return Json("0");

            MajorsDal.Edit(m);
            return Json(MajorsDal.List());
        }

        [HttpPost]
        public JsonResult Del(int id)
        {
            if ((string)Session["Power"] != "1") return Json("0");

            MajorsDal.Del(id);
            return Json(MajorsDal.List());
        }
    }
}