using System;
using System.Collections.Generic;
using System.IO;

namespace EncryptedDialogue.BL.Ciphers
{
    public static class RecordOver
    {
        private const int NumberFileTypes = 2;

        public static Tuple<long, byte[], string> Encode(byte[] tempBuf)
        {
            var directiryParent = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory);
            directiryParent = Directory.GetParent(directiryParent.FullName);

            var random = new Random();
            var fullPathClearFile = "";

            string formatName = null;
            int filesLength;
            var numberPictures = 0;
            string tempPath = null;
            switch (random.Next(NumberFileTypes))
            {
                case 0:
                    fullPathClearFile = Path.Combine(directiryParent.FullName, "Png");
                    filesLength = new DirectoryInfo(fullPathClearFile).GetFiles().Length;
                    numberPictures = random.Next(filesLength);
                    fullPathClearFile = string.Join(string.Empty, fullPathClearFile, "\\", numberPictures, ".png");
                    tempPath = string.Join(string.Empty, Path.GetTempFileName(), numberPictures, ".png");
                    formatName = "png";
                    break;
                case 1:
                    fullPathClearFile = Path.Combine(directiryParent.FullName, "Mp3");
                    filesLength = new DirectoryInfo(fullPathClearFile).GetFiles().Length;
                    numberPictures = random.Next(filesLength);
                    fullPathClearFile = string.Join(string.Empty, fullPathClearFile, "\\", numberPictures, ".mp3");
                    tempPath = string.Join(string.Empty, Path.GetTempFileName(), numberPictures, ".mp3");
                    formatName = "mp3";
                    break;
            }

            
            var fn = new FileInfo(fullPathClearFile);
            fn.CopyTo(tempPath, true);

            Stream fs = new FileStream(tempPath, FileMode.Append, FileAccess.Write);
            var startLength = fs.Length;
            using (var bw = new BinaryWriter(fs))
            {
                bw.Write(tempBuf);
            }
            fs.Close();

            byte[] buffer;
            using (var fileStream = new FileStream(tempPath, FileMode.Open, FileAccess.Read))
            {
                buffer = new byte[fileStream.Length];
                fileStream.Read(buffer, 0, (int) (fileStream.Length));
            }
            File.Delete(tempPath);

            return new Tuple<long, byte[], string>(startLength, buffer, formatName);
        }

        public static byte[] Decode(long startLength, byte[] bufBytes)
        {
            var unityLength = bufBytes.Length - startLength;

            var newBuf = new byte[unityLength];
            var j = 0;
            for (var i = startLength; i < bufBytes.Length; i++)
            {
                newBuf[j] = bufBytes[i];
                j++;
            }

            return newBuf;
        }
    }
}
