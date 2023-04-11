using UTS_DRWA.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace UTS_DRWA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GuruController : ControllerBase
    {
        private GuruContext _context;

        public GuruController(GuruContext context)
        {
            this._context = context;
        }

        // GET: api/Guru
        [HttpGet]
        public ActionResult<IEnumerable<GuruItem>> GetGuruItems()
        {
            _context = HttpContext.RequestServices.GetService(typeof(GuruContext)) as GuruContext;
            return _context.GetAllGuru();
        }

        //Get : api/Guru/{nip}
        [HttpGet("{nip}", Name = "GetGuru")]
        public ActionResult<IEnumerable<GuruItem>> GetGuruItem(String nip)
        {
            _context = HttpContext.RequestServices.GetService(typeof(GuruContext)) as GuruContext;
            return _context.GetGuru(nip);
        }

        [HttpPost]
        public ActionResult<GuruItem> AddGuru([FromForm] string rfid, [FromForm] string nip, [FromForm] string nama_guru, [FromForm] string alamat, [FromForm] int status_guru)
        {
            GuruItem KI = new GuruItem();
            KI.rfid = rfid;
            KI.nip = nip;
            KI.nama_guru = nama_guru;
            KI.alamat = alamat;
            KI.status_guru = status_guru;

            _context = HttpContext.RequestServices.GetService(typeof(GuruContext)) as GuruContext;
            return _context.AddGuru(KI);
        }
    }
}
