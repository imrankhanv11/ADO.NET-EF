using EFCore_DBFirsstApp;
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

        private readonly Operations op = new Operations();
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

        public void ProductwithCatInput()
        {
            op.ProductWithCat();
        }

        public void insertReview()
        {
            Console.WriteLine("Choose the Product : ");
            int ProuductID;
            using( var dbcontext = new InventoryContext())
            {
                var prouducts = dbcontext.Products.ToList();
            ReacheckprouductID:
                foreach (var item in prouducts)
                {
                    Console.WriteLine($"{item.ProductId} : {item.Name}");
                }

                Console.Write("Enter the Product ID : ");
                string checkId = Console.ReadLine();
                ProuductID = input.IntCheck(checkId);

                if(!prouducts.Any(p=> p.ProductId == ProuductID))
                {
                    Console.WriteLine("Try Again ");
                    goto ReacheckprouductID;
                }
            }

            Console.Write("Enter the Customer ID : ");
            string checkID = Console.ReadLine();
            int CustomerID = input.IntCheck(checkID);

            Console.Write("Enter the Raing : ");
            string checkRating = Console.ReadLine();
            int Rating = input.RatingCheck(checkRating);

            Console.Write("Enter the commands : ");
            string Commads = Console.ReadLine();

            AddReview newone = new AddReview
            {
                productID = ProuductID,
                CustomerID = CustomerID,
                Rating = Rating,
                Commands = Commads,
                Date = DateOnly.FromDateTime(DateTime.Now)
            };

            op.AddReviews(newone);
        }

        public void InsertBulkProductsinput()
        {
            List<NewProductBulk> Prouducts = new List<NewProductBulk>();

            string Check;
            do
            {
                Console.Write("Enter the Product Name : ");
                string checkName = Console.ReadLine();
                string Name = input.StringCheck(checkName);

                Console.Write("Choose the Category ID : ");
                int CategoryID;
                using(var dbcontext = new InventoryContext())
                {
                    var category = dbcontext.Categories.ToList();

                CheckAgain_CategoryID:
                    foreach (var item in category)
                    {
                        Console.WriteLine($"ID : {item.CategoryId}, Category : {item.CategoryName}");
                    }

                    Console.Write("Enter the Category ID : ");
                    string checkId = Console.ReadLine();
                    CategoryID = input.IntCheck(checkId);


                    if(!category.Any(c=> c.CategoryId == CategoryID)){
                        Console.WriteLine("Id Not found Try again");
                        goto CheckAgain_CategoryID;
                    }
                }

                Console.Write("Enter the Price : ");
                string checkPrice = Console.ReadLine();
                int Price = input.IntCheck(checkPrice);

                var newProduct = new NewProductBulk
                {
                    ProductName = Name,
                    CategoryId = CategoryID,
                    Price = Price
                };

                Prouducts.Add(newProduct);

                Console.Write("Do you want to Continues (yes/no) : ");
                Check = Console.ReadLine().Trim().ToLower();
            } while (Check == "yes");

            op.BulkInsertProducts(Prouducts);
            
        }

        public void UpdatePrizeinput()
        {
            Console.Write("Choose the Category : ");
            int CategoryID;
            using (var dbcontext = new InventoryContext())
            {
                var Category = dbcontext.Categories.ToList();

            CheckCategoryID:
                foreach (var item in Category)
                {
                    Console.WriteLine($"ID : {item.CategoryId}, Category : {item.CategoryName}");
                }

                Console.Write("Enter the Category Id : ");
                CategoryID = input.IntCheck(Console.ReadLine());

                if (!Category.Any(c => c.CategoryId == CategoryID))
                {
                    Console.WriteLine("ID not found try agian");
                    goto CheckCategoryID;
                }
            }

            op.UpdatePrize(CategoryID);
        }

        public void PurchaseProduct()
        {
            List<SaleProductList> products = new List<SaleProductList>();
            string check;
            do
            {
                Console.Write("Choose the Product : ");
                int ProductID;
                decimal Prize;
                using (var dbcontext = new InventoryContext())
                {
                    var Product = dbcontext.Products.ToList();
                CheckAgain_ProductID:
                    foreach (var item in Product)
                    {
                        Console.WriteLine($"Id : {item.ProductId}, Product : {item.Name}");
                    }


                    Console.Write("Enter the Product Id : ");
                    ProductID = input.IntCheck(Console.ReadLine());

                    if (!Product.Any(p => p.ProductId == ProductID))
                    {
                        Console.WriteLine("ID not found Try Again");
                        goto CheckAgain_ProductID;
                    }
                    var product = dbcontext.Products.Find(ProductID);
                    Prize = (decimal)product.Price;
                }

                Console.Write("Enter the Quantity : ");
                int Quantity = input.IntCheck(Console.ReadLine());

                var value = new SaleProductList
                {
                    ProductId = ProductID,
                    Quantity = Quantity,
                    Price = Prize,
                };

                products.Add(value);

                Console.Write("Do you want to add new Product (yes/no) : ");
                check = input.StringCheck(Console.ReadLine().ToLower().Trim());

            } while (check == "yes");

            Console.Write("Enter the Customer ID : ");
            string CheckCustomerID = Console.ReadLine();
            int CustomerID = input.IntCheck(CheckCustomerID);

            Console.Write("Enter the UserID : ");
            string checkUserID = Console.ReadLine();
            int UserID = input.IntCheck(checkUserID);


            op.SaleProductus(products, CustomerID, UserID);
        }

        public void DapperInput()
        {
            Console.Write("Enter the Supplier ID : ");
            string checkID = Console.ReadLine();
            int SupplierID = input.IntCheck(checkID);

            op.SupplierDetials(SupplierID);
        }
    }
}
