using System;

namespace EXC_NorthWind_01_09_2025
{
    public class Program
    {
        public static async Task Main(string [] args)
        {
            // obj
            Operations one = new Operations();
            while (true)
            {
                Console.WriteLine("*--------------------------------------------------------------------------------------*");
                Console.WriteLine("|                                         #MENU                                        |");
                Console.WriteLine("*--------------------------------------------------------------------------------------*");
                Console.WriteLine("|  1.    |     employee’s full name along with all the territories                     |");
                Console.WriteLine("|  2.    |      orders in the system that do not have any related OrderDetails.        |");
                Console.WriteLine("|  3.    |     product category the highest total quantity ordered across all orders.  |");
                // not complete
                Console.WriteLine("|  4.    |     customer, calculate the average number of days between their orders.    |");
                Console.WriteLine("|  5.    |     all shippers with the total order value they handled                    |");
                Console.WriteLine("|  6.    |     all products that were never included in any order.                     |");
                Console.WriteLine("|  7.    |     Rank employees by their total sales amount in 1998.                     |");
                Console.WriteLine("|  8.    |     Generate a report of total sales per month for the year 1997 by mon     |");
                // not complete
                Console.WriteLine("|  9.    |     customer who had the longest gap between two consecutive orders.        |");
                Console.WriteLine("| 10.    |     prds where the crnt stk units on order is less than the reorder level.  |");
                // not complete
                Console.WriteLine("| 11.    |     list the top 3 other products most frequently ordered together with it. |");
                // show error
                Console.WriteLine("| 12.    |     10% discount to all orders placed by customers from Germany in 1997     |");
                Console.WriteLine("| 13.    |     Delete all orders that were never shipped (ShippedDate IS NULL          |");
                Console.WriteLine("| 14.    |     Insert a new order for an existing customer with at least 2 ord detisle |");
                Console.WriteLine("|        |     making sure EF Core correctly saves both the order and its details.     |");
                Console.WriteLine("|  0.    |     EXIT                                                                    |");
                Console.WriteLine("*--------------------------------------------------------------------------------------*");
                Console.WriteLine();
                Console.Write("Enter the Code : ");
                string Code = Console.ReadLine();

                switch (Code)
                {
                    case "1":
                        await one.EmpTerteroy();
                        break;
                    case "2":
                        await one.OrderWithoutOrderDetail();
                        break;
                    case "3":
                        one.CatWithHightProductQuntity();
                        break;
                    case "4":
                        await one.CusAndTheirAvgOrders();
                        break;
                    case "5":
                        await one.ShippersWithTotalOrders();
                        break;
                    case "6":
                        await one.ProductNotInOrders();
                        break;
                    case "7":
                        await one.RankEmp1998();
                        break;
                    case "8":
                        await one.Report1997Sale();
                        break;
                    case "9":
                        await one.CusLongGap();
                        break;
                    case "10":
                        await one.CompareUnits();
                        break;
                    case "11":
                        await one.top3Products();
                        break;
                    case "12":
                        await one.discount();
                        break;
                    case "13":
                        await one.DeleteOrders();
                        break;
                    case "14":
                        one.InsertNewOrder();
                        break;
                    case "0":
                        Console.WriteLine("Thank You");
                        return;
                    default:
                        Console.WriteLine("Try again Code not found");
                        break;
                }
            }
        }
    }
}