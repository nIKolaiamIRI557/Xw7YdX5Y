// 代码生成时间: 2025-09-06 04:24:10
using System;
using System.Security.Cryptography;
using System.Text;

namespace PasswordTools
{
    /// <summary>
    /// Provides functionality for encrypting and decrypting passwords using Entity Framework.
    /// </summary>
    public class PasswordEncryptionDecryptionTool
    {
        private readonly string _encryptionKey;

        /// <summary>
        /// Initializes a new instance of the PasswordEncryptionDecryptionTool class.
        /// </summary>
        /// <param name="encryptionKey">The key used for encryption and decryption.</param>
        public PasswordEncryptionDecryptionTool(string encryptionKey)
        {
            _encryptionKey = encryptionKey;
        }

        /// <summary>
        /// Encrypts a password using the specified encryption key.
        /// </summary>
        /// <param name="password">The password to encrypt.</param>
        /// <returns>The encrypted password.</returns>
        public string EncryptPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentException("Password cannot be null or empty.", nameof(password));
            }

            var aes = Aes.Create();
            aes.Key = Encoding.UTF8.GetBytes(_encryptionKey);
            aes.GenerateIV();
            var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
            var memoryStream = new MemoryStream();
            var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
            var plainBytes = Encoding.UTF8.GetBytes(password);
            cryptoStream.Write(plainBytes, 0, plainBytes.Length);
            cryptoStream.FlushFinalBlock();
            var ivBytes = aes.IV;
            var encrypted = new byte[ivBytes.Length + memoryStream.ToArray().Length];
            Buffer.BlockCopy(ivBytes, 0, encrypted, 0, ivBytes.Length);
            Buffer.BlockCopy(memoryStream.ToArray(), 0, encrypted, ivBytes.Length, memoryStream.ToArray().Length);
            return Convert.ToBase64String(encrypted);
        }

        /// <summary>
        /// Decrypts a password using the specified encryption key.
        /// </summary>
        /// <param name="encryptedPassword">The encrypted password to decrypt.</param>
        /// <returns>The decrypted password.</returns>
        public string DecryptPassword(string encryptedPassword)
        {
            if (string.IsNullOrEmpty(encryptedPassword))
            {
                throw new ArgumentException("Encrypted password cannot be null or empty.", nameof(encryptedPassword));
            }

            var encrypted = Convert.FromBase64String(encryptedPassword);
            var ivBytes = new byte[16];
            var encryptedBytes = new byte[encrypted.Length - 16];
            Buffer.BlockCopy(encrypted, 0, ivBytes, 0, ivBytes.Length);
            Buffer.BlockCopy(encrypted, ivBytes.Length, encryptedBytes, 0, encryptedBytes.Length);

            var aes = Aes.Create();
            aes.Key = Encoding.UTF8.GetBytes(_encryptionKey);
            aes.IV = ivBytes;
            var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
            var memoryStream = new MemoryStream(encryptedBytes);
            var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
            var plainBytes = new byte[encryptedBytes.Length];
            cryptoStream.Read(plainBytes, 0, plainBytes.Length);
            return Encoding.UTF8.GetString(plainBytes);
        }
    }
}