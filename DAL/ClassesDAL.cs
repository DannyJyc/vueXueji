using System.Collections.Generic;
using System.Linq;
using testxueji.Models;

namespace vuexueji.DAL
{
    public class ClassesDal
    {

        public static IEnumerable<ClassesMajorsTeachers> List()
        {
            using (var db = new XuejiContext())
            {
                var list = from c in db.Classeses
                    join m in db.Majorses on c.MajorsId equals m.Id
                    join t in db.Teacherses on c.TeacherId equals t.Id
                    select new ClassesMajorsTeachers()
                    {
                        Id = c.Id,
                        ClassName = c.Year + m.Name,
                        MajorsId = c.MajorsId,
                        Year = c.Year,
                        TeacherId = c.TeacherId,
                        TeacherName = t.Name
                    };

                return list.ToList();
            }
        }

        public static void Add(Classes c)
        {
            using (var db = new XuejiContext())
            {
                var classes = new Classes()
                {
                    Year = c.Year,
                    MajorsId = c.MajorsId,
                    TeacherId = c.TeacherId
                };
                db.Classeses.Add(classes);
                db.SaveChanges();
            }
        }

        public static void Edit(Classes classes)
        {
            using (var db = new XuejiContext())
            {
                var single = db.Classeses.SingleOrDefault(c => c.Id == classes.Id);
                if (single != null)
                {
                    single.Year = classes.Year;
                    single.MajorsId = classes.MajorsId;
                    single.TeacherId = classes.TeacherId;
                }

                db.SaveChanges();
            }
        }

        public static void Del(int id)
        {
            using (var db = new XuejiContext())
            {
                var del = db.Classeses.SingleOrDefault(c => c.Id == id);
                if (del != null) db.Classeses.Remove(del);
                db.SaveChanges();
            }
        }
    }
}