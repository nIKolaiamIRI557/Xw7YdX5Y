// 代码生成时间: 2025-09-05 20:47:38
using System;
using System.Security.Cryptography;
using System.Text;

namespace HashValueCalculator
{
    /// <summary>
    /// Provides functionality to calculate hash values of string inputs.
    /// </summary>
    public class HashValueCalculator
    {
        /// <summary>
        /// Calculates the SHA256 hash of the given string.
        /// </summary>
        /// <param name="input">The string to be hashed.</param>
        /// <returns>The SHA256 hash of the input string.</returns>
        public string CalculateSHA256Hash(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                throw new ArgumentException("Input string cannot be null or empty.", nameof(input));
            }

            using (SHA256 sha256 = SHA256.Create())
            {
                // Compute the hash of the input string
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));

                // Convert the byte array to a hexadecimal string
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        /// <summary>
        /// Calculates the MD5 hash of the given string.
        /// </summary>
        /// <param name="input">The string to be hashed.</param>
        /// <returns>The MD5 hash of the input string.</returns>
        public string CalculateMD5Hash(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                throw new ArgumentException("Input string cannot be null or empty.", nameof(input));
            }

            using (MD5 md5 = MD5.Create())
            {
                // Compute the hash of the input string
                byte[] bytes = md5.ComputeHash(Encoding.UTF8.GetBytes(input));

                // Convert the byte array to a hexadecimal string
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
