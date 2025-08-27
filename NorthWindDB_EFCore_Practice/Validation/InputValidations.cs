using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Excersice2_EFCoreNorthWind.Validation
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

        public string CatCheck(string input)
        {
            while (true)
            {
                try
                {
                    if (string.IsNullOrEmpty(input) || input.Trim().Length == 0)
                    {
                        throw new Exception("Category cannot be empty");
                    }
                    if (input.Trim().Length >= 15)
                    {
                        throw new Exception("Category more than 15 char");
                    }
                    if (Regex.IsMatch(input, @"\d"))
                    {
                        throw new Exception("Category cannot be Numerical Value");
                    }
                    if (!Regex.IsMatch(input, @"^[a-zA-Z\s]+$"))
                    {
                        throw new Exception("Category cannot be Symbol");
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

        public string ProductCheck(string input)
        {
            while (true)
            {
                try
                {
                    if (string.IsNullOrEmpty(input) || input.Trim().Length == 0)
                    {
                        throw new Exception("Product cannot be empty");
                    }
                    if (input.Trim().Length >= 40)
                    {
                        throw new Exception("Product more than 40 char");
                    }
                    if (Regex.IsMatch(input, @"\d"))
                    {
                        throw new Exception("CateProductgory cannot be Numerical Value");
                    }
                    if (!Regex.IsMatch(input, @"^[a-zA-Z\s]+$"))
                    {
                        throw new Exception("Product cannot be Symbol");
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

        public string FirstNameCheck(string input)
        {
            while (true)
            {
                try
                {
                    if (string.IsNullOrEmpty(input) || input.Trim().Length == 0)
                    {
                        throw new Exception("cannot be empty");
                    }
                    if (input.Trim().Length > 10)
                    {
                        throw new Exception("Not more than 10 char");
                    }
                    if (Regex.IsMatch(input, @"\d"))
                    {
                        throw new Exception("cannot be Numerical Value");
                    }
                    if (!Regex.IsMatch(input, @"^[a-zA-Z\s]+$"))
                    {
                        throw new Exception("cannot be Symbol");
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

        public string LastNameCheck(string input)
        {
            while (true)
            {
                try
                {
                    if (string.IsNullOrEmpty(input) || input.Trim().Length == 0)
                    {
                        throw new Exception("cannot be empty");
                    }
                    if (input.Trim().Length > 20)
                    {
                        throw new Exception("Not more than 20 char");
                    }
                    if (Regex.IsMatch(input, @"\d"))
                    {
                        throw new Exception("cannot be Numerical Value");
                    }
                    if (!Regex.IsMatch(input, @"^[a-zA-Z\s]+$"))
                    {
                        throw new Exception("cannot be Symbol");
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

        public string TitleCheck(string input)
        {
            while (true)
            {
                try
                {
                    if (string.IsNullOrEmpty(input) || input.Trim().Length == 0)
                    {
                        throw new Exception("cannot be empty");
                    }
                    if (input.Trim().Length > 30)
                    {
                        throw new Exception("Not more than 30 char");
                    }
                    if (Regex.IsMatch(input, @"\d"))
                    {
                        throw new Exception("cannot be Numerical Value");
                    }
                    if (!Regex.IsMatch(input, @"^[a-zA-Z\s]+$"))
                    {
                        throw new Exception("cannot be Symbol");
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

        public decimal DiscountCheck(string input)
        {
            decimal value;
            while (true)
            {
                try
                {
                    value = Convert.ToDecimal(input);

                    if (value < 0 || value > 1)
                    {
                        Console.Write("Enter a decimal value between 0 and 1 (e.g., 0.1 for 10%): ");
                        input = Console.ReadLine();
                        continue;
                    }

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

        public string QutityperunitCheck(string input)
        {
            while (true)
            {
                try
                {
                    if (string.IsNullOrEmpty(input) || input.Trim().Length == 0)
                    {
                        throw new Exception("cannot be empty");
                    }
                    if (input.Trim().Length >= 60)
                    {
                        throw new Exception("Cannot more than 60 Char ");
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

        public string DisCheck(string input)
        {
            while (true)
            {
                try
                {
                    if (string.IsNullOrEmpty(input) || input.Trim().Length == 0)
                    {
                        throw new Exception("Discription cannot be empty");
                    }
                    if (input.Trim().Length >= 60)
                    {
                        throw new Exception("Discription cannot more than 60 Char ");
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
