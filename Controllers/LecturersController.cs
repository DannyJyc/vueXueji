using System.Web.Mvc;
using vuexueji.DAL;

namespace vuexueji.Controllers
{
    public class LecturersController : Controller
    {
        // GET: Lecturers

        /// <summary>
        /// 返回所有讲师的数据
        /// </summary>
        /// <returns>返回json格式讲师的数据供视图使用</returns>
        public JsonResult Get()
        {
            return Json(LecturersDAl.List(),JsonRequestBehavior.AllowGet);
        }
    }
}