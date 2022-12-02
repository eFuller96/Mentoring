using ToDoApplication.DataStorage;
using ToDoApplication.Models;
using ToDoApplication.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// if there are 2 implementations to 1 interfaces, add some logic to choose. If it doesn't have name it picks the last one
builder.Services.AddSingleton<ITodoRepository,TodoRepository>();
// Circular dependency explained in Notes.md
builder.Services.AddSingleton<IDictionary<Guid, ToDoItem>>(new Dictionary<Guid, ToDoItem>());
builder.Services.AddSingleton<IDataStorage, FileStorage>();
//builder.Services.AddSingleton<IDataStorage>(new FileStorage("")); //todo put filepath


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();