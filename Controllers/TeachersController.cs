using System.Web.Mvc;
using vuexueji.DAL;

namespace vuexueji.Controllers
{
    public class TeachersController : Controller
    {
        // GET: Teachers
        public JsonResult Get()
        {
            return Json(TeachersDal.List(), JsonRequestBehavior.AllowGet);
        }
    }
}