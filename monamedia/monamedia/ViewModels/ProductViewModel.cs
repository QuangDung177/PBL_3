using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace monamedia.ViewModels
{
    public class ProductViewModel
    {
        public string ProductID { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public string Ram { get; set; }
        public string Memory { get; set; }
        public Nullable<int> Price { get; set; }
        public Nullable<int> Quantity { get; set; }
        public string Color { get; set; }
        public string CPU { get; set; }
        public string GPU { get; set; }
        public string OS { get; set; }
        public string Screen { get; set; }
        public string Pin { get; set; }
        public string Charge { get; set; }
        public string Camera { get; set; }
        public string Size { get; set; }
        public int Warrantly { get; set; }
        public string Sound { get; set; }
        public string Weight { get; set; }
        public string CT { get; set; }
        public int Year { get; set; }
    }
}