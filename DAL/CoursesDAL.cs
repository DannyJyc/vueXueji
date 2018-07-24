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