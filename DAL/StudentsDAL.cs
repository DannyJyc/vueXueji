﻿using System.Collections.Generic;
using System.Linq;
using testxueji.Models;

namespace vuexueji.DAL
{

    public class StudentsDal
    {
        //public static string SingleName(int id)
        //{
        //    using (var db = new XuejiContext())
        //    {
        //        var list = db.Studentses.SingleOrDefault(s => s.Id == id);
        //        if (list != null) return list.Name;
        //    }

        //    return null;
        //}
        /// <summary>
        /// 未用到
        /// </summary>
        /// <param name="id">类型 int，StudentsID</param>
        /// <returns>未用到</returns>
        public static StudentsClasses Single(int id)
        {
            using (var db = new XuejiContext())
            {
                var list = from s in db.Studentses
                           join c in db.Classeses on s.ClassesId equals c.Id
                           join m in db.Majorses on s.Classes.MajorsId equals m.Id
                           where s.Id == id
                           select new StudentsClasses()
                           {
                               Id = s.Id,
                               Number = s.Number,
                               Name = s.Name,
                               Sex = s.Sex,
                               Age = s.Age,
                               Phone = s.Phone,
                               Qq = s.Qq,
                               Wechat = s.Wechat,
                               Status = s.Status,
                               Classes = c.Year + m.Name,
                               ClassesId = c.Id
                           };
                return list.SingleOrDefault();
            }

        }
        /// <summary>
        /// 返回对应班级的所有学生
        /// </summary>
        /// <param name="id">类型 int，classesID</param>
        /// <returns>对应班级类型IEnumerable<Students>的所有学生</returns>
        public static IEnumerable<Students> StudentList(int id)
        {
            using (var db = new XuejiContext())
            {
                var list = from s in db.Studentses
                           where s.ClassesId == id
                           select s;
                return list;
            }
        }
        /// <summary>
        /// 返回学籍状态正常的所有学生
        /// </summary>
        /// <returns>类型为IEnumerable<StudentsClasses>的所有学生</returns>
        public static IEnumerable<StudentsClasses> List()
        {
            using (var db = new XuejiContext())
            {
                var list = from s in db.Studentses
                           join c in db.Classeses on s.ClassesId equals c.Id
                           join m in db.Majorses on s.Classes.MajorsId equals m.Id
                           where s.Status != "退学"
                           select new StudentsClasses()
                           {
                               Id = s.Id,
                               Number = s.Number,
                               Name = s.Name,
                               Sex = s.Sex,
                               Age = s.Age,
                               Phone = s.Phone,
                               Qq = s.Qq,
                               Wechat = s.Wechat,
                               Status = s.Status,
                               Classes = c.Year + m.Name,
                               ClassesId = c.Id

                           };

                return list.ToList();
            }
        }

        /// <summary>
        /// 为students新增一条记录
        /// </summary>
        /// <param name="c">类型Students供students新增记录</param>
        public static void Add(Students c)
        {
            using (var db = new XuejiContext())
            {
                var students = new Students
                {
                    Number = c.Number,
                    Name = c.Name,
                    Sex = c.Sex,
                    Age = c.Age,
                    Phone = c.Phone,
                    Qq = c.Qq,
                    Wechat = c.Wechat,
                    Status = "正常",
                    ClassesId = c.ClassesId
                };
                db.Studentses.Add(students);
                db.SaveChanges();
            }
        }
        /// <summary>
        /// 为students修改一条记录
        /// </summary>
        /// <param name="students">类型 Students，修改内容</param>
        /// <param name="id">类型 int，所需要修改的StudentsID</param>
        public static void Edit(Students students, int id)
        {
            using (var db = new XuejiContext())
            {
                var single = db.Studentses.SingleOrDefault(s => s.Id == id);
                if (single != null)
                {
                    single.Number = students.Number;
                    single.Name = students.Name;
                    single.Sex = students.Sex;
                    single.Age = students.Age;
                    single.Phone = students.Phone;
                    single.Qq = students.Qq;
                    single.Wechat = students.Wechat;
                    single.Status = students.Status;
                    single.ClassesId = students.ClassesId;
                }

                db.SaveChanges();
            }
        }

        /// <summary>
        /// 删除一条Students
        /// </summary>
        /// <param name="id">类型 int，StudentsId</param>
        public static void Del(int id)
        {
            using (var db = new XuejiContext())
            {
                var del = db.Studentses.SingleOrDefault(s => s.Id == id);
                if (del != null) db.Studentses.Remove(del);
                db.SaveChanges();
            }
        }
    }
}
