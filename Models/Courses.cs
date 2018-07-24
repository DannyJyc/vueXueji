using System.Collections.Generic;

namespace testxueji.Models
{
    public class Courses
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int LecturerId { get; set; }

        public Lecturer Lecturer { get; set; }
        public List<CoursesArranging> CoursesArrangings { get; set; }
    }

    public class CoursesLecturer
    {
        public int Id { get; set; }
        public int LecturerId { get; set; }
        public string LecturerName { get; set; }
        public string Name { get; set; }
    }
}