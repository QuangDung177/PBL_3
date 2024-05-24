using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace monamedia.ViewModels
{
    public class SoldProductViewModel
    {
        public string ProductID { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public string Color { get; set; }
        public string Type { get; set; }
        public int? QuantitySold { get; set; }


    }
}