using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThiThuToiec2.Models;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
namespace ThiThuToiec2.Controllers
{
    [Route("api/QuanLyBaiTest")]
    [ApiController]
    public class XuLyTestController : ControllerBase
    {
        private readonly ThiThuToiec2Context _context;
        public XuLyTestController(ThiThuToiec2Context context)
        {
            _context = context;
        }
        // GET: api/<controller>
        [HttpGet("LayDanhSachTest")]
        public ActionResult<IEnumerable<Test_>> GetDanhSachTest()
        {
            var test = from i in _context.Tests
                       select new Test_()
                       {
                           Id = i.Id,
                           TestName = i.TestName
                       };
            return test.ToList();
        }
        // GET api/<controller>/5
        [HttpGet("LayBaiTest")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<Test>> GetTest(int idTest)
        {
            var test = await _context.Tests.FindAsync(idTest);
            if (test == null)
            {
                return NotFound();
            }

            var part = from p in _context.Parts select p;
            if (part == null)
                return NotFound();

            if (idTest != null)
            {
                part = part.Where(s => s.TestID==idTest);
            }

            foreach (var p in part)
            {
                var doanvan = from d in _context.DoanVanAudioImages where d.PartID==p.ID select d;
                if (doanvan == null)
                    return NotFound();
                foreach(var d in doanvan)
                {
                    var cauhoi = from c in _context.CauHois where c.DoanVanAudioImageID == d.ID select c;
                    if (cauhoi == null)
                        return NotFound();
                    d.cauHoi = cauhoi.ToList();
                }
                p.doanVanAudioImages = doanvan.ToList();
            }
            test.part = part.ToList();
            return test;
        }
        [HttpGet("LayDanhSachPart")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<IEnumerable<Part>>> GetCauHoiPart(string partName)
        {
            var part = from p in _context.Parts select p;
            if (part == null)
                return NotFound();
            if (!String.IsNullOrEmpty(partName))
            {
                part = part.Where(s => s.PartName.Contains(partName));
                if (part.Count() == 0)
                    return NotFound();
            }
            foreach (var p in part)
            {
                var doanvan = from d in _context.DoanVanAudioImages where d.PartID == p.ID select d;
                if (doanvan == null)
                    return NotFound();
                foreach (var d in doanvan)
                {
                    var cauhoi = from c in _context.CauHois where c.DoanVanAudioImageID == d.ID select c;
                    if (cauhoi == null)
                        return NotFound();
                    d.cauHoi = cauhoi.ToList();
                }
                p.doanVanAudioImages = doanvan.ToList();
            }
           
            return await part.ToListAsync();
        }
        // POST api/<controller>
        [HttpPost("ThemBaiTest")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<Test_>> ThemTest([FromBody]Test_ value)
        {
            var test = await _context.Tests.FirstOrDefaultAsync(m => m.TestName == value.TestName);
            if (test != null)
            {
                return NotFound();
            }
            _context.Tests.Add(new Test()
            {
               // Id = value.Id,
                TestName = value.TestName
            }
            );
            _context.SaveChanges();
            return value;
        }
        [HttpPut("CapNhatBaiTest")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType] 
        public async Task<ActionResult<Test>> CapNhatTest([FromBody]Test_ value)
        {
            var test = await _context.Tests.FindAsync(value.Id);
            if (test == null)
            {
                return NotFound();
            }

            _context.Entry(test).State = EntityState.Modified;

            await _context.SaveChangesAsync();
            return test;
        }
        // PUT api/<controller>/5
        // DELETE api/<controller>/5
        [HttpDelete("XoaBaiTest")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Delete(string tenBaiTest)
        {
            var test = await _context.Tests.FirstOrDefaultAsync(m => m.TestName == tenBaiTest);
            if (test == null)
            {
                return NotFound();
            }
            var part = from p in _context.Parts select p;
            if (part == null)
                return NotFound();
            part = part.Where(s => s.TestID==test.Id);
            if (part.Count() == 0)
                return NotFound();

            foreach (var p in part)
            {
                var doanvan = from d in _context.DoanVanAudioImages where d.PartID==p.ID select d;
                if (doanvan == null)
                    return NotFound();
              
                foreach (var d in doanvan)
                {
                    var cauhoi = from c in _context.CauHois where c.DoanVanAudioImageID == d.ID select c;
                    if (cauhoi == null)
                        return NotFound();
                    foreach (var c in cauhoi)
                    {
                        _context.CauHois.Remove(c);
                    }
                    _context.DoanVanAudioImages.Remove(d);
                }
                _context.Parts.Remove(p);

            }
            _context.Tests.Remove(test);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpPost,HttpPut("TinhDiem")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<LichSuNguoiDung>> LayDapAn([FromBody]LichSuNguoiDung_ value,int IDAccount)
        {
            var test = await _context.Tests.FindAsync(value.TestID);
            var lichsu = await _context.LichSuNguoiDungs.FirstOrDefaultAsync(m => m.TestID == value.TestID);
            var tienDoNguoiDung = await _context.TienDoNguoiDungs.FirstOrDefaultAsync(m => m.AccountID == IDAccount);
            if (test == null||lichsu!=null||tienDoNguoiDung==null)
            {
                return NotFound();
            }
            int diem = 0;
            for (int i = 0; i < test.part.Count; i++)
            {
                for (int j = 0; j < test.part[i].doanVanAudioImages.Count; j++)
                {
                    for (int k = 0; k < test.part[i].doanVanAudioImages[j].cauHoi.Count; k++)
                        for (int z = 0; z < value.CauTraLoi.Count; z++)
                            if (value.CauTraLoi[z].IdCauHoi == test.part[i].doanVanAudioImages[j].cauHoi[k].Id)
                            {
                                if (value.CauTraLoi[z].DAChon == test.part[i].doanVanAudioImages[j].cauHoi[k].DADung)
                                {
                                    diem++;
                                }
                            }
                }
            }
            tienDoNguoiDung.thangDiem += diem;
            _context.LichSuNguoiDungs.Add(new LichSuNguoiDung
            {
                CauTraLoi=value.CauTraLoi,
                TestID=value.TestID,
                TienDoNguoiDungID=tienDoNguoiDung.ID,
                Time=value.Time,
                Diem=diem
            });
            await _context.SaveChangesAsync();
            _context.Entry(tienDoNguoiDung).State = EntityState.Modified;
            return await _context.LichSuNguoiDungs
                   .FirstOrDefaultAsync(m => m.TestID == value.TestID);
        }
    }
}