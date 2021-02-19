namespace _5204_Passion_Project_n01442368_v2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Editdisplayformat : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Photos", "DateTaken", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Photos", "DateTaken", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
        }
    }
}
