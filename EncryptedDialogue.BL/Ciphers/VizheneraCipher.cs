namespace EncryptedDialogue.BL.Ciphers
{
    public class VizheneraCipher : ICipher
    {
        private const string AlphavitStart = "абвгдеёжзийклмнопрстуфхцчшщъыьэюяabcdefghijklmnopqrstuvwxyz 1234567890!@#\"#№$;%:^?&*()-_+={[]}\\|/',<.>ABCDEFGHIJKLMNOPQRSTUVWXYZАБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ";

        public string Encrypt(string text, string key)
        {
            var result = "";

            key = PreapareKey(key, text);

            for (var i = 0; i < text.Length; i++)
            {
                var index = GetIndex(text[i]);
                var res = (index + GetIndex(key[i])) % AlphavitStart.Length;
                result += AlphavitStart[res];
            }

            return result;
        }

        public string Decrypt(string text, string key)
        {
            var result = "";

            key = PreapareKey(key, text);

            for (var i = 0; i < text.Length; i++)
            {
                var index = GetIndex(text[i]);
                var res = (index - GetIndex(key[i]) + AlphavitStart.Length) % AlphavitStart.Length;
                result += AlphavitStart[res];
            }

            return result;
        }

        private static string PreapareKey(string key, string message)
        {
            var newKey = "";
            while (newKey.Length < message.Length)
            {
                newKey = newKey + key;
            }

            return newKey;
        }

        private static int GetIndex(char c)
        {
            for (var i = 0; i < AlphavitStart.Length; i++)
            {
                if (AlphavitStart[i] == c)
                    return i;
            }

            return 0;
        }
    }
}
