// 代码生成时间: 2025-08-31 10:40:22
// <copyright file="RandomNumberGenerator.cs" company="YourCompany">
//   Copyright (c) YourCompany. All rights reserved.
// </copyright>
using System;

namespace YourNamespace
{
    /// <summary>
    /// Random number generator class utilizing C# and Entity Framework framework.
    /// </summary>
    public class RandomNumberGenerator
    {
        private readonly Random _random;

        /// <summary>
        /// Initializes a new instance of the RandomNumberGenerator class.
        /// </summary>
        public RandomNumberGenerator()
        {
            // Initialize the random number generator with a seed based on the current time.
            _random = new Random();
        }

        /// <summary>
        /// Generates a random integer within a specified range.
        /// </summary>
        /// <param name="minValue">The minimum value of the range.</param>
        /// <param name="maxValue">The maximum value of the range.</param>
        /// <returns>A random integer within the specified range.</returns>
        public int GenerateRandomInt(int minValue, int maxValue)
        {
            if (minValue > maxValue)
            {
                throw new ArgumentException("Min value cannot be greater than max value.");
            }

            return _random.Next(minValue, maxValue + 1);
        }

        /// <summary>
        /// Generates a random double within a specified range.
        /// </summary>
        /// <param name="minValue">The minimum value of the range.</param>
        /// <param name="maxValue">The maximum value of the range.</param>
        /// <returns>A random double within the specified range.</returns>
        public double GenerateRandomDouble(double minValue, double maxValue)
        {
            if (minValue >= maxValue)
            {
                throw new ArgumentException("Min value cannot be greater than or equal to max value.");
            }

            return minValue + _random.NextDouble() * (maxValue - minValue);
        }

        /// <summary>
        /// Demonstrates the use of the RandomNumberGenerator class.
        /// </summary>
        public static void Main()
        {
            var rng = new RandomNumberGenerator();
            try
            {
                Console.WriteLine("Random Int (1 to 100): " + rng.GenerateRandomInt(1, 100));
                Console.WriteLine("Random Double (0.0 to 1.0): " + rng.GenerateRandomDouble(0.0, 1.0));
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }
    }
}
