using Microsoft.AspNetCore.Mvc;
using Test01.Repositories;

namespace Test01.Controllers;


[Route("api/[controller]")]
[ApiController]
public class BooksController : ControllerBase
{
    private readonly IBooksRepository _booksRepository;
    public BooksController(IBooksRepository booksRepository)
    {
        _booksRepository = booksRepository;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAnimal(int id)
    {
        if (!await _booksRepository.DoesBookExist(id))
            return NotFound($"Animal with given ID - {id} doesn't exist");

        var book = await _booksRepository.GetBook(id);

        return Ok(book);
    }
}