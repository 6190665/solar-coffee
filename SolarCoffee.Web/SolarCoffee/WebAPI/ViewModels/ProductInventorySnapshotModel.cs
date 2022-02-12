﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolarCoffee.Data.Models
{
    public class ProductInventorySnapshotModel
    {
        public int Id { get; set; }
        public DateTime SnapshotTime { get; set; }
        public int  QuantityOnHand { get; set; }
        public ProductModel Product { get; set; }
    }
}