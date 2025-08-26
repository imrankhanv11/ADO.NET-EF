using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_Assessment.DTO
{
    public class CustomerOrdersCount
    {
        public string CustomerID { get; set; }
        public string CompanyName { get; set; }
        public int TotalOrders {  get; set; }
    }
}
