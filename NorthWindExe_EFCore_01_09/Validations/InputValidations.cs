using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EXC_NorthWind_01_09_2025
{
    public class InputValidations
    {
        public string CustomerIDCheck(string input)
        {
            while (true)
            {
                try
                {
                    if (string.IsNullOrEmpty(input) || input.Trim().Length == 0)
                    {
                        throw new Exception("CustomerID cannot be empty");
                    }
                    if (input.Trim().Length != 5)
                    {
                        throw new Exception("CustomerID Need sharp 5 char ");
                    }
                    if (input.Trim().Contains(" "))
                    {
                        throw new Exception("CustomerID cannot have whiteSpace");
                    }
                    if (Regex.IsMatch(input, @"\d"))
                    {
                        throw new Exception("CustomerID cannot be Numerical Value");
                    }
                    if (!Regex.IsMatch(input, @"^[a-zA-Z\s]+$"))
                    {
                        throw new Exception("CustomerID cannot be Symbol");
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


        public string ShipNameCheck(string input)
        {
            while (true)
            {
                try
                {
                    if (string.IsNullOrEmpty(input) || input.Trim().Length == 0)
                    {
                        throw new Exception("ShipName cannot be empty");
                    }
                    if (input.Trim().Length >= 40)
                    {
                        throw new Exception("Not more than 40 char");
                    }
                    if (Regex.IsMatch(input, @"\d"))
                    {
                        throw new Exception("ShipName  cannot be Numerical Value");
                    }
                    if (!Regex.IsMatch(input, @"^[a-zA-Z\s]+$"))
                    {
                        throw new Exception("Ship Name cannot be Symbol");
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

        public string CityCheck(string input)
        {
            while (true)
            {
                try
                {
                    if (string.IsNullOrEmpty(input) || input.Trim().Length == 0)
                    {
                        throw new Exception("City cannot be empty");
                    }
                    if (input.Trim().Length >= 15)
                    {
                        throw new Exception("Not more than 15 char");
                    }
                    if (Regex.IsMatch(input, @"\d"))
                    {
                        throw new Exception("City cannot be Numerical Value");
                    }
                    if (!Regex.IsMatch(input, @"^[a-zA-Z\s]+$"))
                    {
                        throw new Exception("City cannot be Symbol");
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

        public string CountryCheck(string input)
        {
            while (true)
            {
                try
                {
                    if (string.IsNullOrEmpty(input) || input.Trim().Length == 0)
                    {
                        throw new Exception("Contry cannot be empty");
                    }
                    if (input.Trim().Length >= 15)
                    {
                        throw new Exception("Not more than 15 char");
                    }
                    if (Regex.IsMatch(input, @"\d"))
                    {
                        throw new Exception("Country cannot be Numerical Value");
                    }
                    if (!Regex.IsMatch(input, @"^[a-zA-Z\s]+$"))
                    {
                        throw new Exception("Country cannot be Symbol");
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

        public decimal DecimalCheck(string input)
        {
            decimal value;
            while (true)
            {
                try
                {
                    value = Convert.ToDecimal(input);
                    break;
                }
                catch (FormatException)
                {
                    Console.Write("Enter a valid decimal value: ");
                    input = Console.ReadLine();
                }
                catch (OverflowException)
                {
                    Console.Write("Enter a value within the valid decimal range: ");
                    input = Console.ReadLine();
                }
            }
            return value;
        }

        
        public string AddressCheck(string input)
        {
            while (true)
            {
                try
                {
                    if (string.IsNullOrEmpty(input) || input.Trim().Length == 0)
                    {
                        throw new Exception("Address cannot be empty");
                    }
                    if (input.Trim().Length >= 60)
                    {
                        throw new Exception("Address cannot more than 60 Char ");
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
