// 代码生成时间: 2025-08-14 04:30:25
using System;
using System.Collections.Generic;

namespace MathematicalCalculatorApp
{
    // 定义一个数学计算工具类
    public class MathematicalCalculator
    {
        // 求和方法
        public double Add(double a, double b)
        {
            return a + b;
        }

        // 求差方法
        public double Subtract(double a, double b)
        {
            return a - b;
        }

        // 求乘积方法
        public double Multiply(double a, double b)
        {
            return a * b;
        }

        // 求商方法
        public double Divide(double a, double b)
        {
            if (b == 0)
            {
                throw new DivideByZeroException("Cannot divide by zero.");
            }

            return a / b;
        }

        // 求幂方法
        public double Power(double baseNumber, double exponent)
        {
            return Math.Pow(baseNumber, exponent);
        }

        // 求平方根方法
        public double SquareRoot(double number)
        {
            if (number < 0)
            {
                throw new ArgumentOutOfRangeException("Cannot take the square root of a negative number.");
            }

            return Math.Sqrt(number);
        }
    }

    // 程序入口类
    public class Program
    {
        // 程序主入口点
        public static void Main(string[] args)
        {
            try
            {
                var calculator = new MathematicalCalculator();

                Console.WriteLine("Enter the first number: ");
                double number1 = double.Parse(Console.ReadLine());
                Console.WriteLine("Enter the second number: ");
                double number2 = double.Parse(Console.ReadLine());

                Console.WriteLine("Enter an operation (+, -, *, /, ^, sqrt): ");
                string operation = Console.ReadLine().ToLower();

                switch (operation)
                {
                    case "+":
                        Console.WriteLine($"Result: {calculator.Add(number1, number2)}");
                        break;
                    case "-":
                        Console.WriteLine($"Result: {calculator.Subtract(number1, number2)}");
                        break;
                    case "*":
                        Console.WriteLine($"Result: {calculator.Multiply(number1, number2)}");
                        break;
                    case "/":
                        Console.WriteLine($"Result: {calculator.Divide(number1, number2)}");
                        break;
                    case "^":
                        Console.WriteLine("Enter the exponent: ");
                        double exponent = double.Parse(Console.ReadLine());
                        Console.WriteLine($"Result: {calculator.Power(number1, exponent)}");
                        break;
                    case "sqrt":
                        Console.WriteLine($"Result: {calculator.SquareRoot(number1)}");
                        break;
                    default:
                        Console.WriteLine("Invalid operation.");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}
