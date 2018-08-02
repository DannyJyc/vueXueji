using System.Collections.Generic;
using System.Linq;
using testxueji.Models;

namespace vuexueji.DAL
{
    public class CoursesDal
    {
        //public static Courses Single(int id)
        //{
        //    using (var db = new XuejiContext())
        //    {
        //        var single = db.Courseses.SingleOrDefault(c => c.Id == id);
        //        return single;
        //    }
        //}
        /// <summary>
        /// 返回课程（courses）的所有信息
        /// </summary>
        /// <returns>类型为IEnumerable<CoursesLecturer>的所有信息</returns>
        public static IEnumerable<CoursesLecturer> List()
        {
            using (var db = new XuejiContext())
            {
                var list = from co in db.Courseses
                    join l in db.Lectureres on co.LecturerId equals l.Id
                    select new CoursesLecturer()
                    {
                        Id = co.Id,
                        LecturerId = co.LecturerId,
                        LecturerName = l.Name,
                        Name = co.Name
                    };
                return list.ToList();
            }
        }

        /// <summary>
        /// 为课程（courses）新增一条记录
        /// </summary>
        /// <param name="co">类型Courses 以供新增记录</param>
        public static void Add(Courses co)
        {
            using (var db = new XuejiContext())
            {
                var courses = new Courses
                {
                    Name = co.Name,
                    LecturerId = co.LecturerId
                };
                db.Courseses.Add(courses);
                db.SaveChanges();
            }
        }
        /// <summary>
        /// 为课程（courses）修改一条记录
        /// </summary>
        /// <param name="courses">类型Courses 中的Id供修改对应课程信息</param>
        public static void Edit(Courses courses)
        {
            using (var db = new XuejiContext())
            {
                var single = db.Courseses.SingleOrDefault(co => co.Id == courses.Id);
                if (single != null)
                {
                    single.Name = courses.Name;
                    single.LecturerId = courses.LecturerId;
                }

                db.SaveChanges();
            }
        }
        /// <summary>
        /// 删除对应id的课程信息
        /// </summary>
        /// <param name="id">类型 int，CoursesID</param>
        public static void Del(int id)
        {
            using (var db = new XuejiContext())
            {
                var del = db.Courseses.SingleOrDefault(co => co.Id == id);
                if (del != null) db.Courseses.Remove(del);
                db.SaveChanges();
            }
        }
    }
}