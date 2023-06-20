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
    public class PresensiMengajarController : ControllerBase
    {
        private readonly PresensiMengajarService _presensiMengajarService;

        public PresensiMengajarController(PresensiMengajarService presensiMengajarService)
        {
            _presensiMengajarService = presensiMengajarService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<PresensiMengajar>>> Get()
        {
            var presensiList = await _presensiMengajarService.GetAsync();
            return Ok(presensiList);
        }

        [HttpGet("{id:length(24)}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PresensiMengajar>> Get(string id)
        {
            var presensi = await _presensiMengajarService.GetAsync(id);

            if (presensi == null)
            {
                return NotFound();
            }

            return Ok(presensi);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create(PresensiMengajar presensi)
        {
            await _presensiMengajarService.CreateAsync(presensi);
            return CreatedAtAction(nameof(Get), new { id = presensi.Id }, presensi);
        }

        [HttpPut("{id:length(24)}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(string id, PresensiMengajar updatedPresensi)
        {
            var presensi = await _presensiMengajarService.GetAsync(id);

            if (presensi == null)
            {
                return NotFound();
            }

            updatedPresensi.Id = presensi.Id;
            await _presensiMengajarService.UpdateAsync(id, updatedPresensi);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(string id)
        {
            var presensi = await _presensiMengajarService.GetAsync(id);

            if (presensi == null)
            {
                return NotFound();
            }

            await _presensiMengajarService.DeleteAsync(id);

            return NoContent();
        }
    }
}
