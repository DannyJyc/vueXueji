namespace vuexueji.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _first : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Classes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MajorsId = c.Int(nullable: false),
                        Year = c.Int(nullable: false),
                        TeacherId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Majors", t => t.MajorsId, cascadeDelete: true)
                .ForeignKey("dbo.Teachers", t => t.TeacherId, cascadeDelete: true)
                .Index(t => t.MajorsId)
                .Index(t => t.TeacherId);
            
            CreateTable(
                "dbo.CoursesArrangings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ClassesId = c.Int(nullable: false),
                        CoursesId = c.Int(nullable: false),
                        WeekArrangingId = c.Int(nullable: false),
                        WeekStart = c.Int(nullable: false),
                        WeekEnd = c.Int(nullable: false),
                        LessonsOrder = c.Int(nullable: false),
                        ClassroomId = c.Int(nullable: false),
                        WeekDays = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Classrooms", t => t.ClassroomId, cascadeDelete: true)
                .ForeignKey("dbo.Courses", t => t.CoursesId, cascadeDelete: true)
                .ForeignKey("dbo.WeekArrangings", t => t.WeekArrangingId, cascadeDelete: true)
                .ForeignKey("dbo.Classes", t => t.ClassesId, cascadeDelete: true)
                .Index(t => t.ClassesId)
                .Index(t => t.CoursesId)
                .Index(t => t.WeekArrangingId)
                .Index(t => t.ClassroomId);
            
            CreateTable(
                "dbo.Classrooms",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 4),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 20),
                        LecturerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Lecturers", t => t.LecturerId, cascadeDelete: true)
                .Index(t => t.LecturerId);
            
            CreateTable(
                "dbo.Lecturers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(nullable: false, maxLength: 15),
                        PassWord = c.String(nullable: false, maxLength: 16),
                        Name = c.String(nullable: false, maxLength: 8),
                        MajorsId = c.String(),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Exams",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CoursesArrangingId = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 20),
                        TimeStamp = c.DateTime(nullable: false),
                        StudentScore = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CoursesArrangings", t => t.CoursesArrangingId, cascadeDelete: true)
                .Index(t => t.CoursesArrangingId);
            
            CreateTable(
                "dbo.Rollcalls",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CoursesArrangingId = c.Int(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                        StudentState = c.String(),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CoursesArrangings", t => t.CoursesArrangingId, cascadeDelete: true)
                .Index(t => t.CoursesArrangingId);
            
            CreateTable(
                "dbo.WeekArrangings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Years = c.Int(nullable: false),
                        Session = c.Int(nullable: false),
                        StartWeek = c.Int(nullable: false),
                        EndWeek = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Majors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 12),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Number = c.String(maxLength: 10),
                        Name = c.String(nullable: false, maxLength: 8),
                        Sex = c.String(maxLength: 2),
                        Age = c.Int(nullable: false),
                        Phone = c.String(maxLength: 12),
                        QQ = c.String(maxLength: 12),
                        Wechat = c.String(maxLength: 30),
                        Status = c.String(maxLength: 4),
                        ClassesId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Classes", t => t.ClassesId, cascadeDelete: true)
                .Index(t => t.ClassesId);
            
            CreateTable(
                "dbo.Teachers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(nullable: false, maxLength: 15),
                        PassWord = c.String(nullable: false, maxLength: 16),
                        PowerId = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 8),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Classes", "TeacherId", "dbo.Teachers");
            DropForeignKey("dbo.Students", "ClassesId", "dbo.Classes");
            DropForeignKey("dbo.Classes", "MajorsId", "dbo.Majors");
            DropForeignKey("dbo.CoursesArrangings", "ClassesId", "dbo.Classes");
            DropForeignKey("dbo.CoursesArrangings", "WeekArrangingId", "dbo.WeekArrangings");
            DropForeignKey("dbo.Rollcalls", "CoursesArrangingId", "dbo.CoursesArrangings");
            DropForeignKey("dbo.Exams", "CoursesArrangingId", "dbo.CoursesArrangings");
            DropForeignKey("dbo.Courses", "LecturerId", "dbo.Lecturers");
            DropForeignKey("dbo.CoursesArrangings", "CoursesId", "dbo.Courses");
            DropForeignKey("dbo.CoursesArrangings", "ClassroomId", "dbo.Classrooms");
            DropIndex("dbo.Students", new[] { "ClassesId" });
            DropIndex("dbo.Rollcalls", new[] { "CoursesArrangingId" });
            DropIndex("dbo.Exams", new[] { "CoursesArrangingId" });
            DropIndex("dbo.Courses", new[] { "LecturerId" });
            DropIndex("dbo.CoursesArrangings", new[] { "ClassroomId" });
            DropIndex("dbo.CoursesArrangings", new[] { "WeekArrangingId" });
            DropIndex("dbo.CoursesArrangings", new[] { "CoursesId" });
            DropIndex("dbo.CoursesArrangings", new[] { "ClassesId" });
            DropIndex("dbo.Classes", new[] { "TeacherId" });
            DropIndex("dbo.Classes", new[] { "MajorsId" });
            DropTable("dbo.Teachers");
            DropTable("dbo.Students");
            DropTable("dbo.Majors");
            DropTable("dbo.WeekArrangings");
            DropTable("dbo.Rollcalls");
            DropTable("dbo.Exams");
            DropTable("dbo.Lecturers");
            DropTable("dbo.Courses");
            DropTable("dbo.Classrooms");
            DropTable("dbo.CoursesArrangings");
            DropTable("dbo.Classes");
        }
    }
}
