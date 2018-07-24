using System;
using System.Collections.Generic;
using System.Linq;
using testxueji.Models;

namespace vuexueji.DAL
{
    public class ExamsDal
    {
        public static string StringJson(int id)
        {
            using (var db = new XuejiContext())
            {
                var json = db.Examses.SingleOrDefault(e => e.Id == id);
                return json.StudentScore;
            }
        }

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