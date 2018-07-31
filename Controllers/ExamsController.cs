using System.Web.Mvc;
using testxueji.Models;
using vuexueji.DAL;

namespace vuexueji.Controllers
{
    public class ExamsController : Controller
    {
        // GET: Exams
        public ActionResult Index(int id)
        {
            ViewBag.id = id;
            ViewBag.students = RollcallDal.StudentsList(id);
            return View();
        }

        public ActionResult Edit(ExamsBox studentscore)
        {
            return Content(ExamsDal.Edit(studentscore));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="eb"></param>
        /// <returns></returns>
        public ActionResult Addexams(ExamsBox eb)
        {
            
            return Content(ExamsDal.Add(eb));
        }

        public ActionResult List(int id)
        {
            ViewBag.list = ExamsDal.List(id);
            return View();
        }

        public ActionResult Score(int id)
        {
            ViewBag.id = id;
            return View();
        }

        public ActionResult StudentsJson(int id)
        {
            return Content(ExamsDal.StringJson(id));
        }

        public ActionResult AllClasses()
        {
            ViewBag.classes = ClassesDal.List();
            return View();
        }
    }
}