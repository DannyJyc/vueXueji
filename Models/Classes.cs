using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace testxueji.Models
{
    public class Classes
    {
        public int Id { get; set; }
        public int MajorsId { get; set; }
        public int Year { get; set; }
        public int TeacherId { get; set; }

        public List<Students> Studentses { get; set; }
        public Majors Majors { get; set; }
        public Teachers Teachers { get; set; }
        public List<CoursesArranging> CoursesArrangings { get; set; }
    }

    public class ClassesMajorsTeachers
    {
        public int Id { get; set; }
        public string ClassName { get; set; }
        public int MajorsId { get; set; }
        public int Year { get; set; }
        public int TeacherId { get; set; }
        public string TeacherName { get; set; }
    }
}