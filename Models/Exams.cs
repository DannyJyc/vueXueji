using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace testxueji.Models
{
    public class Exams
    {
        public int Id { get; set; }
        public int CoursesArrangingId { get; set; }
        public string Name { get; set; }
        public DateTime TimeStamp { get; set; }
        public string StudentScore { get; set; }

        public CoursesArranging CoursesArranging { get; set; }
    }

    public class ExamsBox
    {
        public int CoursesArrangingId { get; set; }
        public string Examsname { get; set; }
        public DateTime Examstime { get; set; }
        public string StudentsId { get; set; }
        public string Score { get; set; }
    }
}