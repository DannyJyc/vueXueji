using System.Collections.Generic;

namespace testxueji.Models
{
    public class Lecturer
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string PassWord { get; set; }
        public string Name { get; set; }
        public string Telephone { get; set; }
        public string MajorsId { get; set; }
        public int Status { get; set; }

        public List<Courses> Courseses { get; set; }
        //public Group Group { get; set; }
    }
}