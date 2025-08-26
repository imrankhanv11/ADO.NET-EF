using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_Assessment.DTO
{
    public class EmployeeOrders
    {
        public int EmployeeID { get; set; }
        public string FullName { get; set; }
        public int Orders {  get; set; }
    }
}
