using BookStoreAPI.Models;
using BookStoreAPI.Services.Interface;
using MongoDB.Driver;

namespace BookStoreAPI.Services.Implement
{
    public class BookService : IBookService
    {
        private readonly IMongoCollection<BookModel> bookCollection;
        public BookService(IBookStoreDatabaseSettings BookStoreDatabaseSettings, IMongoClient
        mongoClient)
        {
            var mongoDatabase = mongoClient.GetDatabase(BookStoreDatabaseSettings.DatabaseName);
            bookCollection = mongoDatabase.GetCollection<BookModel>(BookStoreDatabaseSettings.CollectionName);
        }
        public async Task<List<BookModel>> Get()
        {
            return await bookCollection.Find(b => true).ToListAsync();
        }
        public async Task<BookModel?> FindId(string id)
        {
            return await bookCollection.Find(b => b.Id == id).FirstOrDefaultAsync();
        }

        public async Task Create(BookModel book)
        {
            await bookCollection.InsertOneAsync(book);
        }
        public async Task Update(string id, BookModel book)
        {
            await bookCollection.ReplaceOneAsync(b => b.Id == id, book);
        }
        public async Task Delete(string id)
        {
            bookCollection.DeleteOneAsync(b => b.Id == id);
        }
    }
}
