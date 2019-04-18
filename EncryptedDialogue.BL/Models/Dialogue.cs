using System;

namespace EncryptedDialogue.BL.Models
{
    public class Dialogue
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public DateTime DateTime { get; set; }
        public User RecipientUser { get; set; }
        public User SenderUser { get; set; }
    }
}
