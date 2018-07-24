using System.Collections;
using System.Linq;
using testxueji.Models;

namespace vuexueji.DAL
{
    public class DataProvider : IDataProvider
    {
        public IEnumerable List(string temp)
        {
            using (var db = new XuejiContext())
            {

                switch (temp)
                {

                    case "classes":
                        var classeslist = from classes in db.Classeses
                                   select classes;
                        return classeslist.ToList();
                    case "majors":
                        var majorslist = from majors in db.Majorses
                               select majors;
                        return majorslist.ToList();

                }

                return null;

            }
        }

    }
}