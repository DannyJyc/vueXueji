using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using testxueji.Models;

namespace vuexueji.DAL
{
    public class ClasstimeDAL
    {
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

        public static IEnumerable<Classroom> Classrooms()
        {
            using (var db = new XuejiContext())
            {
                var list = from cr in db.Classrooms
                           select cr;
                return list.ToList();
            }
        }

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