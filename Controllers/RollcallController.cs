using System;
using System.Web.Mvc;
using testxueji.Models;
using vuexueji.DAL;

namespace vuexueji.Controllers
{
    public class RollcallController : Controller
    {
        // GET: Rollcall

        /// <summary>
        /// 用于返回到点名首页，首页{班级名称，课程名称，开始周~结束周，周几，第几节课}
        /// </summary>
        /// <returns>返回视图</returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 《测试用》用讲师的lecturerid列出对应老师的{班级名称，课程名称，开始周~结束周，周几，第几节课}
        /// </summary>
        /// <returns>返回json格式的对应讲师的信息</returns>
        public JsonResult Get()
        {
            return Json(RollcallDal.LeList(1), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 列出对应id的rollcall的StudentState
        /// </summary>
        /// <param name="id">类型 int，rollcall的Id</param>
        /// <returns>返回到视图</returns>
        [HttpGet]
        public ActionResult Addrollcall(int id)
        {
            Session["tempid"] = id;
            ViewBag.students = RollcallDal.StudentsList(id);
            if (RollcallDal.StudentsList(id) == null)
            {
                return Content(RollcallDal.ErroeWhenNull());
            }
            return View();
        }

        /// <summary>
        /// 接受Addrollcall的post之后将数据传到DAL处理成json字符串并添加
        /// </summary>
        /// <param name="sr">类型 StudentsState，{Id，StudentsId,State}</param>
        /// <returns>返回字符串如果是“1”新增成功</returns>
        [HttpPost]
        public ActionResult Addrollcall(StudentState sr)
        {
            
            return Content(RollcallDal.StudentsStatus(sr, Convert.ToInt16(Session["tempid"]), Convert.ToString(Session["Name"])));
        }

        /// <summary>
        /// 用于修改rollcall对应的每个学生的状态
        /// </summary>
        /// <param name="sr">类型 StudentsState，{Id，StudentsId,State}</param>
        /// <returns>返回字符串如果是“1”修改成功</returns>
        [HttpPost]
        public ActionResult Editrollcall(StudentState sr)
        {
            return Content(RollcallDal.EditStudentsStatus(sr));
        }

        /// <summary>
        /// 用于返回对应rollcall Id（session["tempid"])的点名信息
        /// </summary>
        /// <returns>返回视图</returns>
        public ActionResult ListRollcall()
        {
            ViewBag.rollcall = RollcallDal.List(Convert.ToInt16(Session["tempid"]));
            return View();
        }

        /// <summary>
        /// 返回到StudentsStatus并带一个参数到视图
        /// </summary>
        /// <param name="id">类型 int，rollcall Id</param>
        /// <returns>返回视图</returns>
        public ActionResult StudentsStatus(int id)
        {
            ViewBag.id = id;
            return View();
        }

        /// <summary>
        /// 返回rollcall 的 StudentState信息
        /// </summary>
        /// <param name="id">类型 int，rollcall Id</param>
        /// <returns>返回json格式的rollcall 的 StudentState</returns>
        public ActionResult StudentsJson(int id)
        {
            return Content(RollcallDal.StringJson(id));
        }

        /// <summary>
        /// 列出所有rollcall的信息
        /// </summary>
        /// <returns>返回视图</returns>
        [HttpGet]
        public ActionResult AllList()
        {
            ViewBag.rollcall = RollcallDal.AllList();
            return View();
        }
    }
}