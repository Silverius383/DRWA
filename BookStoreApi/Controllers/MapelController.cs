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
    public class MapelController : ControllerBase
    {
        private readonly MapelService _mapelService;

        public MapelController(MapelService mapelService)
        {
            _mapelService = mapelService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<Mapel>>> Get()
        {
            var mapelList = await _mapelService.GetAsync();
            return Ok(mapelList);
        }

        [HttpGet("{id:length(24)}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Mapel>> Get(string id)
        {
            var mapel = await _mapelService.GetAsync(id);

            if (mapel == null)
            {
                return NotFound();
            }

            return Ok(mapel);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create(Mapel mapel)
        {
            await _mapelService.CreateAsync(mapel);
            return CreatedAtAction(nameof(Get), new { id = mapel.Id }, mapel);
        }

        [HttpPut("{id:length(24)}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(string id, Mapel updatedMapel)
        {
            var mapel = await _mapelService.GetAsync(id);

            if (mapel == null)
            {
                return NotFound();
            }

            updatedMapel.Id = mapel.Id;
            await _mapelService.UpdateAsync(id, updatedMapel);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(string id)
        {
            var mapel = await _mapelService.GetAsync(id);

            if (mapel == null)
            {
                return NotFound();
            }

            await _mapelService.DeleteAsync(id);

            return NoContent();
        }
    }
}
