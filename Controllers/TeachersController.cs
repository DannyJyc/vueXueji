using System.Web.Mvc;
using vuexueji.DAL;

namespace vuexueji.Controllers
{
    public class TeachersController : Controller
    {
        // GET: Teachers

        /// <summary>
        /// 返回对应老师（Teachers）的所有信息
        /// </summary>
        /// <returns>返回json格式的老师（teachers）信息</returns>
        public JsonResult Get()
        {
            return Json(TeachersDal.List(), JsonRequestBehavior.AllowGet);
        }
    }
}