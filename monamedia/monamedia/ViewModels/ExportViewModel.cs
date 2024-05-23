using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace monamedia.ViewModels
{
    public class ExportViewModel
    {
        public int? OrderID { get; set; }
        public int? StaffID { get; set; }
        public string NameCustomer { get; set; }
        public string NameStaff { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public DateTime? TimeOrder { get; set; }
        public string Method { get; set; }
        public int? fee { get; set; }
        public int? total { get; set; }
        public string status { get; set; }
        public DateTime? pickUpTime { get; set; }
    }
}