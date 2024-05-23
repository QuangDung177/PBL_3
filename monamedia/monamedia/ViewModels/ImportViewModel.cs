using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace monamedia.ViewModels
{
    public class ImportViewModel
    {
        public int ImportID { get; set; }
        public int? ManagerID { get; set; }
        public string NameManager { get; set; }
        public DateTime? ImportDate { get; set; }
        public int? Total { get; set; }

    }
}