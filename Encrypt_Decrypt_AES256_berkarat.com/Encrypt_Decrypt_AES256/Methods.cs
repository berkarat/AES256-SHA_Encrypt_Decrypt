using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Encrypt_Decrypt_AES256
{
    public static class Methods
    {




        public static string EncryptString (string plainText, byte[] key, byte[] iv)
        {

            Aes encryptor = Aes.Create ();

            encryptor.Mode=CipherMode.CBC;
            //   encryptor.KeySize = 256;
            //encryptor.BlockSize = 128;
            //  encryptor.Padding = PaddingMode.Zeros;

            // Set key and IV
            encryptor.Key=key;
            encryptor.IV=iv;


            MemoryStream memoryStream = new MemoryStream ();


            ICryptoTransform aesEncryptor = encryptor.CreateEncryptor ();

            CryptoStream cryptoStream = new CryptoStream (memoryStream, aesEncryptor, CryptoStreamMode.Write);

            byte[] plainBytes = Encoding.ASCII.GetBytes (plainText);

            cryptoStream.Write (plainBytes, 0, plainBytes.Length);

            cryptoStream.FlushFinalBlock ();

            byte[] cipherBytes = memoryStream.ToArray ();

            memoryStream.Close ();
            cryptoStream.Close ();

            string cipherText = Convert.ToBase64String (cipherBytes, 0, cipherBytes.Length);

            return cipherText;
        }

        public static string DecryptString (string cipherText, byte[] key, byte[] iv)
        {

            Aes encryptor = Aes.Create ();

            encryptor.Mode=CipherMode.CBC;
            // encryptor.KeySize = 256;
            //encryptor.BlockSize = 128;
            //  encryptor.Padding = PaddingMode.Zeros;

            // Set key and IV
            encryptor.Key=key;
            encryptor.IV=iv;


            MemoryStream memoryStream = new MemoryStream ();

            ICryptoTransform aesDecryptor = encryptor.CreateDecryptor ();

            CryptoStream cryptoStream = new CryptoStream (memoryStream, aesDecryptor, CryptoStreamMode.Write);

            string plainText = String.Empty;

            try
            {
                byte[] cipherBytes = Convert.FromBase64String (cipherText);

                cryptoStream.Write (cipherBytes, 0, cipherBytes.Length);

                cryptoStream.FlushFinalBlock ();

                byte[] plainBytes = memoryStream.ToArray ();

                plainText=Encoding.ASCII.GetString (plainBytes, 0, plainBytes.Length);
            }
            finally
            {
                memoryStream.Close ();
                cryptoStream.Close ();
            }

            // Return the decrypted data as a string
            return plainText;
        }
        public static string CreateSignature (string clearpwd, int x)
        {
            using (var sha = SHA256.Create ())
            {
                var computedHash = sha.ComputeHash (Encoding.Unicode.GetBytes (clearpwd+RandomString (x)));
                return Convert.ToBase64String (computedHash);
            }
        }
        private static Random random = new Random ();
        public static string RandomString (int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string (Enumerable.Repeat (chars, length)
              .Select (s => s[random.Next (s.Length)]).ToArray ());
        }





    }
}
