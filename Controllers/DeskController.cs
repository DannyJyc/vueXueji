using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using testxueji.Models;
using vuexueji.DAL;

namespace vuexueji.Controllers
{
    public class DeskController : Controller
    {
        // GET: Desk

        /// <summary>
        /// 返回到老师的控制台首页，并用session传过来的id获取到姓名传到前台！
        /// </summary>
        /// <returns>返回视图</returns>
        public ActionResult Teachers()
        {
            var db = new XuejiContext();
            var item = Convert.ToInt16(Session["Id"]);
            var single = db.Lectureres.SingleOrDefault(l => l.Id ==item);
            ViewBag.Name = single.Name;
            ViewBag.exams = DeskDAL.Exams(Convert.ToInt16(Session["Id"]));
            return View();
        }

        /// <summary>
        /// 返回到学生的控制台首页，并用session传过来的id获取到姓名传到前台！
        /// </summary>
        /// <returns>返回视图</returns>
        public ActionResult Students()
        {
            var db = new XuejiContext();
            var item = Convert.ToInt16(Session["Id"]);
            var single = db.Studentses.SingleOrDefault(s => s.Id == item);
            ViewBag.Name = single.Name;
            ViewBag.gross = DeskDAL.GrossCount(Convert.ToInt16(Session["Id"]));
            ViewBag.check = DeskDAL.Check(Convert.ToInt16(Session["Id"]));
            return View();
        }

        /// <summary>
        /// 给视图提供学生每节课的出勤率
        /// </summary>
        /// <returns>返回json数据，格式“CoursesName：，Count：，”</returns>
        public JsonResult StudentsCount()
        {
            return Json(DeskDAL.Count(Convert.ToInt16(Session["Id"])), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 给视图提供学生的成绩
        /// </summary>
        /// <returns>返回json数据，格式“TimeStamp: ,Name:  ,CoursesName: ,Score: ”</returns>
        public JsonResult StudentsScore()
        {
            return Json(DeskDAL.Score(Convert.ToInt16(Session["Id"])), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 给视图提供老师所教班级的出勤率统计
        /// </summary>
        /// <returns>返回json数据，格式“ClassName：，CoursName:,Count:,”</returns>
        public JsonResult LecturerCount()
        {
            return Json(DeskDAL.TeachersCount(Convert.ToInt16(Session["Id"])), JsonRequestBehavior.AllowGet);
        }
    }
}