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
            if ((string)Session["Power"] != "1") return Json("0");

            return Json(StudentsDal.List(), JsonRequestBehavior.AllowGet);
        }

        //public ContentResult SingleName(int id)
        //{
        //    return Content(StudentsDAL.SingleName(id));
        //}

        [HttpPost]
        public JsonResult Add(Students s)
        {
            if ((string) Session["Power"] != "1") return Json("0");
            StudentsDal.Add(s);
            return Json(StudentsDal.List());
        }
        [HttpPost]
        public JsonResult Edit(Students s)
        {
            if ((string)Session["Power"] != "1") return Json("0");

            StudentsDal.Edit(s,s.Id);
            return Json(StudentsDal.List());

        }

        [HttpPost]
        public JsonResult Delete(int id)
        {
            if ((string)Session["Power"] != "1") return Json("0");

            StudentsDal.Del(id);
            return Json(StudentsDal.List());
        }
    }
}