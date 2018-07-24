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
        public string Qq { get; set; }
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
        public int Id { get; set; }
        public string Number { get; set; }
        public string Name { get; set; }
        public string Sex { get; set; }
        public int Age { get; set; }
        public string Phone { get; set; }
        public string Qq { get; set; }
        public string Wechat { get; set; }
        public string Status { get; set; }
        public string Classes { get; set; }
        public int ClassesId { get; set; }
    }
}