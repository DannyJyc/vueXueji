using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using testxueji.Models;

namespace vuexueji.DAL
{
    public class LecturersDAl
    {
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