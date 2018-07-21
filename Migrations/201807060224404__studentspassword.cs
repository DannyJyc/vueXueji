namespace vuexueji.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _studentspassword : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Students", "PassWord", c => c.String(maxLength: 16));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Students", "PassWord");
        }
    }
}
