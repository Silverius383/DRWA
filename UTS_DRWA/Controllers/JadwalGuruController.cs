using UTS_DRWA.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace UTS_DRWA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JadwalGuruController : ControllerBase
    {
        private JadwalGuruContext _context;

        public JadwalGuruController(JadwalGuruContext context)
        {
            this._context = context;
        }

        // GET: api/jadwalguru
        [HttpGet]
        public ActionResult<IEnumerable<JadwalGuruItem>> GetMapelItems()
        {
            _context = HttpContext.RequestServices.GetService(typeof(JadwalGuruContext)) as JadwalGuruContext;
            return _context.GetAllJadwal();
        }

        //Get : api/jadwalguru/{id_mapel}
        [HttpGet("id_mapel/{id_mapel}", Name = "GetJadwalMapel")]
        public ActionResult<IEnumerable<JadwalGuruItem>> GetJadwalMapelItem(String id_mapel)
        {
            _context = HttpContext.RequestServices.GetService(typeof(JadwalGuruContext)) as JadwalGuruContext;
            return _context.GetJadwalMapel(id_mapel);
        }

        [HttpGet("nip/{nip}", Name = "GetJadwalNIP")]
        public ActionResult<IEnumerable<JadwalGuruItem>> GetJadwalNIPItem(String nip)
        {
            _context = HttpContext.RequestServices.GetService(typeof(JadwalGuruContext)) as JadwalGuruContext;
            return _context.GetJadwalNIP(nip);
        }

        [HttpPost]
        public ActionResult<JadwalGuruItem> AddJadwalGuru([FromForm] string tahun_akademik, [FromForm] string semester, [FromForm] int id_guru, [FromForm] string hari, [FromForm] int id_kelas, [FromForm] int id_mapel, [FromForm] string jam_mulai, [FromForm] string jam_selesai)
        {
            JadwalGuruItem KI = new JadwalGuruItem();
            KI.tahun_akademik = tahun_akademik;
            KI.semester = semester;
            KI.id_guru = id_guru;
            KI.hari = hari;
            KI.id_kelas = id_kelas;
            KI.id_mapel = id_mapel;
            KI.jam_mulai = jam_mulai;
            KI.jam_selesai = jam_selesai;

            _context = HttpContext.RequestServices.GetService(typeof(JadwalGuruContext)) as JadwalGuruContext;
            return _context.AddJadwalGuru(KI);
        }
    }
}
