using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThiThuToiec2.Models
{
    public class DoanVanAudioImage
    {
        public DoanVanAudioImage()
        {
            cauHoi = new List<CauHoi>();
        }
        public int ID { get; set; }
        public string DoanVan { get; set; }
        public string Audio { get; set; }
        public string Image { get; set; }
        public int PartID { get; set; }
        public List<CauHoi> cauHoi { get; set; }
    }
}
