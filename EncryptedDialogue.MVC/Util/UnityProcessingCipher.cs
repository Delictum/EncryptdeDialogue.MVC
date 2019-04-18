using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Web;
using EncryptedDialogue.BL.Ciphers;
using EncryptedDialogue.MVC.Models;

namespace EncryptedDialogue.MVC.Util
{
    public static class UnityProcessingCipher
    {
        private const string Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz_";
        private const int NumberOfCodingTypes = 2;

        public static FileData StartEncodeProcess(HttpPostedFileBase uploadFile)
        {
            byte[] byteArrayData;
            using (var binaryReader = new BinaryReader(uploadFile.InputStream))
            {
                byteArrayData = binaryReader.ReadBytes((int) uploadFile.InputStream.Length);
            }

            var startFormatPosition = uploadFile.FileName.LastIndexOf('.') + 1;
            var keyFileFormat = uploadFile.FileName.Substring(startFormatPosition, uploadFile.FileName.Length - startFormatPosition);
            var valueFileFormat = (int)Enum.Parse(typeof(FileFormat), keyFileFormat);
            var fileFormat = (FileFormat)Enum.GetValues(typeof(FileFormat)).GetValue(valueFileFormat);
            
            var fileName = RandomString();

            var fileData = new FileData
            {
                Name = fileName,
                FileFormat = fileFormat
            };
            long amount = 0;

            byte[] bytes = null;
            var random = new Random();
            switch (random.Next(NumberOfCodingTypes))
            {
                case 0:
                    fileData.Name = string.Join(".", fileData.Name, "png");
                    amount = random.Next(1, 4);
                    if (amount == 3)
                    {
                        amount = 4;
                    }

                    bytes = PngCipher.Encode((int)amount, byteArrayData);
                    break;
                case 1:
                    var encode = RecordOver.Encode(byteArrayData);
                    amount = encode.Item1;
                    bytes = encode.Item2;
                    fileData.Name = string.Join(".", fileData.Name, encode.Item3);
                    break;
            }

            fileData.ByteArrayData = bytes;
            fileData.Amount = amount;

            return fileData;
        }

        public static byte[] StartDecodeProcess(FileData fileData)
        {
            byte[] byteArrayData = null;
            if (fileData.Amount < 5)
            {
                byteArrayData = PngCipher.Decode((int)fileData.Amount, fileData.ByteArrayData);
            }
            else
            {
                byteArrayData = RecordOver.Decode(fileData.Amount, fileData.ByteArrayData);
            }

            return byteArrayData;
        }

        public static string RandomString()
        {
            var random = new Random();
            var length = random.Next(5, 20);
            var stringBuilder = new StringBuilder(length - 1);
            for (var i = 0; i < length; i++)
            {
                var position = random.Next(0, Alphabet.Length - 1);
                stringBuilder.Append(Alphabet[position]);
            }

            return stringBuilder.ToString();
        }
    }
}