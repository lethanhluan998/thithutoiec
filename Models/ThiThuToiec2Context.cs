using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThiThuToiec2.Models
{
    public class ThiThuToiec2Context : DbContext
    {
        public ThiThuToiec2Context(DbContextOptions<ThiThuToiec2Context> options)
            : base(options)
        {
        }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Part> Parts { get; set; }
        public DbSet<Test> Tests { get; set; }
        public DbSet<LichSuNguoiDung> LichSuNguoiDungs { get; set; }
        public DbSet<TienDoNguoiDung> TienDoNguoiDungs { get; set; }
        public DbSet<CauTraLoi> CauTraLois { get; set; }
        public DbSet<CauHoi> CauHois { get; set; }
        public DbSet<DoanVanAudioImage> DoanVanAudioImages { get; set; }
    }

}
