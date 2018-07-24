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

        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(CoursesArranging ca)
        {
            if ((string)Session["Power"] != "1") return Json("0");

            ClasstimeDal.Add(ca);
            return Json(ClasstimeDal.List());
        }

        [HttpPost]
        public ActionResult Edit(CoursesArranging ca)
        {
            if ((string)Session["Power"] != "1") return Json("0");

            ClasstimeDal.Edit(ca);
            return Json(ClasstimeDal.List());
        }

        public JsonResult Classroom()
        {
            return Json(ClasstimeDal.Classrooms(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult WeekArranging()
        {
            return Json(ClasstimeDal.WeekList(), JsonRequestBehavior.AllowGet);//写死班级ID=2
        }

        public ActionResult AddWeek(string dateStart,string dateEnd)
        {            
            return Content(ClasstimeDal.AddWeekarranging(dateStart, dateEnd));
        }

        public JsonResult SingleJsonResult()
        {
            return Json(ClasstimeDal.SingleCoursesArrangingNames(2), JsonRequestBehavior.AllowGet);
        }

        public JsonResult Get()
        {
            return Json(ClasstimeDal.ClassesList(2), JsonRequestBehavior.AllowGet);//写死班级ID=2
        }

        public JsonResult GetAll()
        {
            if ((string)Session["Power"] != "1") return Json("0");

            return Json(ClasstimeDal.List(), JsonRequestBehavior.AllowGet);
        }
    }
}