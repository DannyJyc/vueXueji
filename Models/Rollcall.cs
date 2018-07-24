using System;

namespace testxueji.Models
{
    public class Rollcall
    {
        public int Id { get; set; }
        public int CoursesArrangingId { get; set; }
        public DateTime CreateTime { get; set; }
        public string StudentState { get; set; }
        public string Name { get; set; }

        public CoursesArranging CoursesArranging { get; set; }
    }

    public class StudentState
    {
        public string StudentsId { get; set; }
        public string StudentsName { get; set; }
        public string State { get; set; }
    }
}