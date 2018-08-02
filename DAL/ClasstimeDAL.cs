using System;
using System.Collections.Generic;
using System.Linq;
using testxueji.Models;

namespace vuexueji.DAL
{
    public class ClasstimeDal
    {
        /// <summary>
        /// 添加一个学期（weekarranging）
        /// </summary>
        /// <param name="start">类型 string，开始时间</param>
        /// <param name="end">类型 string，结束时间</param>
        /// <returns>返回字符串 “1” 即添加成功</returns>
        public static string AddWeekarranging(string start, string end)
        {
            DateTime datestart, dateend;
            datestart = Convert.ToDateTime(start);
            dateend = Convert.ToDateTime(end);
            var startYear = datestart.Year;
            var endYear = dateend.Year;
            var session = datestart.Month;
            var startweek = 0;
            var endweek = 0;
            var startdays = datestart.DayOfYear;
            var enddays = dateend.DayOfYear;
            Week(ref startweek, startdays, startYear);
            Week(ref endweek, enddays, startYear);
            if (startYear != endYear)
            {
                Week(ref endweek, Convert.ToDateTime(startYear + "-12-31").DayOfYear + enddays, startYear);
            }
            if (endweek - startweek + 1 != 18)
            {
                return (endweek - startweek + 1).ToString();
            }
            using (var db = new XuejiContext())
            {
                var weekarranging = new WeekArranging
                {
                    Years = startYear,
                    Session = session > 6 ? 1 : 2,
                    StartWeek = startweek,
                    EndWeek = endweek
                };
                db.WeekArrangings.Add(weekarranging);
                db.SaveChanges();
            }
            return "1";
        }

        /// <summary>
        /// 计算输入当前时间属于该年第几周
        /// </summary>
        /// <param name="startweek">类型 ref int，想要输出的星期数</param>
        /// <param name="days">类型 int，当前时间到目前为止的天数</param>
        /// <param name="years">类型 int，当前年份</param>
        private static void Week(ref int startweek, int days, int years)
        {
            string[] weekdays = { "7", "1", "2", "3", "4", "5", "6" };
            var week = weekdays[Convert.ToInt32(Convert.ToDateTime(years + "-01-01").DayOfWeek)];
            var temp = 7 - Convert.ToInt16(week) + 1;
            days -= temp;
            startweek = 1;
            while (days > 0)
            {

                startweek++;
                days -= 7;
            }
        }
        /// <summary>
        /// 修改一条课程具体信息
        /// </summary>
        /// <param name="coursesarranging">类型CoursesArranging 中的Id 以供修改记录</param>
        public static void Edit(CoursesArranging coursesarranging)
        {
            using (var db = new XuejiContext())
            {
                var single = db.CoursesArrangings.SingleOrDefault(ca => ca.Id == coursesarranging.Id);
                if (single != null)
                {
                    single.ClassesId = coursesarranging.ClassesId;
                    single.CoursesId = coursesarranging.CoursesId;
                    single.WeekArrangingId = coursesarranging.WeekArrangingId;
                    single.WeekStart = coursesarranging.WeekStart;
                    single.WeekEnd = coursesarranging.WeekEnd;
                    single.LessonsOrder = coursesarranging.LessonsOrder;
                    single.ClassroomId = coursesarranging.ClassroomId;
                    single.WeekDays = coursesarranging.WeekDays;
                }

                db.SaveChanges();
            }
        }

        /// <summary>
        /// 为课程新增一条记录（CoursesArranging）
        /// </summary>
        /// <param name="ca">类型Coursesarranging 以供新增记录</param>
        public static void Add(CoursesArranging ca)
        {
            using (var db = new XuejiContext())
            {
                var coursesarranging = new CoursesArranging
                {
                    ClassesId = ca.ClassesId,
                    CoursesId = ca.CoursesId,
                    WeekArrangingId = ca.WeekArrangingId,
                    WeekStart = ca.WeekStart,
                    WeekEnd = ca.WeekEnd,
                    LessonsOrder = ca.LessonsOrder,
                    ClassroomId = ca.ClassroomId,
                    WeekDays = ca.WeekDays
                };
                db.CoursesArrangings.Add(coursesarranging);
                db.SaveChanges();
            }
        }

        /// <summary>
        /// 返回所有教室门牌号的
        /// </summary>
        /// <returns>返回类型IEnumerable<Classroom>的所有数据</returns>
        public static IEnumerable<Classroom> Classrooms()
        {
            using (var db = new XuejiContext())
            {
                var list = from cr in db.Classrooms
                           select cr;
                return list.ToList();
            }
        }

        /// <summary>
        /// 返回所有学期的
        /// </summary>
        /// <returns>返回类型IEnumerable<WeekArrangingName> 的所有学期列表</returns>
        public static IEnumerable<WeekArrangingName> WeekList()
        {
            using (var db = new XuejiContext())
            {
                var list = from wa in db.WeekArrangings
                           select new WeekArrangingName()
                           {
                               Id = wa.Id,
                               Years = wa.Years,
                               Session = wa.Session == 1 ? "上学期" : "下学期"
                           };
                return list.ToList();
            }
        }

        /// <summary>
        /// 返回所有课程列表，并且按照星期排序
        /// </summary>
        /// <returns>返回类型IEnumerable<CoursesArrangingName>的所有课程</returns>
        public static IEnumerable<CoursesArrangingName> List()
        {
            using (var db = new XuejiContext())
            {
                var list = from ca in db.CoursesArrangings
                           join wa in db.WeekArrangings on ca.WeekArrangingId equals wa.Id
                           join c in db.Classeses on ca.ClassesId equals c.Id
                           join m in db.Majorses on ca.Classes.MajorsId equals m.Id
                           join co in db.Courseses on ca.CoursesId equals co.Id
                           join l in db.Lectureres on ca.Courses.LecturerId equals l.Id
                           join cr in db.Classrooms on ca.ClassroomId equals cr.Id
                           orderby ca.WeekDays
                           select new CoursesArrangingName()
                           {
                               Id = ca.Id,
                               ClassesName = c.Year + m.Name,
                               CoursesName = co.Name,
                               LectureresName = l.Name,
                               WeekArranging = wa.Years,
                               WeekSession = wa.Session,
                               WeekStart = ca.WeekStart,
                               WeekEnd = ca.WeekEnd,
                               LessonsOrder = ca.LessonsOrder,
                               ClassroomName = cr.Name,
                               WeekDays = ca.WeekDays,
                               ClassesId = ca.ClassesId,
                               CoursesId = ca.CoursesId,
                               WeekArrangingId = ca.WeekArrangingId,
                               ClassroomId = ca.ClassroomId
                           };
                return list.ToList();
            }
        }

        /// <summary>
        /// 按照CoursesArrangingid查找一条课程信息
        /// </summary>
        /// <param name="id">类型 int，CoursesarrangingId</param>
        /// <returns>返回CoursesArrangingName类型的一条数据</returns>
        public static CoursesArrangingName SingleCoursesArrangingNames(int id)
        {
            using (var db = new XuejiContext())
            {
                var list = from ca in db.CoursesArrangings
                           join wa in db.WeekArrangings on ca.WeekArrangingId equals wa.Id
                           join c in db.Classeses on ca.ClassesId equals c.Id
                           join m in db.Majorses on ca.Classes.MajorsId equals m.Id
                           join co in db.Courseses on ca.CoursesId equals co.Id
                           join l in db.Lectureres on ca.Courses.LecturerId equals l.Id
                           join cr in db.Classrooms on ca.ClassroomId equals cr.Id
                           where ca.Id == id
                           orderby ca.WeekDays
                           select new CoursesArrangingName()
                           {
                               Id = ca.Id,
                               ClassesName = c.Year + m.Name,
                               CoursesName = co.Name,
                               LectureresName = l.Name,
                               WeekArranging = wa.Years,
                               WeekSession = wa.Session,
                               WeekStart = ca.WeekStart,
                               WeekEnd = ca.WeekEnd,
                               LessonsOrder = ca.LessonsOrder,
                               ClassroomName = cr.Name,
                               WeekDays = ca.WeekDays
                           };
                return list.SingleOrDefault();
            }

        }

        /// <summary>
        /// 按照班级Id(classId)查找符合的所有课程信息
        /// </summary>
        /// <param name="id">类型 int，classesID</param>
        /// <returns>返回类型IEnumerable<CoursesArrangingName>的对应班级id的所有课程信息</returns>
        public static IEnumerable<CoursesArrangingName> ClassesList(int id)
        {
            using (var db = new XuejiContext())
            {
                var list = from ca in db.CoursesArrangings
                           join wa in db.WeekArrangings on ca.WeekArrangingId equals wa.Id
                           join c in db.Classeses on ca.ClassesId equals c.Id
                           join m in db.Majorses on ca.Classes.MajorsId equals m.Id
                           join co in db.Courseses on ca.CoursesId equals co.Id
                           join l in db.Lectureres on ca.Courses.LecturerId equals l.Id
                           join cr in db.Classrooms on ca.ClassroomId equals cr.Id
                           where ca.ClassesId == id
                           orderby ca.WeekDays
                           select new CoursesArrangingName()
                           {
                               Id = ca.Id,
                               ClassesName = c.Year + m.Name,
                               ClassesId = c.Id,
                               CoursesName = co.Name,
                               LectureresName = l.Name,
                               WeekArranging = wa.Years,
                               WeekSession = wa.Session,
                               WeekStart = ca.WeekStart,
                               WeekEnd = ca.WeekEnd,
                               LessonsOrder = ca.LessonsOrder,
                               ClassroomName = cr.Name,
                               WeekDays = ca.WeekDays
                           };
                return list.ToList();
            }

        }
    }
}