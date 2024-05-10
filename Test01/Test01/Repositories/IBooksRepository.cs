using Test01.Models.DTOs;
namespace Test01.Repositories;

public interface IBooksRepository
{
    Task<bool> DoesBookExist(int id);
    Task<bool> DoesGenreExist(int id);
    Task<BookDto> GetBook(int id);
}