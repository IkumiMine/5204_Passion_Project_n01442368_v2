namespace _5204_Passion_Project_n01442368_v2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _20210301v2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Photos", "FilmID", "dbo.Films");
            DropForeignKey("dbo.Photos", "LensID", "dbo.Lenses");
            DropIndex("dbo.Photos", new[] { "FilmID" });
            DropIndex("dbo.Photos", new[] { "LensID" });
            AlterColumn("dbo.Photos", "FilmID", c => c.Int(nullable: false));
            AlterColumn("dbo.Photos", "LensID", c => c.Int(nullable: false));
            CreateIndex("dbo.Photos", "FilmID");
            CreateIndex("dbo.Photos", "LensID");
            AddForeignKey("dbo.Photos", "FilmID", "dbo.Films", "FilmID", cascadeDelete: true);
            AddForeignKey("dbo.Photos", "LensID", "dbo.Lenses", "LensID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Photos", "LensID", "dbo.Lenses");
            DropForeignKey("dbo.Photos", "FilmID", "dbo.Films");
            DropIndex("dbo.Photos", new[] { "LensID" });
            DropIndex("dbo.Photos", new[] { "FilmID" });
            AlterColumn("dbo.Photos", "LensID", c => c.Int());
            AlterColumn("dbo.Photos", "FilmID", c => c.Int());
            CreateIndex("dbo.Photos", "LensID");
            CreateIndex("dbo.Photos", "FilmID");
            AddForeignKey("dbo.Photos", "LensID", "dbo.Lenses", "LensID");
            AddForeignKey("dbo.Photos", "FilmID", "dbo.Films", "FilmID");
        }
    }
}
