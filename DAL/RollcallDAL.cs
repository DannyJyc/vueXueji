using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using testxueji.Models;

namespace vuexueji.DAL
{
    public class RollcallDAL
    {
        public static string StringJson(int id)
        {
            using (var db = new XuejiContext())
            {
                var json = db.Rollcalls.SingleOrDefault(rc => rc.Id == id);
                return json.StudentState;
            }
        }

        public static IEnumerable<Rollcall> List(int id)
        {
            var db = new XuejiContext();
            var list = from rc in db.Rollcalls
                       where rc.CoursesArrangingId == id
                       select rc;
            return list.ToList();
        }

        public static string StudentsStatus(StudentState studentState, int id)
        {
            var name = studentState.StudentsId;
            var state = studentState.State;
            var namearr = name.Split(',');
            var statuearr = state.Split(',');
            using (var db = new XuejiContext())
            {
                var str = "[";

                for (var i = 0; i < namearr.Length; i++)
                {
                    var studentsid = Convert.ToInt16(namearr[i]) ;
                    var single = db.Studentses.SingleOrDefault(s => s.Id == studentsid);
                    str += str == "[" ? "{\"StudentsId\":\"" + namearr[i] + "\",\"StudentsName\":\"" + single.Name + "\",\"State\":\"" + statuearr[i] + "\"}" : ",{\"StudentsId\":\"" + namearr[i] + "\",\"StudentsName\":\"" + single.Name + "\",\"State\":\"" + statuearr[i] + "\"}";
                }
                str += "]";//后更换stringbuilder

                var rollcall = new Rollcall
                {
                    CoursesArrangingId = id,
                    StudentState = str,
                    CreateTime = DateTime.Now,
                    Name = "庄德鑫"//从控制器用session传递参数到这个方法

                };
                db.Rollcalls.Add(rollcall);
                db.SaveChanges();
            }

            return "1";
        }

        public static string erroeWhenNull()
        {
            return "未找到指定对象！";
        }

        public static IEnumerable<StudentsRollcall> StudentsList(int id)
        {
            var db = new XuejiContext();

            var list = db.CoursesArrangings.SingleOrDefault(ca => ca.Id == id);

            if (list != null)
            {
                var cid = list.ClassesId;
                var studentslist = from s in db.Studentses
                                   where s.ClassesId == cid
                                   select new StudentsRollcall()
                                   {
                                       Id = s.Id,
                                       Name = s.Name
                                   };
                return studentslist;
            }

            return null;
        }

        public static IEnumerable<CoursesArrangingName> LeList(int id)
        {
            string[] weekdays = { "7", "1", "2", "3", "4", "5", "6" };
            var week = weekdays[Convert.ToInt32(DateTime.Now.DayOfWeek)];
            int weekday = Convert.ToInt16(week);
            using (var db = new XuejiContext())
            {
                var list = from ca in db.CoursesArrangings
                           join wa in db.WeekArrangings on ca.WeekArrangingId equals wa.Id
                           join c in db.Classeses on ca.ClassesId equals c.Id
                           join m in db.Majorses on ca.Classes.MajorsId equals m.Id
                           join co in db.Courseses on ca.CoursesId equals co.Id
                           join l in db.Lectureres on ca.Courses.LecturerId equals l.Id
                           join cr in db.Classrooms on ca.ClassroomId equals cr.Id
                           where l.Id == id && ca.WeekDays == weekday//显示当天的课程
                           orderby ca.LessonsOrder
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
    }
}