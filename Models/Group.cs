using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace testxueji.Models
{
    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PowerId { get; set; }

        public List<Teachers> Teacherses { get; set; }
        public List<Lecturer> Lecturer { get; set; }

    }
}