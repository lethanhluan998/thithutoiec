using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThiThuToiec2.Models
{
    public class TienDoNguoiDung
    {
        public TienDoNguoiDung()
        {
            lichSuNguoiDung = new List<LichSuNguoiDung>();
        }
        public int ID { get; set; }
        public int AccountID { get; set; }
        public int thangDiem { get; set; }
        public List<LichSuNguoiDung> lichSuNguoiDung { get; set; }

    }
}
