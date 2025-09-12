using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OkulApi.Models;
using OkulApi.Data;
using OkulApi.Dtos;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace OkulApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OgrencilerController : ControllerBase
    {
        private readonly EokulContext _context;

        public OgrencilerController(EokulContext context)
        {
            _context = context;
        }
        

        #region Tek öğrenci getirme

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OgrenciDto>>> GetOgrenci()
        {
            var result = await _context.Ogrenciler
                .Include(o => o.OgrenciDersler)
                .ThenInclude(od => od.Ders)
                .Select(static o => new OgrenciDto
                {
                    Id = o.Id,
                    AdiSoyadi = o.AdiSoyadi,
                    Dersler = o.OgrenciDersler.Select(od => new DersDto
                    {
                        DersId = od.DersId,
                        DersAdi = od.Ders.DersAdi,
                        Not = od.Not
                    }).ToList()
                }).ToListAsync();
            return result;
        }

        #endregion

        #region Id'ye göre öğrenci getirme

        [HttpGet("{id}")]
        public IActionResult GetOgrenci(int id)
        {
            var result = _context.Ogrenciler.Find(id);
            return Ok(result);
        }

        #endregion

        #region Öğrenci ve ders ekleme

        [HttpPost("ogrenci")]
        public async Task<ActionResult<Ogrenci>> PostOgrenci(OgrenciCreateDto dto)
        {
            var ogrenci = new Ogrenci
            {
                AdiSoyadi=dto.AdiSoyadi
            };

            _context.Ogrenciler.Add(ogrenci);
            await _context.SaveChangesAsync();
            return Ok(ogrenci);
        }

        [HttpPost("ders")]
        public async Task<ActionResult<Ders>> PostDers(DersDto dto)
        {
            var drs = new Ders
            {
                DersAdi=dto.DersAdi
            };

            _context.Dersler.Add(drs);
            await _context.SaveChangesAsync();
            return Ok(drs);
        }



        #endregion

        #region Öğrenci güncelleme

        [HttpPut("{id}")]
        public IActionResult PutOgrenci(int id, OgrenciCreateDto ogrenciDto)
        {
            var result =  _context.Ogrenciler.Find(id);

            result.AdiSoyadi = ogrenciDto.AdiSoyadi;

            //result.OgrenciDersler.Clear();

            //foreach(var x in ogrenciDto.Dersler)
            //{
            //    var ders = await _context.Dersler.FirstOrDefaultAsync(d => d.DersId == x.DersId);

            //    if (ders == null)
            //    {
            //        ders = new Ders { DersAdi = x.DersAdi };
            //        _context.Dersler.Add(ders);
            //    }

            //    result.OgrenciDersler.Add(new OgrenciDers
            //    {
            //        OgrenciId = result.Id,
            //        Ders = ders,
            //    });
            //}

            _context.Update(result);
            _context.SaveChanges();
            return Ok($"Id={id} öğrencisi güncellendi");
        }

        [HttpPut("{ders}")]
        public IActionResult PutOgrenci(int ders, DersDto dersDto)
        {
            var result = _context.Dersler.Find(ders);

            result.DersAdi = dersDto.DersAdi;

            //result.OgrenciDersler.Clear();

            //foreach(var x in ogrenciDto.Dersler)
            //{
            //    var ders = await _context.Dersler.FirstOrDefaultAsync(d => d.DersId == x.DersId);

            //    if (ders == null)
            //    {
            //        ders = new Ders { DersAdi = x.DersAdi };
            //        _context.Dersler.Add(ders);
            //    }

            //    result.OgrenciDersler.Add(new OgrenciDers
            //    {
            //        OgrenciId = result.Id,
            //        Ders = ders,
            //    });
            //}

            _context.Update(result);
            _context.SaveChanges();
            return Ok($"Id={ders} Ders bilgisi güncellendi");
        }

        [HttpPut("{ogrders}")]
        public IActionResult PutOgrenci(int ogrders, OgrenciDers ogrdersDto)
        {
            var result = _context.OgrenciDersler.Find(ogrders);

            result.OgrenciId = ogrdersDto.OgrenciId;
            result.DersId = ogrdersDto.DersId;
            result.Not = ogrdersDto.Not;

            //result.OgrenciDersler.Clear();

            //foreach(var x in ogrenciDto.Dersler)
            //{
            //    var ders = await _context.Dersler.FirstOrDefaultAsync(d => d.DersId == x.DersId);

            //    if (ders == null)
            //    {
            //        ders = new Ders { DersAdi = x.DersAdi };
            //        _context.Dersler.Add(ders);
            //    }

            //    result.OgrenciDersler.Add(new OgrenciDers
            //    {
            //        OgrenciId = result.Id,
            //        Ders = ders,
            //    });
            //}

            _context.Update(result);
            _context.SaveChanges();
            return Ok($"Id={ogrders} Ders ve öğrenci bilgisi güncellendi");
        }
        #endregion

        #region Öğrenci silme

        [HttpDelete("{id}")]
        public IActionResult DelOgrenci(int id)
        {
            var result= _context.Ogrenciler.Find(id);
            _context.Ogrenciler.Remove(result);
            _context.SaveChanges();
            return Ok("Öğrenci kaydı silindi");
        }
        #endregion
    }
}
