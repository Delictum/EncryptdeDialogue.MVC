namespace EncryptedDialogue.MVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeFileFormat : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FileDatas", "FileFormat", c => c.Int(nullable: false));
            DropColumn("dbo.FileDatas", "Format");
        }
        
        public override void Down()
        {
            AddColumn("dbo.FileDatas", "Format", c => c.String());
            DropColumn("dbo.FileDatas", "FileFormat");
        }
    }
}
