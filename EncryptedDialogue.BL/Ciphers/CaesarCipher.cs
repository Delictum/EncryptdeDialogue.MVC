namespace EncryptedDialogue.BL.Ciphers
{
    public class CaesarCipher : ICipher
    {
        private const string Alphavit = "абвгдеёжзийклмнопрстуфхцчшщъыьэюяabcdefghijklmnopqrstuvwxyz 1234567890!@#\"#№$;%:^?&*()-_+={[]}\\|/',<.>ABCDEFGHIJKLMNOPQRSTUVWXYZАБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ";

        public string Encrypt(string text, string key)
        {
            var position = int.Parse(key);
            var textOut = "";

            foreach (var letter in text)
            {
                var buf = Alphavit.IndexOf(letter);
                var temp = buf + position;
                if (temp > Alphavit.Length-1)
                    temp = temp - Alphavit.Length;
                textOut += Alphavit[temp];
            }

            return textOut;
        }

        public string Decrypt(string text, string key)
        {
            var position = int.Parse(key);
            var textOut = "";

            foreach (var letter in text)
            {
                var buf = Alphavit.IndexOf(letter);
                var temp = buf - position;
                if (temp < 0)
                    temp = temp + Alphavit.Length;
                textOut += Alphavit[temp];
            }

            return textOut;
        }
    }
}
