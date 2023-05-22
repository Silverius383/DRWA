using BookStoreApi.Models;
using BookStoreApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
namespace BookStoreApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    private readonly BooksService _booksService;

    public BooksController(BooksService booksService) =>
        _booksService = booksService;
/// <param name="id"></param>

/// <response code="400">If the item is null</response>
/// <response code="404">If the item is not exist</response>
/// <response code="500">If the server error</response>
/// <response code="201">Returns the newly created item</response>

    [HttpGet]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]

    public async Task<List<Book>> Get() =>
        await _booksService.GetAsync();

    /// <param name="id"></param>

/// <response code="400">If the item is null</response>
/// <response code="404">If the item is not exist</response>
/// <response code="500">If the server error</response>
/// <response code="201">Returns the newly created item</response>

    [HttpGet("{id:length(24)}")]
        [Authorize]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]

    public async Task<ActionResult<Book>> Get(string id)
    {
        var book = await _booksService.GetAsync(id);

        if (book is null)
        {
            return NotFound();
        }

        return book;
    }
/// <param name="newBook"></param>

/// <response code="400">If the item is null</response>
/// <response code="404">If the item is not exist</response>
/// <response code="500">If the server error</response>
/// <response code="201">Returns the newly created item</response>
    [HttpPost]
        [Authorize]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]


    public async Task<IActionResult> Post(Book newBook)
    {
        await _booksService.CreateAsync(newBook);

        return CreatedAtAction(nameof(Get), new { id = newBook.Id }, newBook);
    }
/// <param name="updatedBook"></param>

/// <response code="400">If the item is null</response>
/// <response code="404">If the item is not exist</response>
/// <response code="500">If the server error</response>
/// <response code="201">Returns the newly created item</response>
    [HttpPut("{id:length(24)}")]
        [Authorize]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]

    public async Task<IActionResult> Update(string id, Book updatedBook)
    {
        var book = await _booksService.GetAsync(id);

        if (book is null)
        {
            return NotFound();
        }

        updatedBook.Id = book.Id;

        await _booksService.UpdateAsync(id, updatedBook);

        return NoContent();
    }

/// <param name="id"></param>

/// <response code="400">If the item is null</response>
/// <response code="404">If the item is not exist</response>
/// <response code="500">If the server error</response>
/// <response code="201">Returns the newly created item</response>
    [HttpDelete("{id:length(24)}")]
        [Authorize]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]

    public async Task<IActionResult> Delete(string id)
    {
        var book = await _booksService.GetAsync(id);

        if (book is null)
        {
            return NotFound();
        }

        await _booksService.RemoveAsync(id);

        return NoContent();
    }
}