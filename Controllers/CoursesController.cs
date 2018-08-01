using System.Web.Mvc;
using testxueji.Models;
using vuexueji.DAL;

namespace vuexueji.Controllers
{
    public class CoursesController : Controller
    {
        // GET: Courses

        /// <summary>
        /// 返回到课程（courses）的主页
        /// </summary>
        /// <returns>返回视图</returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 得到视图所需的数据
        /// </summary>
        /// <returns>返回json格式的所有课程（courses）</returns>
        public ActionResult Get()
        {
            if ((string)Session["Power"] != "1") return Json("0");

            return Json(CoursesDal.List(),JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 新增一条课程信息（courses）
        /// </summary>
        /// <param name="co">类型为Courses</param>
        /// <returns>返回json格式的所有课程（courses）</returns>
        [HttpPost]
        public JsonResult Add(Courses co)
        {
            if ((string)Session["Power"] != "1") return Json("0");

            CoursesDal.Add(co);
            return Json(CoursesDal.List());
        }

        /// <summary>
        /// 修改一条课程信息
        /// </summary>
        /// <param name="co">类型为Courses</param>
        /// <returns>返回json格式的所有课程（courses）</returns>
        [HttpPost]
        public JsonResult Edit(Courses co)
        {
            if ((string)Session["Power"] != "1") return Json("0");

            CoursesDal.Edit(co);
            return Json(CoursesDal.List());
        }

        /// <summary>
        /// 根据课程ID删除对应的课程
        /// </summary>
        /// <param name="id">类型int,coursesId</param>
        /// <returns>返回json格式的所有课程（courses）</returns>
        [HttpPost]
        public JsonResult Del(int id)
        {
            if ((string)Session["Power"] != "1") return Json("0");

            CoursesDal.Del(id);
            return Json(CoursesDal.List());
        }
    }
}