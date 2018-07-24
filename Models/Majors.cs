using System.Collections.Generic;

namespace testxueji.Models
{
    public class Majors
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Classes> Classeses { get; set; }
    }
}