using UTS_DRWA.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace UTS_DRWA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MapelController : ControllerBase
    {
        private MapelContext _context;

        public MapelController(MapelContext context)
        {
            this._context = context;
        }

        // GET: api/Mapel
        [HttpGet]
        public ActionResult<IEnumerable<MapelItem>> GetMapelItems()
        {
            _context = HttpContext.RequestServices.GetService(typeof(MapelContext)) as MapelContext;
            return _context.GetAllMapel();
        }

        //Get : api/Mapel/{id}
        [HttpGet("{id}", Name = "GetMapel")]
        public ActionResult<IEnumerable<MapelItem>> GetMapelItem(String id)
        {
            _context = HttpContext.RequestServices.GetService(typeof(MapelContext)) as MapelContext;
            return _context.GetMapel(id);
        }

        [HttpPost]
        public ActionResult<MapelItem> AddMapel([FromForm] string nama_mapel, [FromForm] string deskripsi)
        {
            MapelItem KI = new MapelItem();
            KI.nama_mapel = nama_mapel;
            KI.deskripsi = deskripsi;

            _context = HttpContext.RequestServices.GetService(typeof(MapelContext)) as MapelContext;
            return _context.AddMapel(KI);
        }
    }
}
