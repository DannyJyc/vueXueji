using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace testxueji.Models
{
    public class WeekArranging
    {
        public int Id { get; set; }
        public int Years { get; set; }
        public int Session { get; set; }
        public int StartWeek { get; set; }
        public int EndWeek { get; set; }

        public List<CoursesArranging> CoursesArrangings { get; set; }
    }

    public class WeekArrangingName
    {
        public int Id { get; set; }
        public int Years { get; set; }
        public string Session { get; set; }
        public int StartWeek { get; set; }
        public int EndWeek { get; set; }
    }
}