﻿using System;
using System.Collections;
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
        /// <summary>
        /// 返回对应学生id的总出勤率
        /// </summary>
        /// <param name="id">类型 int，StudentsID</param>
        /// <returns>对应学生的总出勤率</returns>
        public static string GrossCount(int id)
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

            return ((double)normal / gross).ToString("P");
        }

        /// <summary>
        /// 返回对应老师所教班级的出勤率
        /// </summary>
        /// <param name="id">类型 int，LecturerID</param>
        /// <returns>返回对应老师所教班级的出勤率（json字符串）</returns>
        public static string TeachersCount(int id)
        {
            var db = new XuejiContext();
            var single = db.Lectureres.SingleOrDefault(l => l.Id == id);
            var list = (from rollcall in db.Rollcalls
                        join coursesarranging in db.CoursesArrangings on rollcall.CoursesArrangingId equals coursesarranging.Id
                        join classes in db.Classeses on coursesarranging.ClassesId equals classes.Id
                        join majors in db.Majorses on classes.MajorsId equals majors.Id
                        join courses in db.Courseses on coursesarranging.CoursesId equals courses.Id
                        where rollcall.Name == single.Name
                        select new
                        {
                            ClassName = classes.Year + majors.Name,
                            CoursName = courses.Name,
                            ClassesId = classes.Id,
                            CoursesId = courses.Id
                        }).Distinct();
            var str = "[";
            foreach (var i in list)
            {
                var rollcalllist = from rollcall in db.Rollcalls
                                   join coursesarranging in db.CoursesArrangings on rollcall.CoursesArrangingId equals coursesarranging
                                       .Id
                                   where coursesarranging.ClassesId == i.ClassesId && coursesarranging.CoursesId == i.CoursesId
                                   select new
                                   {
                                       StudentState = rollcall.StudentState
                                   };
                int gross = 0;
                int normal = 0;
                foreach (var n in rollcalllist)
                {
                    var temp = JsonConvert.DeserializeObject<List<StudentState>>(n.StudentState);
                    foreach (var s in temp)
                    {
                        gross++;
                        normal += Convert.ToInt16(s.State) == 2 || Convert.ToInt16(s.State) == 1 ? 1 : 0;
                    }
                }

                var item = ((double)normal / gross).ToString("P");
                str += str == "[" ? "{\"ClassName\":\"" + i.ClassName + "\",\"CoursName\":\"" + i.CoursName + "\",\"Count\":\"" + item + "\"}" : ",{\"ClassName\":\"" + i.ClassName + "\",\"CoursName\":\"" + i.CoursName + "\",\"Count\":\"" + item + "\"}";

            }
            str += "]";
            return str;
        }

        /// <summary>
        /// 返回对应学生所上课程和对应的出勤率
        /// </summary>
        /// <param name="id">类型 int，StudentsID</param>
        /// <returns>返回对应学生id所上的课和出勤率（json字符串）</returns>
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
                                where coursesarranging.ClassesId == classesId && coursesarranging.CoursesId == coursesId
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
                str += str == "[" ? "{\"CoursesName\":\"" + CoursesName.Name + "\",\"Count\":\"" + (normal / (double)gross).ToString("P") + "\"}" : ",{\"CoursesName\":\"" + CoursesName.Name + "\",\"Count\":\"" + ((double)normal / (double)gross).ToString("P") + "\"}";

            }
            str += "]";
            return str;
        }
        /// <summary>
        /// 返回对应学生的所有考试信息
        /// </summary>
        /// <param name="id">类型 int，StudentsID</param>
        /// <returns>返回对应学生id的所有考试信息（json字符串）</returns>
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
                str += str == "[" ? "{\"TimeStamp\":\"" + i.TimeStamp + "\",\"Name\":\"" + i.Name + "\",\"CoursesName\":\"" + i.CoursesName + "\",\"Score\":\"" + Score + "\"}" : ",{\"TimeStamp\":\"" + i.TimeStamp + "\",\"Name\":\"" + i.Name + "\",\"CoursesName\":\"" + i.CoursesName + "\",\"Score\":\"" + Score + "\"}";

            }

            str += "]";
            return str;
        }

        /// <summary>
        /// 返回老师所教课程的所有考试信息
        /// </summary>
        /// <param name="id">类型 int，LecturerID<</param>
        /// <returns>返回对应Lecturerid所教课程的所有考试信息</returns>
        public static IEnumerable Exams(int id)
        {
            var db = new XuejiContext();
            var list = from exams in db.Examses
                       join coursesarranging in db.CoursesArrangings on exams.CoursesArrangingId equals coursesarranging.Id
                       join classes in db.Classeses on coursesarranging.ClassesId equals classes.Id
                       join majors in db.Majorses on classes.MajorsId equals majors.Id
                       join courses in db.Courseses on coursesarranging.CoursesId equals courses.Id
                       where courses.LecturerId == id
                       select new ExamsList()
                       {
                           ClassesId = classes.Id,
                           TimeStamp = exams.TimeStamp,
                           Name = exams.Name,
                           CoursesName = courses.Name,
                           ClassesName = classes.Year + majors.Name
                       };
            return list;
        }

        /// <summary>
        /// 返回对应学生的总出勤，旷课，迟到次数
        /// </summary>
        /// <param name="id">类型 int，StudentsID</param>
        /// <returns>对应学生的总出勤，旷课，迟到次数（int[]）</returns>
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

            int[] check = new int[3];
            check[0] = gross;
            check[1] = cut;
            check[2] = late;
            return check;
        }
    }
}