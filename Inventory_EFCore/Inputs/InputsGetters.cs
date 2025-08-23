using DbFirst_EFCore_01;
using EFCore_DBFirstApp.DTO;
using EFCore_DBFirstApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore_DBFirstApp
{
    public class InputsGetters
    {

        InputValidation input = new InputValidation();

        Operations op = new Operations();
        public void addProuducts()
        {

            int ProductId;
            using (var dbcontext = new InventoryContext())
            {
                var products = dbcontext.Products.ToList();
            Rechcheck:
                foreach (var item in products)
                {
                    Console.WriteLine($"ID : {item.ProductId}, Name : {item.Name}");
                }


                Console.Write("Enter the Product ID : ");
                string checkID = Console.ReadLine();
                ProductId = input.IntCheck(checkID);

                if(!products.Any(c=> c.CategoryId == ProductId))
                {
                    Console.WriteLine("ID not found Try Again");
                    goto Rechcheck;
                }
            }

            int WareHouseID;
            using(var dbcontext = new InventoryContext())
            {
                var WareHouse = dbcontext.Warehouses.ToList();
            RecheckWareHouseID:
                foreach (var item in WareHouse)
                {
                    Console.WriteLine($"ID : {item.WarehouseId}, Name : {item.Name}, Location : {item.Location}");
                }

                Console.Write("Enter the WareHouse ID : ");
                string checkID = Console.ReadLine();
                WareHouseID = input.IntCheck(checkID);

                if(!WareHouse.Any(w=> w.WarehouseId == WareHouseID))
                {
                    Console.WriteLine("WereHouse ID not Found");
                    goto RecheckWareHouseID;
                }
            }

            Console.Write("Enter the Quantity : ");
            string checkQuantity = Console.ReadLine();
            int Quantity = input.IntCheck(checkQuantity);

            int UserID;
            using( var dbcontext = new InventoryContext())
            {
                var User = dbcontext.Users.ToList();
            RecheckUserID:
                Console.Write("Enter the User ID : ");
                string checkID = Console.ReadLine();
                UserID = input.IntCheck(checkID);

                if(!User.Any(u=> u.UserId == UserID))
                {
                    Console.WriteLine("ID not found ");
                    goto RecheckUserID;
                }
            }

            AddProductsinWareHouse Prouducts = new AddProductsinWareHouse
            {
                ProductID = ProductId, WareHouseID =WareHouseID, Quantity =Quantity, UserId = UserID
            };

            op.AddProductsWareHouse(Prouducts );
        }

        public void checkStockInput()
        {
            Console.WriteLine("Available Products : ");
            int productId;
            using( var dbcontext = new InventoryContext())
            {
                var Product = dbcontext.Products.ToList();

            RecheckproudctID2:
                foreach (var item in Product)
                {
                    Console.WriteLine($"ID : {item.ProductId}, Product : {item.Name}");
                }

                Console.Write("Enter the Product ID : ");
                string checkId = Console.ReadLine();
                productId = input.IntCheck(checkId);

                if(!Product.Any(u=> u.ProductId == productId))
                {
                    Console.WriteLine("Id not found");
                    goto RecheckproudctID2;
                }
            }

            op.CheckProuductStock(productId);
        }
    }
}
