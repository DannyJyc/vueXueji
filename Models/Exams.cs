using System;

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

    public class StudentScore
    {
        public int StudentsId { get; set; }
        public string StudentsName { get; set; }
        public int Score { get; set; }
    }

    public class ExamsBox
    {
        public int Id { get; set; }
        public int CoursesArrangingId { get; set; }
        public string Examsname { get; set; }
        public DateTime Examstime { get; set; }
        public string StudentsId { get; set; }
        public string Score { get; set; }
    }

    public class ExamsList
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime TimeStamp { get; set; }
        public string CoursesName { get; set; }
        public string ClassesName { get; set; }
        public string StudentScore { get; set; }

        public int ClassesId { get; set; }
    }
}