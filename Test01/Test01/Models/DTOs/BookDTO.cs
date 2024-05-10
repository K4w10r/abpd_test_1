namespace Test01.Models.DTOs;

public class BookDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public List<AuthorDto> Authors { get; set; } = null!;
    public List<GenreDto> Genres { get; set; } = null!;
}

public class AuthorDto
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
}

public class GenreDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}

public class PubHouseDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string OwnerFirstName { get; set; } = string.Empty;
    public string OwnerLastName { get; set; } = string.Empty;
}
