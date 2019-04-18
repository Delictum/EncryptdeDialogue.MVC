namespace EncryptedDialogue.MVC.Models
{
    public class FileData
    {
        public int Id { get; set; }
        public byte[] ByteArrayData { get; set; }
        public string Name { get; set; }
        public long Amount { get; set; }
        public FileFormat FileFormat { get; set; }
    }
}