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
            if ((string)Session["Power"] != "1") return Json("0");

            return Json(ClassesDal.List(), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Add(Classes c)
        {
            if ((string)Session["Power"] != "1") return Json("0");

            ClassesDal.Add(c);
            return Json(ClassesDal.List());
        }

        [HttpPost]
        public JsonResult Edit(Classes c)
        {
            if ((string)Session["Power"] != "1") return Json("0");

            ClassesDal.Edit(c);
            return Json(ClassesDal.List());
        }

        [HttpPost]
        public JsonResult Del(int id)
        {
            if ((string)Session["Power"] != "1") return Json("0");

            ClassesDal.Del(id);
            return Json(ClassesDal.List());
        }
    }
}