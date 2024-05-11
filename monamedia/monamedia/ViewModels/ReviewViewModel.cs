using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace monamedia.ViewModels
{
    public class ReviewViewModel
    {
        public int reviewID { get; set; }
        public int ?OrderDetailId { get; set; }
        public string NameCustomer { get; set; }
        public string NameProduct { get; set; }
        public string ImageUrl { get; set; }
        public string ProductColor { get; set; }
        public int ?Star {  get; set; }
        public string CmtCustomer { get; set; }
        public string CmtStaff { get; set; }
        public DateTime ?CmtTime { get; set; }
    }
}