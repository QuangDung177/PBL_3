using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace monamedia.ViewModels
{
    public class StaffExportViewModel
    {
        public int StaffID { get; set; }
        public string FullName { get; set; }
        public int? DeliveredQuantity { get; set; }
        public int? TotalRevenue { get; set; }
    }
}