namespace vuexueji.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class Teacherstelephone : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Lecturers", "Telephone", c => c.String(nullable: false, maxLength: 11));
            AddColumn("dbo.Teachers", "Telephone", c => c.String(nullable: false, maxLength: 11));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Teachers", "Telephone");
            DropColumn("dbo.Lecturers", "Telephone");
        }
    }
}
