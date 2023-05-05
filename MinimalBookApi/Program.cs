using System.Net;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
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

app.UseHttpsRedirection();


var books = new List<Book> { 
    new Book { Id = 1, Author = "Author1", Title = "Title1"},
    new Book { Id = 2, Author = "Author2", Title = "Title2"},
    new Book { Id = 3, Author = "Author3", Title = "Title3"},
    new Book { Id = 4, Author = "Author4", Title = "Title4"}
};


app.MapGet("/book", () =>
{
    return books;
});


app.MapGet("/book/{id}", (int id) =>
{
    var book = books.Find(x => x.Id == id);

    if (book is null)
        return Results.NotFound("This book doesn't exist.");

    return Results.Ok(book);
});

app.MapPost("/book", (Book book) =>
{
    books.Add(book);

    return books;
});

app.MapPut("/book", (Book updatedBook) =>
{
    var book = books.Find(x => x.Id == updatedBook.Id);

    if (book is null)
        return Results.NotFound("This book doesn't exist.");

    book.Author = updatedBook.Author;
    book.Title = updatedBook.Title;

    return Results.Ok(book);
});



app.MapDelete("/book/{id}", (int id) =>
{
    var book = books.Find(x => x.Id == id);

    if (book is null)
        return Results.NotFound("This book doesn't exist.");

    books.Remove(book);

    return Results.Ok(books);

});

app.Run();



public class Book
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public required string Author { get; set; }
}
