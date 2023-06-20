using BookStoreApi.Models;
using BookStoreApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStoreApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class PresensiHarianGuruController : ControllerBase
    {
        private readonly PresensiHarianGuruService _presensiHarianGuruService;

        public PresensiHarianGuruController(PresensiHarianGuruService presensiHarianGuruService)
        {
            _presensiHarianGuruService = presensiHarianGuruService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<PresensiHarianGuru>>> Get()
        {
            var presensiList = await _presensiHarianGuruService.GetAsync();
            return Ok(presensiList);
        }

        [HttpGet("{id:length(24)}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PresensiHarianGuru>> Get(string id)
        {
            var presensi = await _presensiHarianGuruService.GetAsync(id);

            if (presensi == null)
            {
                return NotFound();
            }

            return Ok(presensi);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create(PresensiHarianGuru presensi)
        {
            await _presensiHarianGuruService.CreateAsync(presensi);
            return CreatedAtAction(nameof(Get), new { id = presensi.Id }, presensi);
        }

        [HttpPut("{id:length(24)}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(string id, PresensiHarianGuru updatedPresensi)
        {
            var presensi = await _presensiHarianGuruService.GetAsync(id);

            if (presensi == null)
            {
                return NotFound();
            }

            updatedPresensi.Id = presensi.Id;
            await _presensiHarianGuruService.UpdateAsync(id, updatedPresensi);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(string id)
        {
            var presensi = await _presensiHarianGuruService.GetAsync(id);

            if (presensi == null)
            {
                return NotFound();
            }

            await _presensiHarianGuruService.DeleteAsync(id);

            return NoContent();
        }
    }
}
