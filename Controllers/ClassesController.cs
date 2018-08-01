using System.Web.Mvc;
using testxueji.Models;
using vuexueji.DAL;

namespace vuexueji.Controllers
{
    public class ClassesController : Controller
    {
        // GET: Classes
        /// <summary>
        /// 返回到班级（classes）主页
        /// </summary>
        /// <returns>返回视图</returns>
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 获取所有班级（classes）的信息
        /// </summary>
        /// <returns>返回json格式班级信息</returns>
        public JsonResult Get()
        {
            if ((string)Session["Power"] != "1") return Json("0");

            return Json(ClassesDal.List(), JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 为班级（classes）新增一条记录
        /// </summary>
        /// <param name="c">传递类型为Classes类型的参数</param>
        /// <returns>返回json格式班级信息</returns>
        [HttpPost]
        public JsonResult Add(Classes c)
        {
            if ((string)Session["Power"] != "1") return Json("0");

            ClassesDal.Add(c);
            return Json(ClassesDal.List());
        }
        /// <summary>
        /// 为班级（classes）修改一条记录
        /// </summary>
        /// <param name="c">传递参数为Classes类型的参数</param>
        /// <returns>返回json格式班级信息</returns>
        [HttpPost]
        public JsonResult Edit(Classes c)
        {
            if ((string)Session["Power"] != "1") return Json("0");

            ClassesDal.Edit(c);
            return Json(ClassesDal.List());
        }
        /// <summary>
        /// 为班级（classes）删除一条记录
        /// </summary>
        /// <param name="id">整形id，对应的是classes表的Id（索引）</param>
        /// <returns>返回json格式班级信息</returns>
        [HttpPost]
        public JsonResult Del(int id)
        {
            if ((string)Session["Power"] != "1") return Json("0");

            ClassesDal.Del(id);
            return Json(ClassesDal.List());
        }
    }
}