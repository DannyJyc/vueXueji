using System.Collections.Generic;
using System.Linq;
using testxueji.Models;

namespace vuexueji.DAL
{
    public class TeachersDal
    {
        /// <summary>
        /// 返回老师（teachers)的所有记录
        /// </summary>
        /// <returns>类型为IEnumerable<Teachers>的所有老师（teachers）记录</returns>
        public static IEnumerable<Teachers> List()
        {
            using (var db = new XuejiContext())
            {
                var list = db.Teacherses.Select(t => t);
                return list.ToList();
            }
        }
    }
}