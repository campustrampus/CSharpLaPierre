namespace CSharpLaPierre.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EnableMigrations : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Transactions", "Tag", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Transactions", "Tag");
        }
    }
}
