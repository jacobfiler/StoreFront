using StoreFrontLab.DATA.EF;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StoreFrontLab.UI.MVC.Models
{
    public class ShoppingCartViewModel
    {
        [Range(1,int.MaxValue)]
        public int Qty { get; set; }
        public Product Prod { get; set; }

        public ShoppingCartViewModel(int qty, Product productItem)
        {
            Qty = qty;
            Prod = productItem;
        }
    }
}