using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace monamedia.ViewModels
{
    public class DiscountViewModel
    {
        [Required(ErrorMessage = "Tên đợt giảm giá là bắt buộc")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Phần trăm giảm giá là bắt buộc")]
        [Range(0, 100, ErrorMessage = "Phần trăm giảm giá phải từ 0 đến 100")]
        public int Percent { get; set; }

        [Required(ErrorMessage = "Ngày bắt đầu là bắt buộc")]
        public DateTime DateStart { get; set; }

        [Required(ErrorMessage = "Ngày kết thúc là bắt buộc")]
        public DateTime DateEnd { get; set; }

        public List<string> SelectedProductIds { get; set; }
    }
}