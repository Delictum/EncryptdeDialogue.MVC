using System;

namespace EncryptedDialogue.MVC.Models
{
    public class Dialogue
    {
        public int Id { get; set; }
        public string RecipientId { get; set; }
        public string SenderId { get; set; }
        public int FileDataId { get; set; }
        public string MessageText { get; set; }
        public DateTime DateTime { get; set; }

        public FileData FileData { get; set; }
    }
}