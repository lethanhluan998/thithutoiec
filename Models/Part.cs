using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThiThuToiec2.Models
{
    public class Part
    {
        public Part()
        {
            doanVanAudioImages = new List<DoanVanAudioImage>();
        }
        public int ID { get; set; }
        public string PartName { get; set; }
        public int TestID { get; set; }
        public List<DoanVanAudioImage> doanVanAudioImages;
    }
}
