using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThiThuToiec2.Models
{
    public partial class Test
    {
        public Test()
        {
            part = new List<Part>();
        }
        public int Id { get; set; }
        public string TestName { get; set; }
        public List<Part> part { get; set; }
    }
}
