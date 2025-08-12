using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Practice12
{
    internal class InputValidation
    {
        public string StringCheck(string input)
        {
            while (true)
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(input))
                    {
                        throw new FormatException("Input cannot be empty.");
                    }

                    if (Regex.IsMatch(input, @"\d"))
                    {
                        throw new FormatException("Input should not contain numbers.");
                    }

                    if (!Regex.IsMatch(input, @"^[a-zA-Z\s]+$"))
                    {
                        throw new FormatException("Input should not contain symbols.");
                    }

                    break;
                }
                catch (FormatException ex)
                {
                    Console.WriteLine($"Invalid input: {ex.Message} Please try again:");
                    input = Console.ReadLine();
                }
            }

            return input;

        }
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
                    Console.WriteLine("Invalid input. Please enter a valid numeric Age:");
                    input = Console.ReadLine();
                }
                catch (OverflowException)
                {
                    Console.WriteLine("The number entered is too large or too small. Try again:");
                    input = Console.ReadLine();
                }
            }
            return value;

        }
    }
}
