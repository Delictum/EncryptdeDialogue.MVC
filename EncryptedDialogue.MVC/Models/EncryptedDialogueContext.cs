using System.Data.Entity;

namespace EncryptedDialogue.MVC.Models
{
    public class EncryptedDialogueContext : DbContext
    {
        public EncryptedDialogueContext()
            : base("EncryptedDialogueContext")
        { }

        public DbSet<Dialogue> Dialogues { get; set; }
        public DbSet<FileData> FileDatas { get; set; }
    }
}