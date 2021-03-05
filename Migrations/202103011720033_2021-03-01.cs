namespace _5204_Passion_Project_n01442368_v2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _20210301 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Photos", "FilmID", "dbo.Films");
            DropForeignKey("dbo.Photos", "LensID", "dbo.Lenses");
            DropIndex("dbo.Photos", new[] { "FilmID" });
            DropIndex("dbo.Photos", new[] { "LensID" });
            AddColumn("dbo.Photos", "IsPhoto", c => c.Boolean(nullable: false));
            AddColumn("dbo.Photos", "PhotoExtension", c => c.String());
            AlterColumn("dbo.Photos", "FilmID", c => c.Int());
            AlterColumn("dbo.Photos", "LensID", c => c.Int());
            CreateIndex("dbo.Photos", "FilmID");
            CreateIndex("dbo.Photos", "LensID");
            AddForeignKey("dbo.Photos", "FilmID", "dbo.Films", "FilmID");
            AddForeignKey("dbo.Photos", "LensID", "dbo.Lenses", "LensID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Photos", "LensID", "dbo.Lenses");
            DropForeignKey("dbo.Photos", "FilmID", "dbo.Films");
            DropIndex("dbo.Photos", new[] { "LensID" });
            DropIndex("dbo.Photos", new[] { "FilmID" });
            AlterColumn("dbo.Photos", "LensID", c => c.Int(nullable: false));
            AlterColumn("dbo.Photos", "FilmID", c => c.Int(nullable: false));
            DropColumn("dbo.Photos", "PhotoExtension");
            DropColumn("dbo.Photos", "IsPhoto");
            CreateIndex("dbo.Photos", "LensID");
            CreateIndex("dbo.Photos", "FilmID");
            AddForeignKey("dbo.Photos", "LensID", "dbo.Lenses", "LensID", cascadeDelete: true);
            AddForeignKey("dbo.Photos", "FilmID", "dbo.Films", "FilmID", cascadeDelete: true);
        }
    }
}
