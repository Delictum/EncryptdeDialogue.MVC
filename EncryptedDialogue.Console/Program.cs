using EncryptedDialogue.BL.Ciphers;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Id3;
using NAudio.Wave;
using SoxSharp;

namespace EncryptedDialogue.Console
{
    class Program
    {
        public static void Main()
        {
            //var encode = RecordOver.Encode();

            //File.WriteAllBytes("2.jpg", RecordOver.Decode(encode.Item2, encode.Item1));

            var tfile = TagLib.File.Create("2.mp3");
            var title = tfile.Tag.Title;
            var duration = tfile.Properties.Duration;
            var com = tfile.Tag.Comment;
            System.Console.WriteLine("Title: {0}, duration: {1}, com: {2}", title, duration, com);

            byte[] buf;
            var fs = new FileStream("1.jpg", FileMode.Open, FileAccess.Read);
            using (var bs = new BinaryReader(fs))
            {
                buf = bs.ReadBytes((int)fs.Length);
            }

            tfile.Tag.Comment = Encoding.Default.GetString(buf);
            tfile.Save();

            var newBuf = Encoding.Default.GetBytes(tfile.Tag.Comment);
            File.WriteAllBytes("2.jpg", newBuf);
        }
    }
}
