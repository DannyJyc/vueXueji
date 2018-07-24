using System.Collections.Generic;

namespace testxueji.Models
{
    public class Classroom
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<CoursesArranging> CoursesArrangings { get; set; }
    }
}