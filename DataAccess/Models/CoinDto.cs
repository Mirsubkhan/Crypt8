﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class CoinDto
    {
        public string Name { get; set; }
        public string Symbol { get; set; }
        public decimal MarketCap { get; set; }
        public decimal TotalSupply { get; set; }
        public decimal Price { get; set; }
    }
}
