using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore_DBFirstApp.DTO
{
    public  class AddReview
    {
        public int productID { get; set; }
        public int CustomerID { get; set; }
        public int Rating { get; set; }
        public string Commands { get; set; }
        public DateOnly Date { get; set; }
    }
}
