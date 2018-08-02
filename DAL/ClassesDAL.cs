using System.Collections.Generic;
using System.Linq;
using testxueji.Models;

namespace vuexueji.DAL
{
    public class ClassesDal
    {
        /// <summary>
        /// 列出class所有
        /// </summary>
        /// <returns>返回IEnumerable<ClassesMajorsTeachers>类型的class所有数据</returns>
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

        /// <summary>
        /// 为class新增一条记录
        /// </summary>
        /// <param name="c">类型为Classes 以供class新增记录</param>
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

        /// <summary>
        /// 为class修改一条记录
        /// </summary>
        /// <param name="classes">类型为Classes 中的Id以供class修改记录</param>
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
        /// <summary>
        /// 为class删除一个记录
        /// </summary>
        /// <param name="id">类型 int，以classId为索引删除一个记录</param>
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