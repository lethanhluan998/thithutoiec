using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ThiThuToiec2.Models;
using Microsoft.AspNetCore.Http;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
namespace ThiThuToiec2.Controllers
{
    [Produces("application/json")]
    [Route("api/QuanLyNguoiDung")]
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class LoginController : ControllerBase
    {
        private readonly ThiThuToiec2Context _context;
        public LoginController(ThiThuToiec2Context context)
        {
            _context = context; 
        }
        /// <summary>
        /// Lay danh sach nguoi dung
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /Quanlynguoidung
        /// </remarks>
        /// <returns>mot danh sach nguoi dung</returns>
        /// <response code="201">Tra ve mot danh sach nguoi dung</response>
        /// <response code="400">Neu danh sach trong</response>   
        [HttpGet("LayDanhSachNguoiDung")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public ActionResult<IEnumerable<NguoiDung_>> GetDanhSachNguoiDung()
        {
            var nguoiDung = from i in _context.Accounts
                            select new NguoiDung_()
                            {
                                Id = i.Id,
                                Name = i.Name,
                                UserName = i.UserName,
                                Password = i.Password,
                                Mail = i.Mail,
                                Sdt = i.Sdt,
                                Status = i.Status
                            };
            return nguoiDung.ToList();
        }
        /// <summary>
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="mail"></param>
        /// <param name="sdt"></param>
        /// <returns></returns>
        [HttpPost("TimKiemNguoiDung")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<NguoiDung_>> TimKiemNguoiDung(string userName,string mail,int sdt)
        {
            var TaiKhoan = await _context.Accounts
               .FirstOrDefaultAsync(m => (m.UserName == userName)|| (m.Mail == mail)|| (m.Sdt == sdt));
            if (TaiKhoan == null)
            {
                return NotFound();
            }
                     var nguoiDung= new NguoiDung_()
                            {
                                Id = TaiKhoan.Id,
                                Name = TaiKhoan.Name,
                                UserName = TaiKhoan.UserName,
                                Password = TaiKhoan.Password,
                                Mail = TaiKhoan.Mail,
                                Sdt = TaiKhoan.Sdt,
                                Status = TaiKhoan.Status
                            };
            return nguoiDung;
        }
        // POST api/<controller>
        [HttpPost("DangNhap")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<NguoiDung_>> PostAccount([Bind(include: "UserName,Password")]Item_ item)
        {
            var TaiKhoan = await _context.Accounts
                .FirstOrDefaultAsync(m => (m.UserName == item.UserName)&&(m.Password==item.Password));
            if (TaiKhoan == null)
            {
                return NotFound();
            }
            var nguoiDung = new NguoiDung_()
            {
                Id = TaiKhoan.Id,
                Name = TaiKhoan.Name,
                UserName = TaiKhoan.UserName,
                Password = TaiKhoan.Password,
                Mail = TaiKhoan.Mail,
                Sdt = TaiKhoan.Sdt,
                Status = TaiKhoan.Status
            };
            return nguoiDung;
        }
        [HttpPost("DangKy")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<Account>> CreateAccount([Bind(include:"Name,UserName,Password,Mail,Sdt,Status")] NguoiDung_ nguoiDung)
        {
            var TaiKhoan = await _context.Accounts
                .FirstOrDefaultAsync(m => m.UserName == nguoiDung.UserName);
            if (TaiKhoan !=null)
            {
                return NotFound();
            }
            _context.Accounts.Add(new Account
            {
              //  Id = nguoiDung.Id,
                Name = nguoiDung.Name,
                UserName = nguoiDung.UserName,
                Password = nguoiDung.Password,
                Mail = nguoiDung.Mail,
                Sdt = nguoiDung.Sdt,
                Status = nguoiDung.Status
            }
            );
            _context.SaveChanges();
            var TaiKhoan2 = await _context.Accounts
                .FirstOrDefaultAsync(m => m.UserName == nguoiDung.UserName);
            _context.TienDoNguoiDungs.Add(new TienDoNguoiDung
            {
                AccountID=TaiKhoan2.Id,
                thangDiem=0
            });
            _context.SaveChanges();
            return TaiKhoan2;
        }
        [HttpPost("ThongTinNguoiDung")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<Account>> TTND([Bind(include: "UserName,Password")]Item_ item)
        {
            var User = await _context.Accounts
                .FirstOrDefaultAsync(m => (m.UserName == item.UserName) && (m.Password == item.Password));
            if (User == null)
            {
                return NotFound();
            }
            var tienDo = await _context.TienDoNguoiDungs.FirstOrDefaultAsync(m=>m.AccountID==User.Id);
            if (tienDo == null)
            {
                return NotFound();
            }
            User.tienDoNguoiDung = tienDo;
            
            var lichSus = from p in _context.LichSuNguoiDungs select p;
            if (lichSus == null)
            {
                return NotFound();
            }
            lichSus = lichSus.Where(s => s.TienDoNguoiDungID==tienDo.ID);

            User.tienDoNguoiDung.lichSuNguoiDung = await lichSus.ToListAsync();
            if (lichSus.Count() == 0)
                return NotFound();
            return User;
        }
        [HttpPost("ThemNguoiDung")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<Account>> ThemND([Bind(include: "Name,UserName,Password,Mail,Sdt,Status")]NguoiDung_ nguoiDung)
        {
            var TaiKhoan = await _context.Accounts
               .FirstOrDefaultAsync(m => m.UserName == nguoiDung.UserName);
            if (TaiKhoan != null)
            {
                return NotFound();
            }
            _context.Accounts.Add(new Account
            {
                //  Id = nguoiDung.Id,
                Name = nguoiDung.Name,
                UserName = nguoiDung.UserName,
                Password = nguoiDung.Password,
                Mail = nguoiDung.Mail,
                Sdt = nguoiDung.Sdt,
                Status = nguoiDung.Status
            }
            );
            _context.SaveChanges();
            var TaiKhoan2 = await _context.Accounts
                .FirstOrDefaultAsync(m => m.UserName == nguoiDung.UserName);
            _context.TienDoNguoiDungs.Add(new TienDoNguoiDung
            {
                AccountID = TaiKhoan2.Id,
                thangDiem = 0
            });
            _context.SaveChanges();
            return TaiKhoan2;
        }
        [HttpPost("LichSuNguoiDung")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<LichSuNguoiDung>> LichSuNguoiDung(int iDAccount)
        {
            var TaiKhoan2 = await _context.TienDoNguoiDungs
                   .FirstOrDefaultAsync(m => m.AccountID == iDAccount);
            if (TaiKhoan2 == null)
            {
                return NotFound();
            }
            var User = await _context.LichSuNguoiDungs
              .FirstOrDefaultAsync(m => m.TienDoNguoiDungID == TaiKhoan2.ID);
            if (User == null)
            {
                return NotFound();
            }
            return User;
        }
        // PUT api/<controller>/5
        [HttpPut("SuaThongTinNguoiDung")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<NguoiDung_>> SuaTTND(NguoiDung_ nguoiDung)
        {
            var User = await _context.Accounts.AsNoTracking()
              .FirstOrDefaultAsync(m => m.UserName == nguoiDung.UserName);
            if (User == null)
            {
                return NotFound();
            }
            _context.Entry(new Account
            {
                Id = nguoiDung.Id,
                Name = nguoiDung.Name,
                UserName = nguoiDung.UserName,
                Password = nguoiDung.Password,
                Mail = nguoiDung.Mail,
                Sdt = nguoiDung.Sdt,
                Status = nguoiDung.Status
            }).State = EntityState.Modified;
            _context.SaveChanges();
            return nguoiDung;
        }
        // DELETE api/<controller>/5
        [HttpDelete("XoaNguoiDung")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<Account>> XoaND(string taiKhoan)
        {
            var TaiKhoan = await _context.Accounts
                .FirstOrDefaultAsync(m => m.UserName == taiKhoan);
            if (TaiKhoan == null)
            {
                return NotFound();
            }
            _context.Accounts.Remove(TaiKhoan);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
