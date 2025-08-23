using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore_DBFirstApp.DTO
{
    public class AddProductsinWareHouse
    {
        public int ProductID { get; set; }
        public int WareHouseID { get; set; }
        public int Quantity { get; set; }
        public int UserId { get; set; }
    }
}
