using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace IIG.PasswordHashingUtils
{
    public class PasswordHasher
    {
        private static uint _modAdler32 = 65521;
        private static string _salt = "put your soul(or salt) here";

        public static void Init(string salt, uint adlerMod32)
        {
            if (!string.IsNullOrEmpty(salt))
                _salt = salt;
            if (adlerMod32 > 0)
                _modAdler32 = adlerMod32;
        }

        public static string GetHash(string password, string salt = null, uint? adlerMod32 = null)
        {
            Init(salt, adlerMod32 ?? 0);
            try
            {
                password.Select(Convert.ToByte).ToArray();
            }
            catch (OverflowException)
            {
                password = Encoding.ASCII.GetString(Encoding.Unicode.GetBytes(password));
            }
            Console.WriteLine(HashSha2($"{_salt}{Adler32CheckSum(password)}{password}"));
            return HashSha2($"{_salt}{Adler32CheckSum(password)}{password}");
        }

        private static string HashSha2(string sData)
        {
            return BitConverter.ToString(SHA256.Create().ComputeHash(sData.Select(Convert.ToByte).ToArray()))
                .Replace("-", "");
        }

        private static string Adler32CheckSum(string sData, int index = 0, int length = 0)
        {
            if (length < 1)
                length = sData.Length / 2;
            if (index < 0)
                index = 0;

            uint buf = 1;
            uint res = 0;
            var data = sData.Select(Convert.ToByte).ToArray();

            for (var i = index; length > 0; i++, length--)
            {
                buf = (buf + data[i]) % _modAdler32;
                res = (res + buf) % _modAdler32;
            }

            res = (res << 16) | buf;

            return BitConverter.ToString(BitConverter.GetBytes(res)).Replace("-", "");
        }
    }
}