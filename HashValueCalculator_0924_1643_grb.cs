// 代码生成时间: 2025-09-24 16:43:04
// <copyright file="HashValueCalculator.cs" company="YourCompany">
// The MIT License (MIT)
// 
// Copyright (c) 2023 YourCompany
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
// </copyright>

using System;
using System.Security.Cryptography;
using System.Text;

namespace YourCompany.Utilities
{
    /// <summary>
    /// A utility class for calculating hash values.
    /// </summary>
    public class HashValueCalculator
    {
        /// <summary>
        /// Calculates the SHA256 hash of a given string.
        /// </summary>
        /// <param name="input">The input string to be hashed.</param>
        /// <returns>The SHA256 hash of the input string.</returns>
        /// <exception cref="ArgumentNullException">Thrown when input is null.</exception>
        public string CalculateSHA256Hash(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                throw new ArgumentNullException(nameof(input), "Input cannot be null or empty.");
            }

            using (SHA256 sha256 = SHA256.Create())
            {
                // Compute hash of the input string
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));

                // Convert byte array to a hexadecimal string
                StringBuilder sb = new StringBuilder();
                foreach (byte b in hashedBytes)
                {
                    sb.Append(b.ToString("x2"));
                }

                return sb.ToString();
            }
        }
    }
}
