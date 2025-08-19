using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CodeFirstApproach.Validation
{
    internal class InputValidations
    {
        public int IntCheck(string input)
        {
            int value;
            while (true)
            {
                try
                {
                    value = Convert.ToInt32(input);
                    break;
                }
                catch (FormatException)
                {
                    Console.Write("Enter the valid numerical value : ");
                    input = Console.ReadLine();
                }
                catch (OverflowException)
                {
                    Console.Write("Enter the Valid Range value : ");
                    input = Console.ReadLine();
                }
            }
            return value;
        }

        public int AgeCheck(string input)
        {
            int value;
            while (true)
            {
                try
                {
                    value = Convert.ToInt32(input);
                    if (value <= 0 || value > 120)
                    {
                        throw new Exception("Age in out of range");
                    }
                    break;
                }
                catch (FormatException)
                {
                    Console.Write("Enter the valid numerical value : ");
                    input = Console.ReadLine();
                }
            }
            return value;
        }

        public string StringCheck(string input)
        {
            while (true)
            {
                try
                {
                    if (string.IsNullOrEmpty(input))
                    {
                        throw new Exception("String cannot be empty");
                    }
                    if (Regex.IsMatch(input, @"\d"))
                    {
                        throw new Exception("String cannot be Numerical Value");
                    }
                    if (!Regex.IsMatch(input, @"^[a-zA-Z\s]+$"))
                    {
                        throw new Exception("String cannot be Symbol");
                    }
                    break;
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Invalid input, {e.Message}");
                    input = Console.ReadLine();
                }

            }
            return input;
        }

    }
}
