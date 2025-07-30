// 代码生成时间: 2025-07-31 00:57:20
using System;
using System.Security.Cryptography;
using System.Text;

namespace PasswordEncryptionDecryption
{
    /// <summary>
    /// A utility class for encrypting and decrypting passwords using AES.
    /// </summary>
    public class PasswordUtil
    {
        private const string EncryptionKey = "your-encryption-key"; // Replace with your own key
        private const int KeySize = 256;
        private const int HashSize = 256;

        /// <summary>
        /// Encrypts the given password.
        /// </summary>
        /// <param name="password">The password to encrypt.</param>
        /// <returns>The encrypted password.</returns>
        public static string EncryptPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException("Password cannot be null or whitespace.", nameof(password));
            }

            using (Aes aes = Aes.Create())
            {
                aes.KeySize = KeySize;
                aes.BlockSize = 128;
                aes.Key = Encoding.UTF8.GetBytes(EncryptionKey.Substring(0, 32)); // Ensure the key is 32 bytes long
                aes.IV = new byte[16] { 0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x0A, 0x0B, 0x0C, 0x0D, 0x0E, 0x0F };
                using (ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV))
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                        {
                            using (StreamWriter sw = new StreamWriter(cs))
                            {
                                sw.WriteLine(password);
                            }
                        }
                        return Convert.ToBase64String(ms.ToArray());
                    }
                }
            }
        }

        /// <summary>
        /// Decrypts the given encrypted password.
        /// </summary>
        /// <param name="encryptedPassword">The encrypted password to decrypt.</param>
        /// <returns>The decrypted password.</returns>
        public static string DecryptPassword(string encryptedPassword)
        {
            if (string.IsNullOrWhiteSpace(encryptedPassword))
            {
                throw new ArgumentException("Encrypted password cannot be null or whitespace.", nameof(encryptedPassword));
            }

            using (Aes aes = Aes.Create())
            {
                aes.KeySize = KeySize;
                aes.BlockSize = 128;
                aes.Key = Encoding.UTF8.GetBytes(EncryptionKey.Substring(0, 32)); // Ensure the key is 32 bytes long
                aes.IV = new byte[16] { 0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x0A, 0x0B, 0x0C, 0x0D, 0x0E, 0x0F };
                using (ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV))
                {
                    using (MemoryStream ms = new MemoryStream(Convert.FromBase64String(encryptedPassword)))
                    {
                        using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                        {
                            using (StreamReader sr = new StreamReader(cs))
                            {
                                return sr.ReadLine();
                            }
                        }
                    }
                }
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string password = "your-password";
                string encrypted = PasswordUtil.EncryptPassword(password);
                Console.WriteLine($"Encrypted Password: {encrypted}");

                string decrypted = PasswordUtil.DecryptPassword(encrypted);
                Console.WriteLine($"Decrypted Password: {decrypted}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}