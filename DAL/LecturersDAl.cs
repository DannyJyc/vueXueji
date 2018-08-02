using System.Collections.Generic;
using System.Linq;
using testxueji.Models;

namespace vuexueji.DAL
{
    public class LecturersDAl
    {
        /// <summary>
        /// 返回所有的讲师信息
        /// </summary>
        /// <returns>以类型IEnumerable<Lecturer>的格式返回讲师的所有信息</returns>
        public static IEnumerable<Lecturer> List()
        {
            using (var db = new XuejiContext())
            {
                var list = db.Lectureres.Select(l => l);
                return list.ToList();
            }
        }
    }
}