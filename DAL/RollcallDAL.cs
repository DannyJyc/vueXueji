using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using testxueji.Models;

namespace vuexueji.DAL
{
    public class RollcallDal
    {
        /// <summary>
        /// 返回对应rollcallid的学生状态
        /// </summary>
        /// <param name="id">类型 int，rollcallid</param>
        /// <returns>对应rollcallid的学生状态（json字符串）</returns>
        public static string StringJson(int id)
        {
            using (var db = new XuejiContext())
            {
                var json = db.Rollcalls.SingleOrDefault(rc => rc.Id == id);
                return json.StudentState;
            }
        }

        /// <summary>
        /// 返回所有点名信息（rollcall）
        /// </summary>
        /// <returns>返回所有点名信息</returns>
        public static IEnumerable AllList()
        {
            var db = new XuejiContext();
            var list = from rollcall in db.Rollcalls
                       select rollcall;
            return list;

        }

        /// <summary>
        /// 返回对应课程（Coursesarrangingid）的所有点名信息
        /// </summary>
        /// <param name="id">类型int，coursesarrangingid</param>
        /// <returns>返回类型IEnumerable<Rollcall>的所有点名信息</returns>
        public static IEnumerable<Rollcall> List(int id)
        {
            var db = new XuejiContext();
            var list = from rc in db.Rollcalls
                       where rc.CoursesArrangingId == id
                       select rc;
            return list.ToList();
        }

        /// <summary>
        /// 修改对应rollcallID的学生状态
        /// </summary>
        /// <param name="studentState">类型 StudentState中id以供修改rollcall的学生状态</param>
        /// <returns>返回字符串“1”为修改成功</returns>
        public static string EditStudentsStatus(StudentState studentState)
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
                    var studentsid = Convert.ToInt16(namearr[i]);
                    var single = db.Studentses.SingleOrDefault(s => s.Id == studentsid);
                    str += str == "[" ? "{\"Id\":\"" + i + "\",\"StudentsId\":\"" + namearr[i] + "\",\"StudentsName\":\"" + single.Name + "\",\"State\":\"" + statuearr[i] + "\"}" : ",{\"Id\":\"" + i + "\",\"StudentsId\":\"" + namearr[i] + "\",\"StudentsName\":\"" + single.Name + "\",\"State\":\"" + statuearr[i] + "\"}";
                }
                str += "]";//后更换stringbuilder
                var singlerollcall = db.Rollcalls.SingleOrDefault(r => r.Id == studentState.Id);
                if (singlerollcall != null) singlerollcall.StudentState = str;
                db.SaveChanges();

            }

            return "1";
        }

        /// <summary>
        /// 新增一条点名记录（rollcall）
        /// </summary>
        /// <param name="studentState">类型StudentState，学生状态</param>
        /// <param name="id">类型 int，coursesarrangingId</param>
        /// <param name="handlers">类型 string，操作者</param>
        /// <returns>返回字符串“1”为修改成功</returns>
        public static string StudentsStatus(StudentState studentState, int id,string handlers)
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
                    var studentsid = Convert.ToInt16(namearr[i]);
                    var single = db.Studentses.SingleOrDefault(s => s.Id == studentsid);
                    str += str == "[" ? "{\"Id\":\"" + i + "\",\"StudentsId\":\"" + namearr[i] + "\",\"StudentsName\":\"" + single.Name + "\",\"State\":\"" + statuearr[i] + "\"}" : ",{\"Id\":\"" + i + "\",\"StudentsId\":\"" + namearr[i] + "\",\"StudentsName\":\"" + single.Name + "\",\"State\":\"" + statuearr[i] + "\"}";
                }
                str += "]";//后更换stringbuilder

                var rollcall = new Rollcall
                {
                    CoursesArrangingId = id,
                    StudentState = str,
                    CreateTime = DateTime.Now,
                    Name = handlers//从控制器用session传递参数到这个方法

                };
                db.Rollcalls.Add(rollcall);
                db.SaveChanges();
            }

            return "1";
        }
        /// <summary>
        /// 出错的友好提示
        /// </summary>
        /// <returns>返回友好提示的字符串</returns>
        public static string ErroeWhenNull()
        {
            return "未找到指定对象！";
        }

        /// <summary>
        /// 列出对应Coursesarranging的所有学生
        /// </summary>
        /// <param name="id">类型 int，coursesarrangingId</param>
        /// <returns>返回类型IEnumerable<StudentsRollcall> 的所有学生</returns>
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

        /// <summary>
        /// 返回对应coursesarrangingId的一条课程信息
        /// </summary>
        /// <param name="id">类型 int，CoursesarrangingId</param>
        /// <returns>返回类型CoursesArrangingName的一条数据</returns>
        public static CoursesArrangingName SingleCoursesArranging(int id)
        {
            var db = new XuejiContext();
            var list = (from coursesarranging in db.CoursesArrangings
                        join classes in db.Classeses on coursesarranging.ClassesId equals classes.Id
                        join majors in db.Majorses on classes.MajorsId equals majors.Id
                        join courses in db.Courseses on coursesarranging.CoursesId equals courses.Id
                        where coursesarranging.Id == id
                        select new CoursesArrangingName
                        {
                            ClassesName = classes.Year + majors.Name,
                            CoursesName = courses.Name,
                            WeekDays = coursesarranging.WeekDays,
                            LessonsOrder = coursesarranging.LessonsOrder
                        }).SingleOrDefault();

            return list;
        }

        /// <summary>
        /// 显示讲师当天所教的所有课程
        /// </summary>
        /// <param name="id">类型 int，LecturerId</param>
        /// <returns>返回类型IEnumerable<CoursesArrangingName>的所有课程信息</returns>
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
                //if (item == 1)
                //{
                //    var date = DateTime.Now.ToString("yyyy-MM-dd");
                //    var str = "[";
                //    foreach (var i in list)
                //    {
                //        var rollcalllist = from rollcall in db.Rollcalls
                //                           join coursesarranging in db.CoursesArrangings on rollcall.CoursesArrangingId equals
                //                               coursesarranging.Id
                //                           where coursesarranging.ClassesId == i.ClassesId && coursesarranging.CoursesId == i.CoursesId
                //                           select new
                //                           {
                //                               StudentsStatus = rollcall.StudentState
                //                           };
                //        var gross = 0;
                //        var normal = 0;
                //        foreach (var n in rollcalllist)
                //        {
                //            var temp = JsonConvert.DeserializeObject<List<StudentState>>(n.StudentsStatus);
                //            foreach (var s in temp)
                //            {
                //                normal += Convert.ToInt16(s.State) == 1|| Convert.ToInt16(s.State) == 2 ? 1 : 0;
                //                gross++;
                //            }
                //        }
                //    }

                //    str += "]";
                //}
                return list.ToList();
            }
        }
    }
}