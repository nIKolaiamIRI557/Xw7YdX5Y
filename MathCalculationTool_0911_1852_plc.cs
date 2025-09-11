// 代码生成时间: 2025-09-11 18:52:12
 * is designed to be easily maintainable and extensible.
 */

using System;

namespace MathTools
{
    public class MathCalculationTool
    {
        // Adds two numbers
        public double Add(double number1, double number2)
        {
            return number1 + number2;
        }

        // Subtracts the second number from the first
        public double Subtract(double number1, double number2)
        {
            return number1 - number2;
        }

        // Multiplies two numbers
        public double Multiply(double number1, double number2)
        {
            return number1 * number2;
        }

        // Divides the first number by the second
        public double Divide(double number1, double number2)
        {
            // Error handling for division by zero
            if (number2 == 0)
            {
                throw new DivideByZeroException("Cannot divide by zero.");
            }
            return number1 / number2;
        }

        // Calculates the square of a number
        public double Square(double number)
        {
            return Math.Pow(number, 2);
        }

        // Calculates the cube of a number
        public double Cube(double number)
        {
            return Math.Pow(number, 3);
        }

        // Calculates the nth root of a number
        public double NthRoot(double number, int n)
        {
            // Error handling for invalid n (n must be positive)
            if (n <= 0)
            {
                throw new ArgumentException("The value of n must be greater than zero.");
            }
            return Math.Pow(number, 1.0 / n);
        }
    }
}
