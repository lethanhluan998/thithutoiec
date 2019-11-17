using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThiThuToiec2.Models
{
    public class LichSuNguoiDung
    {
        public LichSuNguoiDung()
        {
            CauTraLoi = new List<CauTraLoi>();
        }
        public int Id { get; set; }
        public int TienDoNguoiDungID { get; set; }
        public int TestID { get; set; }
        public int Diem { get; set; }
        public int Time { get; set; }
        public List<CauTraLoi> CauTraLoi { get; set; }
    }
}
