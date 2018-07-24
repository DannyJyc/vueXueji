using System.Data.Entity;

namespace testxueji.Models
{
    public class XuejiContext : DbContext
    {
        public XuejiContext() : base("name=mvcxueji")
        {
        }

        public DbSet<Students> Studentses { get; set; }
        public DbSet<Classes> Classeses { get; set; }
        public DbSet<Courses> Courseses { get; set; }
        public DbSet<Majors> Majorses { get; set; }
        public DbSet<Teachers> Teacherses { get; set; }
        public DbSet<Lecturer> Lectureres { get; set; }
        //public DbSet<Group> Groups { get; set; }
        public DbSet<Classroom> Classrooms { get; set; }
        public DbSet<CoursesArranging> CoursesArrangings { get; set; }
        public DbSet<WeekArranging> WeekArrangings { get; set; }
        public DbSet<Rollcall> Rollcalls { get; set; }
        public DbSet<Exams> Examses { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Student
            modelBuilder.Entity<Students>().Property(s => s.Number).HasMaxLength(10);
            modelBuilder.Entity<Students>().Property(s => s.Name).IsRequired().HasMaxLength(8);
            modelBuilder.Entity<Students>().Property(s => s.Sex).HasMaxLength(2);
            modelBuilder.Entity<Students>().Property(s => s.Phone).HasMaxLength(12);
            modelBuilder.Entity<Students>().Property(s => s.Qq).HasMaxLength(12);
            modelBuilder.Entity<Students>().Property(s => s.Wechat).HasMaxLength(30);
            modelBuilder.Entity<Students>().Property(s => s.Status).HasMaxLength(4);
            modelBuilder.Entity<Students>().Property(s => s.PassWord).HasMaxLength(16);//MD5/16位加密
            //Classes
            modelBuilder.Entity<Classes>().Property(c => c.Year).IsRequired();
            //Courses
            modelBuilder.Entity<Courses>().Property(co => co.Name).IsRequired().HasMaxLength(20);
            //Majors
            modelBuilder.Entity<Majors>().Property(m => m.Name).IsRequired().HasMaxLength(12);
            //Teachers
            modelBuilder.Entity<Teachers>().Property(t => t.Telephone).IsRequired().HasMaxLength(11);
            modelBuilder.Entity<Teachers>().Property(t => t.Name).IsRequired().HasMaxLength(8);
            modelBuilder.Entity<Teachers>().Property(t => t.UserName).IsRequired().HasMaxLength(15);
            modelBuilder.Entity<Teachers>().Property(t => t.PassWord).IsRequired().HasMaxLength(16);//MD5/16位加密
            //Lecturer
            modelBuilder.Entity<Lecturer>().Property(le => le.Telephone).IsRequired().HasMaxLength(11);
            modelBuilder.Entity<Lecturer>().Property(le => le.Name).IsRequired().HasMaxLength(8);
            modelBuilder.Entity<Lecturer>().Property(le => le.UserName).IsRequired().HasMaxLength(15);
            modelBuilder.Entity<Lecturer>().Property(le => le.PassWord).IsRequired().HasMaxLength(16);//MD5/16位加密
            //Group
            //modelBuilder.Entity<Group>().Property(g => g.Name).IsRequired().HasMaxLength(10);
            //modelBuilder.Entity<Group>().Property(g => g.PowerId).IsRequired();
            //CoursesArranging
            //None
            //Exams
            modelBuilder.Entity<Exams>().Property(e => e.Name).IsRequired().HasMaxLength(20);
            modelBuilder.Entity<Exams>().Property(e => e.TimeStamp).HasColumnType("datetime");
            //Classroom
            modelBuilder.Entity<Classroom>().Property(cr => cr.Name).IsRequired().HasMaxLength(4);
            //WeekArranging
            //None
            //Rollcall
            modelBuilder.Entity<Rollcall>().Property(r => r.CreateTime).HasColumnType("datetime");

            //Rule
            //modelBuilder.Entity<Group>().HasMany(g => g.Teacherses).WithRequired(t => t.Group).HasForeignKey(l => l.GroupId);
            //modelBuilder.Entity<Group>().HasMany(g => g.Lecturer).WithRequired(le => le.Group).HasForeignKey(l => l.GroupId);
            modelBuilder.Entity<Majors>().HasMany(m => m.Classeses).WithRequired(c => c.Majors).HasForeignKey(l => l.MajorsId);
            modelBuilder.Entity<Teachers>().HasMany(t => t.Classeses).WithRequired(c => c.Teachers).HasForeignKey(l => l.TeacherId);
            modelBuilder.Entity<Lecturer>().HasMany(t => t.Courseses).WithRequired(co => co.Lecturer).HasForeignKey(l => l.LecturerId);
            modelBuilder.Entity<Classroom>().HasMany(cr => cr.CoursesArrangings).WithRequired(ca => ca.Classroom).HasForeignKey(l => l.ClassroomId);
            modelBuilder.Entity<Classes>().HasMany(c => c.CoursesArrangings).WithRequired(ca => ca.Classes).HasForeignKey(l => l.ClassesId);
            modelBuilder.Entity<Courses>().HasMany(co => co.CoursesArrangings).WithRequired(ca => ca.Courses).HasForeignKey(l => l.CoursesId);
            modelBuilder.Entity<WeekArranging>().HasMany(w => w.CoursesArrangings).WithRequired(ca => ca.WeekArranging).HasForeignKey(l => l.WeekArrangingId);
            modelBuilder.Entity<CoursesArranging>().HasMany(cr => cr.Rollcalls).WithRequired(r => r.CoursesArranging).HasForeignKey(l => l.CoursesArrangingId);
            modelBuilder.Entity<CoursesArranging>().HasMany(cr => cr.Examses).WithRequired(e => e.CoursesArranging)
                .HasForeignKey(l => l.CoursesArrangingId);

        }
    }
}