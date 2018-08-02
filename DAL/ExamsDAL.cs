using System;
using System.Collections.Generic;
using System.Linq;
using testxueji.Models;

namespace vuexueji.DAL
{
    public class ExamsDal
    {
        /// <summary>
        /// 返回对应examsID的学生考试信息的json字符串
        /// </summary>
        /// <param name="id">类型 int，examsID</param>
        /// <returns>对应examsID的学生考试信息的json字符串</returns>
        public static string StringJson(int id)
        {
            using (var db = new XuejiContext())
            {
                var json = db.Examses.SingleOrDefault(e => e.Id == id);
                return json.StudentScore;
            }
        }

        /// <summary>
        /// 列出对应班级的所有考试信息
        /// </summary>
        /// <param name="id">类型 int，classesID</param>
        /// <returns>返回对应classesID的所有考试信息</returns>
        public static IEnumerable<ExamsList> List(int id)
        {
            var db = new XuejiContext();
            var list = from e in db.Examses
                       join coursesarranging in db.CoursesArrangings on e.CoursesArrangingId equals coursesarranging.Id
                       join courses in db.Courseses on coursesarranging.CoursesId equals courses.Id
                       where coursesarranging.ClassesId == id
                       select new ExamsList()
                       {
                           Id = e.Id,
                           Name = e.Name,
                           TimeStamp = e.TimeStamp,
                           CoursesName = courses.Name
                       };
            return list.ToList();

        }

        /// <summary>
        /// 为exams表修改一条记录
        /// </summary>
        /// <param name="eb">类型 Examsbox中id 供exams修改对应数据</param>
        /// <returns>返回字符串“1”为修改成功</returns>
        public static string Edit(ExamsBox eb)
        {
            var name = eb.StudentsId;
            var score = eb.Score;
            var namearr = name.Split(',');
            var scorearr = score.Split(',');
            using (var db = new XuejiContext())
            {
                var str = "[";
                for (var i = 0; i < namearr.Length; i++)
                {
                    var studentsid = Convert.ToInt16(namearr[i]);
                    var single = db.Studentses.SingleOrDefault(s => s.Id == studentsid);
                    str += str == "["
                        ? "{\"StudentsId\":\"" + namearr[i] + "\",\"StudentsName\":\"" + single.Name +
                          "\",\"Score\":\"" + scorearr[i] + "\"}"
                        : ",{\"StudentsId\":\"" + namearr[i] + "\",\"StudentsName\":\"" + single.Name +
                          "\",\"Score\":\"" + scorearr[i] + "\"}";
                }

                str += "]"; //后更换stringbuilder
                var singlescore = db.Examses.SingleOrDefault(ex => ex.Id == eb.Id);
                singlescore.StudentScore = str;
                db.SaveChanges();
            }

            return "1";
        }

        /// <summary>
        /// 为exams新增一条记录
        /// </summary>
        /// <param name="eb">类型examsbox，供exams新增记录</param>
        /// <returns>返回字符串“1”为新增成功</returns>
        public static string Add(ExamsBox eb)
        {
            var name = eb.StudentsId;
            var score = eb.Score;
            var namearr = name.Split(',');
            var scorearr = score.Split(',');
            using (var db = new XuejiContext())
            {
                var str = "[";
                for (var i = 0; i < namearr.Length; i++)
                {
                    var studentsid = Convert.ToInt16(namearr[i]);
                    var single = db.Studentses.SingleOrDefault(s => s.Id == studentsid);
                    str += str == "[" ? "{\"StudentsId\":\"" + namearr[i] + "\",\"StudentsName\":\"" + single.Name + "\",\"Score\":\"" + scorearr[i] + "\"}" : ",{\"StudentsId\":\"" + namearr[i] + "\",\"StudentsName\":\"" + single.Name + "\",\"Score\":\"" + scorearr[i] + "\"}";
                }
                str += "]";//后更换stringbuilder

                var exams = new Exams
                {
                    CoursesArrangingId = eb.CoursesArrangingId,
                    TimeStamp = eb.Examstime,
                    Name = eb.Examsname,
                    StudentScore = str
                };
                db.Examses.Add(exams);
                db.SaveChanges();
            }

            return "1";
        }
    }
}