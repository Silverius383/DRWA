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
    public class GuruController : ControllerBase
    {
        private readonly GuruService _guruService;

        public GuruController(GuruService guruService)
        {
            _guruService = guruService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<Guru>>> Get()
        {
            var guruList = await _guruService.GetAsync();
            return Ok(guruList);
        }

        [HttpGet("{id:length(24)}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Guru>> Get(string id)
        {
            var guru = await _guruService.GetAsync(id);

            if (guru == null)
            {
                return NotFound();
            }

            return Ok(guru);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create(Guru guru)
        {
            await _guruService.CreateAsync(guru);
            return CreatedAtAction(nameof(Get), new { id = guru.Id }, guru);
        }

        [HttpPut("{id:length(24)}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(string id, Guru updatedGuru)
        {
            var guru = await _guruService.GetAsync(id);

            if (guru == null)
            {
                return NotFound();
            }

            updatedGuru.Id = guru.Id;
            await _guruService.UpdateAsync(id, updatedGuru);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(string id)
        {
            var guru = await _guruService.GetAsync(id);

            if (guru == null)
            {
                return NotFound();
            }

            await _guruService.DeleteAsync(id);

            return NoContent();
        }
    }
}
