namespace graph_ql_demo.Types;

[QueryType]
public class Query
{

    private BookService _bookService;

    public Query(BookService bookService)
    {
        _bookService = bookService;
    }

    public  Book? GetBook(int bookId) => _bookService.GetBook(bookId);
    
    public IEnumerable<Book> GetBooks(string authorName) => _bookService.GetBooks().Where(b => b.Author.Name == authorName);
}
