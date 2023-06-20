using BookStoreApi.Models;
using BookStoreApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace BookStoreApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]

    public class KelasController : ControllerBase
    {
        private readonly KelasService _kelasCollection;

    public KelasController (KelasService kelasService) => _kelasCollection = kelasService;



        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<List<Kelas>> Get() => await _kelasCollection.GetAsync();


        [HttpGet("{id}")]
        // [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Kelas>> Get(string id)
        {
            var kelas = await _kelasCollection.GetAsync(id);

            if (kelas == null)
            {
                return NotFound();
            }

            return kelas;
        }

        

        [HttpPost]
        // [Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post(Kelas newKelas)
        {
            await _kelasCollection.CreateAsync(newKelas);

            return CreatedAtAction(nameof(Get), new { id = newKelas.Id }, newKelas);
        }

        [HttpPut("{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(string id, Kelas updatedKelas)
        {
            var kelas = await _kelasCollection.GetAsync(id);

            if (kelas == null)
            {
                return NotFound();
            }

            updatedKelas.Id = kelas.Id;

            // await _kelasCollection.UpdateAsync(id, updatedKelas);

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(string id)
        {
            var kelas = await _kelasCollection.GetAsync(id);

            if (kelas == null)
            {
                return NotFound();
            }

            // await _kelasCollection.DeleteAsync(id);

            return NoContent();
        }
    }
}

