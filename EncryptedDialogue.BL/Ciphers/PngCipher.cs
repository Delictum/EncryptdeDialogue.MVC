using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace EncryptedDialogue.BL.Ciphers
{
    public static class PngCipher
    {
        public const int Int8BitsCount = 8;
        public const int Int32BitsCount = 32;

        public static byte[] Encode(int bitsUsage, byte[] bytesEncodedFile)
        {
            var directiryParent = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory);
            directiryParent = Directory.GetParent(directiryParent.FullName);

            var fullPathClearFile = Path.Combine(directiryParent.FullName, "Png");
            var filesLength = new DirectoryInfo(fullPathClearFile).GetFiles().Length;

            var random = new Random();
            var numberPictures = random.Next(filesLength);
            fullPathClearFile = string.Join(string.Empty, fullPathClearFile, "\\", numberPictures, ".png");

            string[] args = { "/c", fullPathClearFile };

            return StartProcess(args, bytesEncodedFile, bitsUsage);
        }

        public static byte[] Decode(int bitsUsage, byte[] buffer)
        {
            string[] args = { "/d" };

            return StartProcess(args, buffer, bitsUsage);
        }

        public static byte[] StartProcess(string[] args, byte[] buffer, int bitsUsage)
        {
            if (args.Length < 1)
            {
                return null;
            }

            const StringComparison sc = StringComparison.CurrentCultureIgnoreCase;

            if (args[0].Equals("/c", sc))
            {
                #region ~ encode ~

                if (!File.Exists(args[1]))
                {
                    return null;
                }

                if (!IsValidBitsUsageSize(bitsUsage))
                {
                    throw new ArgumentOutOfRangeException();
                }

                using (var bmp = (Bitmap) Image.FromFile(args[1]))
                {
                    var bd = bmp.LockBits(
                        new Rectangle(0, 0, bmp.Width, bmp.Height),
                        ImageLockMode.ReadWrite,
                        bmp.PixelFormat
                    );

                    var rgb = new byte[bd.Stride * bd.Height];
                    Marshal.Copy(bd.Scan0, rgb, 0, rgb.Length);

                    EncodeDataBitsToImage(buffer, rgb, bitsUsage);

                    Marshal.Copy(rgb, 0, bd.Scan0, rgb.Length);
                    bmp.UnlockBits(bd);

                    var temp = Path.GetTempFileName() + ".png";
                    bmp.Save(temp, ImageFormat.Png);

                    byte[] tempBuf;
                    Stream fs = new FileStream(temp, FileMode.Open, FileAccess.Read);
                    using (var binaryReader = new BinaryReader(fs))
                    {
                        tempBuf = binaryReader.ReadBytes((int) fs.Length);
                    }

                    fs.Close();

                    return tempBuf;
                }

                #endregion
            }

            if (!args[0].Equals("/d", sc))
            {
                return null;
            }

            #region ~ decode ~
            if (!IsValidBitsUsageSize(bitsUsage))
            {
                throw new ArgumentOutOfRangeException();
            }

            using (var bmp = new Bitmap(new MemoryStream(buffer)))
            {
                var bd = bmp.LockBits(
                    new Rectangle(0, 0, bmp.Width, bmp.Height),
                    ImageLockMode.ReadOnly,
                    bmp.PixelFormat
                );

                var rgb = new byte[bd.Stride * bd.Height];
                Marshal.Copy(bd.Scan0, rgb, 0, rgb.Length);

                var processingText = DecodeImageBitsToString(rgb, bitsUsage);

                bmp.UnlockBits(bd);
                return processingText;
            }
            #endregion
        }

        public static void EncodeDataBitsToImage(IReadOnlyList<byte> text, byte[] image, int bitsUsage)
        {
            if (!IsValidBitsUsageSize(bitsUsage))
                throw new ArgumentOutOfRangeException();
            if (image == null)
                throw new NullReferenceException();
            if (text == null)
                throw new NullReferenceException();

            if (text.Count * (Int8BitsCount / bitsUsage) > image.Length)
                throw new OutOfMemoryException();

            WriteLength(image, text.Count, bitsUsage);

            int i;           
            int j;                 
            var k = Int32BitsCount / bitsUsage; 

            var mask0 = (byte)(~0 << bitsUsage);
            var mask1 = (byte)~mask0;

            for (i = 0; i < text.Count; ++i)
                for (j = 0; j < Int8BitsCount; j += bitsUsage)
                {
                    image[k] &= mask0;
                    image[k] |= (byte)((text[i] & (mask1 << j)) >> j);
                    k++;
                }
        }

        public static byte[] DecodeImageBitsToString(byte[] image, int bitsUsage)
        {
            if (!IsValidBitsUsageSize(bitsUsage))
                throw new ArgumentOutOfRangeException();
            if (image == null)
                throw new NullReferenceException();

            var length = ReadLength(image, bitsUsage) + 1;
            var buff = new byte[length];

            int i;              
            int j;                  
            var k = Int32BitsCount / bitsUsage; 

            var mask0 = (byte)~(~0 << bitsUsage);

            for (i = 0; i < length; ++i)
                for (j = 0; j < Int8BitsCount; j += bitsUsage)
                {
                    buff[i] |= (byte)((image[k] & mask0) << j);
                    k++;
                }

            return buff;
        }

        public static bool IsValidBitsUsageSize(int bitsUsage)
        {
            return !(((bitsUsage & (bitsUsage - 1)) != 0) ||
                (bitsUsage > 4 || bitsUsage < 1));
        }

        public static void WriteLength(IList<byte> buff, int length, int bitsUsage)
        {
            var mask0 = (byte)(~0 << bitsUsage);
            var mask1 = (byte)(~mask0);

            for (var i = 0; i < Int32BitsCount / bitsUsage; ++i)
            {
                buff[i] &= mask0;
                buff[i] |= (byte)((length & (mask1 << i * bitsUsage)) >> i * bitsUsage);
            }
        }
        public static int ReadLength(IReadOnlyList<byte> buff, int bitsUsage)
        {
            var length = 0;
            var mask0 = (byte)(~(~0 << bitsUsage));

            for (var i = 0; i < Int32BitsCount / bitsUsage; ++i)
                length |= (buff[i] & mask0) << i * bitsUsage;

            return length;
        }
    }
}
