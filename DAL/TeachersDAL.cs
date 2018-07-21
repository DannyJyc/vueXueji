using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using testxueji.Models;

namespace vuexueji.DAL
{
    public class TeachersDAL
    {
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