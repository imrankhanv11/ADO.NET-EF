using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore_DBFirstApp.DTO
{
    public class CheckProductStock
    {
        public int productid { get; set; }
        public string productname { get; set; }
        public int totalStock { get; set; }
    }
}
