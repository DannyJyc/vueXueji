using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using testxueji.Models;

namespace vuexueji.DAL
{
    public class DeskDAL
    {
        //Count
        //rollcall表‘0’第一次点点名时未在标识‘1’正常 ‘2’为迟到标识 ‘3’第二次旷课标识
        public static double GrossCount(int id)
        {
            var db = new XuejiContext();
            var single = db.Studentses.SingleOrDefault(s => s.Id == id);
            int classesId = single.ClassesId;
            var list = from rollcall in db.Rollcalls
                       join coursesarranging in db.CoursesArrangings on rollcall.CoursesArrangingId equals coursesarranging.Id
                       where coursesarranging.ClassesId == classesId
                       select new
                       {
                           studentstate = rollcall.StudentState
                       };
            int gross = 0;
            int normal = 0;
            foreach (var i in list)
            {
                gross++;
                var temp = JsonConvert.DeserializeObject<List<StudentState>>(i.studentstate);
                foreach (var n in temp)
                {
                    normal += Convert.ToInt16(n.StudentsId) == id && Convert.ToInt16(n.State) == 1 ? 1 : 0;
                }

            }

            return (normal / gross)*100;
        }

        public static string Count(int id)
        {
            var db = new XuejiContext();
            var single = db.Studentses.SingleOrDefault(s => s.Id == id);
            int classesId = single.ClassesId;
            var list = (from rollcall in db.Rollcalls
                join coursesarranging in db.CoursesArrangings on rollcall.CoursesArrangingId equals coursesarranging.Id
                where coursesarranging.ClassesId == classesId
                //group coursesarranging by coursesarranging.CoursesId
                //into courses
                select new
                {
                    coursesId = coursesarranging.CoursesId
                }).Distinct();
            var str = "[";
            foreach (var i in list)
            {
                int gross = 0;
                int normal = 0;
                int coursesId = i.coursesId;
                var countlist = from rollcall in db.Rollcalls
                    join coursesarranging in db.CoursesArrangings on rollcall.CoursesArrangingId equals coursesarranging.Id
                    where coursesarranging.ClassesId == classesId&&coursesarranging.CoursesId==coursesId
                    select new
                    {
                        studentstate = rollcall.StudentState
                    };
                foreach (var n in countlist)
                {
                    gross++;
                    var temp = JsonConvert.DeserializeObject<List<StudentState>>(n.studentstate);
                    foreach (var s in temp)
                    {
                        normal += Convert.ToInt16(s.StudentsId) == id && Convert.ToInt16(s.State) == 1 ? 1 : 0;
                    }
                }

                var CoursesName = db.Courseses.SingleOrDefault(c => c.Id == coursesId);
                str += str == "[" ? "{\"CoursesName\":\"" + CoursesName.Name + "\",\"Count\":\"" + normal / gross*100 + "\"}" : ",{\"CoursesName\":\"" + CoursesName.Name + "\",\"Count\":\"" + normal / gross*100 + "\"}";

            }
            str += "]";
            return str;
        }

        public static string Score(int id)
        {
            var db = new XuejiContext();
            var single = db.Studentses.SingleOrDefault(s => s.Id == id);
            int classesId = single.ClassesId;
            var list = from exams in db.Examses
                join coursesarranging in db.CoursesArrangings on exams.CoursesArrangingId equals coursesarranging.Id
                join courses in db.Courseses on coursesarranging.CoursesId equals courses.Id
                where coursesarranging.ClassesId == classesId
                select new ExamsList()
                {
                    TimeStamp = exams.TimeStamp,
                    Name = exams.Name,
                    CoursesName = courses.Name,
                    StudentScore = exams.StudentScore
                };
            var str = "[";
            foreach (var i in list)
            {
                int Score = 0;
                var temp = JsonConvert.DeserializeObject<List<StudentScore>>(i.StudentScore);
                foreach (var n in temp)
                {
                    Score = n.StudentsId == id ? n.Score : -1;
                    if (Score != 0 || Score != -1) break;
                }
                str += str == "[" ? "{\"TimeStamp\":\"" + i.TimeStamp +"\",\"Name\":\"" + i.Name + "\",\"CoursesName\":\"" + i.CoursesName +"\",\"Score\":\"" + Score + "\"}" : ",{\"TimeStamp\":\"" + i.TimeStamp + "\",\"Name\":\"" + i.Name + "\",\"CoursesName\":\"" + i.CoursesName + "\",\"Score\":\"" + Score + "\"}";

            }

            str += "]";
            return str;
        }

        public static int[] Check(int id)
        {
            
            var db = new XuejiContext();
            var single = db.Studentses.SingleOrDefault(s => s.Id == id);
            int classesId = single.ClassesId;
            var list = from rollcall in db.Rollcalls
                join coursesarranging in db.CoursesArrangings on rollcall.CoursesArrangingId equals coursesarranging.Id
                where coursesarranging.ClassesId == classesId
                select new
                {
                    studentstate = rollcall.StudentState
                };
            int gross = 0;
            int cut = 0;//3
            int late = 0;//2
            foreach (var i in list)
            {
                gross++;
                var temp = JsonConvert.DeserializeObject<List<StudentState>>(i.studentstate);
                foreach (var n in temp)
                {
                    cut += Convert.ToInt16(n.StudentsId) == id && Convert.ToInt16(n.State) == 3 ? 1 : 0;
                    late += Convert.ToInt16(n.StudentsId) == id && Convert.ToInt16(n.State) == 2 ? 1 : 0;
                }

            }

            int[] check=new int[3];
            check[0] = gross;
            check[1] = cut;
            check[2] = late;
            return check;
        }
    }
}