using graph_ql_demo.Types;

public class BookService
{
    private Dictionary<int, Book> _books = new()
    {
        {1, new Book(Author: new Author("John"), Title: "C#", Id: 1)},
        {2, new Book(Author: new Author("Eve"), Title: "Java", Id: 2)},
        {3, new Book(Author: new Author("Mark"), Title: "ASP.NET", Id: 3)}
    };

    public Book? GetBook(int bookId)
    {
        return _books[bookId];
    }

    public IEnumerable<Book> GetBooks()
    {
        return _books.Values;
    }
}