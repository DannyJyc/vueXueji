using System.Web.Mvc;
using testxueji.Models;
using vuexueji.DAL;

namespace vuexueji.Controllers
{
    public class ClasstimeController : Controller
    {
        // GET: Classtime

        /// <summary>
        /// 返回到添加学期（weekarranging）的页
        /// </summary>
        /// <returns>返回视图（index）</returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 返回到课表（classtime）的页
        /// </summary>
        /// <returns>返回视图（classtime）</returns>
        public ActionResult Classtime()
        {
            return View();
        }

        /// <summary>
        /// 返回到所有课表（AllClasstime）的页
        /// </summary>
        /// <returns>返回视图（allclasstime）</returns>
        public ActionResult AllClasstime()
        {
            return View();
        }

        /// <summary>
        /// 返回到修改课表（classtime）的页
        /// </summary>
        /// <returns>返回视图修改课表（classtime）的页</returns>
        [HttpGet]
        public ActionResult Edit()
        {
            return View();
        }

        /// <summary>
        /// 暂无用处，测试用！
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        /// <summary>
        /// 暂无用处，测试用！
        /// </summary>
        /// <param name="ca"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Add(CoursesArranging ca)
        {
            if ((string)Session["Power"] != "1") return Json("0");

            ClasstimeDal.Add(ca);
            return Json(ClasstimeDal.List());
        }

        /// <summary>
        /// 从Edit视图以post的方式，修改classtime
        /// </summary>
        /// <param name="ca">参数类型为CoursesArranging</param>
        /// <returns>返回所有的课表（classtime）</returns>
        [HttpPost]
        public ActionResult Edit(CoursesArranging ca)
        {
            if ((string)Session["Power"] != "1") return Json("0");

            ClasstimeDal.Edit(ca);
            return Json(ClasstimeDal.List());
        }

        /// <summary>
        /// 给视图提供classroom的数据
        /// </summary>
        /// <returns>返回json格式的教室（classroom）数据</returns>
        public JsonResult Classroom()
        {
            return Json(ClasstimeDal.Classrooms(), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 给视图提供classroom的数据
        /// </summary>
        /// <returns>返回json格式的学期（WeekArranging）数据</returns>
        public JsonResult WeekArranging()
        {
            return Json(ClasstimeDal.WeekList(), JsonRequestBehavior.AllowGet);//写死班级ID=2
        }

        /// <summary>
        /// 用于新增学期
        /// </summary>
        /// <param name="dateStart">类型string，学期开始时间</param>
        /// <param name="dateEnd">类型string，学期结束时间</param>
        /// <returns>返回字符串如果添加成功返回“1”</returns>
        public ActionResult AddWeek(string dateStart,string dateEnd)
        {            
            return Content(ClasstimeDal.AddWeekarranging(dateStart, dateEnd));
        }

        /// <summary>
        /// 未用到
        /// </summary>
        /// <returns></returns>
        public JsonResult SingleJsonResult()
        {
            return Json(ClasstimeDal.SingleCoursesArrangingNames(2), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 根据班级id（classes）查询对应班级课表
        /// </summary>
        /// <returns>返回对应班级ID的json数据</returns>
        public JsonResult Get()
        {
            return Json(ClasstimeDal.ClassesList(2), JsonRequestBehavior.AllowGet);//写死班级ID=2
        }

        /// <summary>
        /// 得到所有课表（classtime）
        /// </summary>
        /// <returns>返回所有课表（classtime）的json数据</returns>
        public JsonResult GetAll()
        {
            if ((string)Session["Power"] != "1") return Json("0");

            return Json(ClasstimeDal.List(), JsonRequestBehavior.AllowGet);
        }
    }
}