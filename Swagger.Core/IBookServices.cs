

namespace Swagger.Core
{
    public interface IBookServices
    {

        List<Book> GetBooks();

        Book AddBook(Book book);

        Book GetBook(string id);
    }
}
