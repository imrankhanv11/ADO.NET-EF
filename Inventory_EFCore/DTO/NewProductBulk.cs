using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore_DBFirstApp.DTO
{
    public class NewProductBulk
    {
        public string ProductName { get; set; }
        public int CategoryId {  get; set; }
        public int Price { get; set; }
    }
}
