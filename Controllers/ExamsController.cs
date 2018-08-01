using System.Web.Mvc;
using testxueji.Models;
using vuexueji.DAL;

namespace vuexueji.Controllers
{
    public class ExamsController : Controller
    {
        // GET: Exams
        /// <summary>
        /// 新增考试（Exams）
        /// </summary>
        /// <param name="id">类型int，classesId</param>
        /// <returns></returns>
        public ActionResult Index(int id)
        {
            ViewBag.id = id;
            ViewBag.students = RollcallDal.StudentsList(id);
            return View();
        }

        /// <summary>
        /// 修改学生成绩
        /// </summary>
        /// <param name="studentscore">类型 ExamsBox</param>
        /// <returns>返回字符串，返回“1”则修改成功</returns>
        public ActionResult Edit(ExamsBox studentscore)
        {
            return Content(ExamsDal.Edit(studentscore));
        }

        /// <summary>
        /// 新增学生成绩
        /// </summary>
        /// <param name="eb">类型 ExamsBox</param>
        /// <returns>返回字符串，返回“1”新增成功</returns>
        public ActionResult Addexams(ExamsBox eb)
        {
            
            return Content(ExamsDal.Add(eb));
        }

        /// <summary>
        /// 返回视图并返回对应班级的考试信息数据
        /// </summary>
        /// <param name="id">类型 int，classesID</param>
        /// <returns>返回视图</returns>
        public ActionResult List(int id)
        {
            ViewBag.list = ExamsDal.List(id);
            return View();
        }

        /// <summary>
        /// 返回对应examID的学生列表
        /// </summary>
        /// <param name="id">类型int，examsID</param>
        /// <returns>返回视图</returns>
        public ActionResult Score(int id)
        {
            ViewBag.id = id;
            return View();
        }

        /// <summary>
        /// 根据对应examsID返回对应的学生的成绩
        /// </summary>
        /// <param name="id">类型 int，examsID</param>
        /// <returns>返回学生-学生成绩的json数据</returns>
        public ActionResult StudentsJson(int id)
        {
            return Content(ExamsDal.StringJson(id));
        }

        /// <summary>
        /// 返回所有班级名称和老师的信息
        /// </summary>
        /// <returns>返回视图</returns>
        public ActionResult AllClasses()
        {
            ViewBag.classes = ClassesDal.List();
            return View();
        }
    }
}