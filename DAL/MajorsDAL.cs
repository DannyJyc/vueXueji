using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using testxueji.Models;

namespace vuexueji.DAL
{
    public class MajorsDAL
    {
        public static IEnumerable<Majors> List()
        {
            using (var db = new XuejiContext())
            {
                var list = db.Majorses.Select(m => m);
                return list.ToList();
            }
        }

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

        public static void Edit(Majors majors)
        {
            using (var db = new XuejiContext())
            {
                var single = db.Majorses.SingleOrDefault(m => m.Id == majors.Id);
                if (single != null) single.Name = majors.Name;
                db.SaveChanges();
            }
        }

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