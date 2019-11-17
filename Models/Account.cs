using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ThiThuToiec2.Models
{
    public partial class Account
    {
        public Account()
        {
            tienDoNguoiDung = new TienDoNguoiDung();
        }
        public int Id { get; set; }

        [Required]
        [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$")]
        public string Name { get; set; }

        [Required]
        [StringLength(60, MinimumLength = 6)]
        [RegularExpression(@"^[A-Z]+[a-zA-Z0-9""'\s-]*$")]
        public string UserName { get; set; }

        [Required]
        [StringLength(60, MinimumLength = 6)]
        [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$")]
        public string Password { get; set; }

        [Required]
        public string Mail { get; set; }

        public int Sdt { get; set; }
        public string Status { get; set; }
        public TienDoNguoiDung tienDoNguoiDung { get; set; }
    }
}
