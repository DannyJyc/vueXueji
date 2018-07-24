using System.Collections.Generic;

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