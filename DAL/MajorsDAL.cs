using System.Collections.Generic;
using System.Linq;
using testxueji.Models;

namespace vuexueji.DAL
{
    public class MajorsDal
    {
        /// <summary>
        /// 列出专业（major）的所有信息
        /// </summary>
        /// <returns>以类型IEnumerable<Majors>返回专业的所有信息</returns>
        public static IEnumerable<Majors> List()
        {
            using (var db = new XuejiContext())
            {
                var list = db.Majorses.Select(m => m);
                return list.ToList();
            }
        }

        /// <summary>
        /// 为专业（major）新增一条记录
        /// </summary>
        /// <param name="m">类型 Majors 供major新增记录</param>
        public static void Add(Majors m)
        {
            using (var db = new XuejiContext())
            {
                var majors = new Majors
                {
                    Name = m.Name
                };
                db.Majorses.Add(majors);
                db.SaveChanges();
            }
        }

        /// <summary>
        /// 为专业（major）修改一条记录
        /// </summary>
        /// <param name="majors">类型 Majors中的id 供major修改一条记录</param>
        public static void Edit(Majors majors)
        {
            using (var db = new XuejiContext())
            {
                var single = db.Majorses.SingleOrDefault(m => m.Id == majors.Id);
                if (single != null) single.Name = majors.Name;
                db.SaveChanges();
            }
        }

        /// <summary>
        /// 专业（major）删除一条记录
        /// </summary>
        /// <param name="id">类型 int，MajorId 删除对应专业记录</param>
        public static void Del(int id)
        {
            using (var db = new XuejiContext())
            {
                var del = db.Majorses.SingleOrDefault(m => m.Id == id);
                if (del != null) db.Majorses.Remove(del);
                db.SaveChanges();
            }
        }
    }
}