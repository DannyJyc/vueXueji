using System.Collections.Generic;

namespace testxueji.Models
{
    public class CoursesArranging
    {
        public int Id { get; set; }
        public int ClassesId { get; set; }
        public int CoursesId { get; set; }
        public int WeekArrangingId { get; set; }
        public int WeekStart { get; set; }
        public int WeekEnd { get; set; }
        public int LessonsOrder { get; set; }
        public int ClassroomId { get; set; }
        public int WeekDays { get; set; }

        public Classroom Classroom { get; set; }
        public Classes Classes { get; set; }
        public Courses Courses { get; set; }
        public WeekArranging WeekArranging { get; set; }
        public List<Rollcall> Rollcalls { get; set; }
        public List<Exams> Examses { get; set; }
    }

    public class CoursesArrangingName
    {
        public int Id { get; set; }
        public string ClassesName { get; set; }
        public string CoursesName { get; set; }
        public string LectureresName { get; set; }
        public int WeekArranging { get; set; }
        public int WeekSession { get; set; }
        public int WeekStart { get; set; }
        public int WeekEnd { get; set; }
        public int LessonsOrder { get; set; }
        public string ClassroomName { get; set; }
        public int WeekDays { get; set; }

        public int ClassesId { get; set; }
        public int CoursesId { get; set; }
        public int WeekArrangingId { get; set; }
        public int ClassroomId { get; set; }

    }

}