using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore_DBFirstApp.DTO
{
    public class PurchaseDetialsView
    {
        public int sale_id { get; set; }
        //public DateOnly sale_Date {  get; set; }
        public string custome_name { get; set; }
        public string sold_by { get; set; }
        public string product_name { get; set; }
        public decimal total_price { get; set; }
    }
}
