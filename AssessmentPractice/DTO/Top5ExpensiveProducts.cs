﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_Assessment.DTO
{
    public class Top5ExpensiveProducts
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; } 
        public decimal ProductPrice { get; set; }
    }
}
