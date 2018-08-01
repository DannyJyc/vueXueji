using System.Web.Mvc;
using testxueji.Models;
using vuexueji.DAL;

namespace vuexueji.Controllers
{
    public class MajorsController : Controller
    {
        // GET: Majors
        /// <summary>
        /// 用于返回专业（major）主页
        /// </summary>
        /// <returns>返回视图</returns>
        public ViewResult Index()
        {
            return View();
        }

        /// <summary>
        /// 列出所有专业（major）
        /// </summary>
        /// <returns>返回json格式的所有专业（major）信息</returns>
        [HttpGet]
        public JsonResult Get()
        {
            if ((string)Session["Power"] != "1") return Json("0");

            return Json(MajorsDal.List(), JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 用于为专业（major）增加一条信息
        /// </summary>
        /// <param name="m">类型：Majors</param>
        /// <returns>返回json格式的所有专业（major）信息</returns>
        [HttpPost]
        public JsonResult Add(Majors m)
        {
            if ((string)Session["Power"] != "1") return Json("0");

            MajorsDal.Add(m);
            return Json(MajorsDal.List());
        }
        /// <summary>
        /// 用于修改一条专业（major）信息
        /// </summary>
        /// <param name="m">类型：Majors，根据Majors中的ID修改对应的内容</param>
        /// <returns>返回json格式的所有专业（major）信息</returns>
        [HttpPost]
        public JsonResult Edit(Majors m)
        {
            if ((string)Session["Power"] != "1") return Json("0");

            MajorsDal.Edit(m);
            return Json(MajorsDal.List());
        }
        /// <summary>
        /// 用于删除对应ID的一条专业（major）信息
        /// </summary>
        /// <param name="id">类型：int，删除对应majorID的信息</param>
        /// <returns>返回json格式的所有专业（major）信息</returns>
        [HttpPost]
        public JsonResult Del(int id)
        {
            if ((string)Session["Power"] != "1") return Json("0");

            MajorsDal.Del(id);
            return Json(MajorsDal.List());
        }
    }
}