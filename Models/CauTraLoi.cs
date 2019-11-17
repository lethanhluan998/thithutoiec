using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThiThuToiec2.Models
{
    public class CauTraLoi
    {
        public int Id { get; set; }
        public int LichSuNguoiDungID { get; set; }
        public string PartName { get; set; }
        public int IdCauHoi { get; set; }
        public string DAChon { get; set; }
    }
}
