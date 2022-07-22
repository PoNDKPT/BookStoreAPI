namespace BookStoreAPI.Services.Interface
{
    public interface IBookStoreDatabaseSettings
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
        string CollectionName { get; set; }
    }
}
