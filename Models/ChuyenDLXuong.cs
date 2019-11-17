using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ThiThuToiec2.Models
{
    public class NguoiDung_
    {
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
    }
    public class Item_
    {
        public string UserName;
        public string Password;
    }
    public class Test_
    {
        public int Id { get; set; }
        public string TestName { get; set; }
    }
    public class LichSuNguoiDung_
    {
        public LichSuNguoiDung_()
        {
            CauTraLoi = new List<CauTraLoi>();
        }
        public int Id { get; set; }
        public int TestID { get; set; }
        public int Time { get; set; }
        public List<CauTraLoi> CauTraLoi { get; set; }
    }

}
