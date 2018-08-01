using System.Web.Mvc;
using testxueji.Models;
using vuexueji.DAL;

namespace vuexueji.Controllers
{
    public class StudentsController : Controller
    {
        // GET: Students

        /// <summary>
        /// 返回到学生（Students）的主页
        /// </summary>
        /// <returns>返回视图</returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 获取所有学生（Students）信息
        /// </summary>
        /// <returns>返回json格式的学生（students）信息</returns>
        public JsonResult Get()
        {
            if ((string)Session["Power"] != "1") return Json("0");

            return Json(StudentsDal.List(), JsonRequestBehavior.AllowGet);
        }

        //public ContentResult SingleName(int id)
        //{
        //    return Content(StudentsDAL.SingleName(id));
        //}

        /// <summary>
        /// 为学生（Students）新增一条记录
        /// </summary>
        /// <param name="s">类型 Students</param>
        /// <returns>返回json格式的学生（students）信息</returns>
        [HttpPost]
        public JsonResult Add(Students s)
        {
            if ((string) Session["Power"] != "1") return Json("0");
            StudentsDal.Add(s);
            return Json(StudentsDal.List());
        }

        /// <summary>
        /// 为对应Students Id的学生信息进行修改
        /// </summary>
        /// <param name="s">类型 Students</param>
        /// <returns>返回json格式的学生（students）信息</returns>
        [HttpPost]
        public JsonResult Edit(Students s)
        {
            if ((string)Session["Power"] != "1") return Json("0");

            StudentsDal.Edit(s,s.Id);
            return Json(StudentsDal.List());

        }

        /// <summary>
        /// 将对应StudentsId 的记录删除
        /// </summary>
        /// <param name="id">类型int，StudentsId</param>
        /// <returns>返回json格式的学生（students）信息</returns>
        [HttpPost]
        public JsonResult Delete(int id)
        {
            if ((string)Session["Power"] != "1") return Json("0");

            StudentsDal.Del(id);
            return Json(StudentsDal.List());
        }
    }
}