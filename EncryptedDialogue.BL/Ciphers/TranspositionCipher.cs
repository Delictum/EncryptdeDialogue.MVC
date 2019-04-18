using System;
using System.Linq;
using System.Text;

namespace EncryptedDialogue.BL.Ciphers
{
    public class TranspositionCipher : ICipher
    {
        public string Encrypt(string text, string key)
        {
            var result = new StringBuilder();
            var strKey = key.Split(' ');

            return strKey.Aggregate(result, (current, t) => current.Append(text[int.Parse(t) - 1])).ToString();
        }

        public string Decrypt(string text, string key)
        {
            var result = new StringBuilder();
            var strKey = key.Split(' ');

            for (var i = 0; i < text.Length; i++)
            for (var j = 0; j < strKey.Length; j++)
                if (int.Parse(strKey[j]) == i + 1)
                {
                    result.Append(text[j]);
                    break;
                }

            return result.ToString();
        }
    }
}
