using BookStoreAPI.Services.Implement;
using BookStoreAPI.Services.Interface;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

/*var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";*/

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
#region Add services
builder.Services.Configure<BookStoreDatabaseSettings>(
    builder.Configuration.GetSection(nameof(BookStoreDatabaseSettings)));

builder.Services.AddSingleton<IBookStoreDatabaseSettings>(db =>
    db.GetRequiredService<IOptions<BookStoreDatabaseSettings>>().Value);

builder.Services.AddSingleton<IMongoClient>(m =>
    new MongoClient(builder.Configuration.GetValue<string>("BookStoreDatabaseSettings:ConnectionString")));

builder.Services.AddScoped<IBookService, BookService>();

/*builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins("http://localhost:8080/", "https://localhost:7227/")
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});*/

builder.Services.AddCors();
#endregion

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(builder =>
{
    builder
    .AllowAnyHeader()
    .AllowAnyMethod()
    .AllowAnyOrigin();
});

/*app.UseCors();*/

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
