using Microsoft.Data.SqlClient;
using Test01.Models.DTOs;

namespace Test01.Repositories;

public class BooksRepository : IBooksRepository
{
    private readonly IConfiguration _configuration;
    public BooksRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<bool> DoesBookExist(int id)
    {
        var query = "SELECT 1 FROM books WHERE PK = @ID";

        await using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default"));
        await using SqlCommand command = new SqlCommand();

        command.Connection = connection;
        command.CommandText = query;
        command.Parameters.AddWithValue("@ID", id);

        await connection.OpenAsync();

        var res = await command.ExecuteScalarAsync();

        return res is not null;
    }

    public async Task<bool> DoesGenreExist(int id)
    {
        var query = "SELECT 1 FROM genres WHERE PK = @ID";

        await using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default"));
        await using SqlCommand command = new SqlCommand();

        command.Connection = connection;
        command.CommandText = query;
        command.Parameters.AddWithValue("@ID", id);

        await connection.OpenAsync();

        var res = await command.ExecuteScalarAsync();

        return res is not null;
    }

    public async Task<BookDto> GetBook(int id)
    {
         var query = @"SELECT 
    books.PK AS BookId, 
    books.title AS BookTitle, 
	authors.first_name AS AuthorFN, 
    authors.last_name AS AuthorLN, 
    genres.name AS GenreName 
	FROM books 
    JOIN books_genres ON books_genres.FK_book = books.PK 
    JOIN genres ON books_genres.FK_genre = genres.PK 
    JOIN books_authors ON books_authors.FK_book = books.PK 
    JOIN authors ON books_authors.FK_author = authors.PK 
    WHERE books.PK = @ID";
	    
	    await using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default"));
	    await using SqlCommand command = new SqlCommand();

	    command.Connection = connection;
	    command.CommandText = query;
	    command.Parameters.AddWithValue("@ID", id);
	    
	    await connection.OpenAsync();

	    var reader = await command.ExecuteReaderAsync();

	    var bookIdOrdinal = reader.GetOrdinal("BookID");
	    var bookTitleOrdinal = reader.GetOrdinal("BookTitle");
	    var authorFNOrdinal = reader.GetOrdinal("AuthorFN");
	    var authorLNOrdinal = reader.GetOrdinal("AuthorLN");
	    var genreNameOrdinal = reader.GetOrdinal("GenreName");


	    BookDto bookDto = null;

	    while (await reader.ReadAsync())
	    {
		    if (bookDto is not null)
		    {
			    bookDto.Authors.Add(new AuthorDto()
			    {
				    FirstName = reader.GetString(authorFNOrdinal),
				    LastName = reader.GetString(authorLNOrdinal)
			    });
			    bookDto.Genres.Add(new GenreDto()
			    {
				    Name = reader.GetString(genreNameOrdinal)
			    });
			    
		    }
		    else
		    {
			    bookDto = new BookDto()
			    {
				    Id = reader.GetInt32(bookIdOrdinal),
				    Title = reader.GetString(bookTitleOrdinal),
				    
				    Authors = new List<AuthorDto>()
				    {
					    new AuthorDto()
					    {
						    FirstName = reader.GetString(authorFNOrdinal),
						    LastName = reader.GetString(authorLNOrdinal)
					    }
				    },
				    
				    Genres = new List<GenreDto>()
				    {
					    new GenreDto()
					    {
						    Name = reader.GetString(genreNameOrdinal)
					    }
				    }
			    };
		    }
	    }

	    if (bookDto is null) throw new Exception();
        
        return bookDto;
    }

    public Task<int> DeleteGenre(int id)
    {
	    throw new NotImplementedException();
    }
}