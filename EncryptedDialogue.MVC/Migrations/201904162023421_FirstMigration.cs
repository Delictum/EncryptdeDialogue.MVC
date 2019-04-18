namespace EncryptedDialogue.MVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Dialogues",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RecipientId = c.String(),
                        SenderId = c.String(),
                        FileDataId = c.Int(nullable: false),
                        MessageText = c.String(),
                        DateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.FileDatas", t => t.FileDataId, cascadeDelete: true)
                .Index(t => t.FileDataId);
            
            CreateTable(
                "dbo.FileDatas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ByteArrayData = c.Binary(),
                        Name = c.String(),
                        Amount = c.Long(nullable: false),
                        Format = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Dialogues", "FileDataId", "dbo.FileDatas");
            DropIndex("dbo.Dialogues", new[] { "FileDataId" });
            DropTable("dbo.FileDatas");
            DropTable("dbo.Dialogues");
        }
    }
}
