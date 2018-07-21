using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace testxueji.Models
{
    public class Teachers
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string PassWord { get; set; }
        public int PowerId { get; set; }
        public string Telephone { get; set; }
        public string Name { get; set; }
        public int Status { get; set; }

        public List<Classes> Classeses { get; set; }
        //public Group Group { get; set; }

        //public List<CoursesArranging> CoursesArrangings { get; set; }
    }
}