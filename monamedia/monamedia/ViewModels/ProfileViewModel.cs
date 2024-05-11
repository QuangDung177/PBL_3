using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace monamedia.ViewModels
{
    public class ProfileViewModel
    {
        public int accountID { get; set; }
        public string email { get; set; }
        public string name { get; set; }
        public DateTime birhtDate { get; set; }
        public string address { get; set; }
        public string phone { get; set; }
        public bool gender { get; set; }
    }
}