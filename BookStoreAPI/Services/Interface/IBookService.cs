using BookStoreAPI.Models;

namespace BookStoreAPI.Services.Interface
{
    public interface IBookService
    {
        Task<List<BookModel>> Get();
        Task<BookModel?> FindId(string id);
        Task Create(BookModel book);
        Task Update(string id, BookModel book);
        Task Delete(string id);
    }
}
