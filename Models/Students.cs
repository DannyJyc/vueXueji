using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace testxueji.Models
{
    public class Students
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public string Name { get; set; }
        public string Sex { get; set; }
        public int Age { get; set; }
        public string Phone { get; set; }
        public string QQ { get; set; }
        public string Wechat { get; set; }
        public string Status { get; set; }
        public string PassWord { get; set; }
        public int ClassesId { get; set; }

        public Classes Classes { get; set; }
    }

    public class StudentsRollcall
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class StudentsClasses
    {
        public int id { get; set; }
        public string number { get; set; }
        public string name { get; set; }
        public string sex { get; set; }
        public int age { get; set; }
        public string phone { get; set; }
        public string qq { get; set; }
        public string wechat { get; set; }
        public string status { get; set; }
        public string classes { get; set; }
        public int classesId { get; set; }
    }
}