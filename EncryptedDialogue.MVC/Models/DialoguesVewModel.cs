using System.Collections.Generic;

namespace EncryptedDialogue.MVC.Models
{
    public class DialoguesVewModel
    {
        public IEnumerable<ApplicationUser> ApplicationUsers { get; set; }
        public IEnumerable<Dialogue> Dialogues { get; set; }
        public IEnumerable<FileData> FileDatas { get; set; }
    }
}