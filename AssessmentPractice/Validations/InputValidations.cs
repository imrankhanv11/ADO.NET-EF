using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EF_Assessment.Validations
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
                    if(input.Trim().Contains(" "))
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

        public string CustomerCompanyCheck(string input)
        {
            while (true)
            {
                try
                {
                    if (string.IsNullOrEmpty(input) || input.Trim().Length == 0)
                    {
                        throw new Exception("Company Name cannot be empty");
                    }
                    if (input.Trim().Length >= 40)
                    {
                        throw new Exception("Company Name Need sharp 5 char ");
                    }
                    if (Regex.IsMatch(input, @"\d"))
                    {
                        throw new Exception("Compay Name cannot be Numerical Value");
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

        public string ContactNameCheck(string input)
        {
            while (true)
            {
                try
                {
                    if (string.IsNullOrEmpty(input) || input.Trim().Length == 0)
                    {
                        throw new Exception("ContactName cannot be empty");
                    }
                    if (input.Trim().Length >= 30)
                    {
                        throw new Exception("Not more than 30 char");
                    }
                    if (Regex.IsMatch(input, @"\d"))
                    {
                        throw new Exception("ContactName  cannot be Numerical Value");
                    }
                    if (!Regex.IsMatch(input, @"^[a-zA-Z\s]+$"))
                    {
                        throw new Exception("Contact Name cannot be Symbol");
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

        public string ContactTileCheck(string input)
        {
            while (true)
            {
                try
                {
                    if (string.IsNullOrEmpty(input) || input.Trim().Length == 0)
                    {
                        throw new Exception("Contact Title cannot be empty");
                    }
                    if (input.Trim().Length >= 30)
                    {
                        throw new Exception("Not more than 30 char");
                    }
                    if (Regex.IsMatch(input, @"\d"))
                    {
                        throw new Exception("Contact title  cannot be Numerical Value");
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

        // emp id check
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

        // update emp
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
                    if(input.Trim().Length >= 60)
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
