using King_TodoApi;
using KingTodoApi;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add DI - AddService
builder.Services.AddDbContext<TodoDb>(opt => opt.UseInMemoryDatabase("TodoList"));

var app = builder.Build();

// Configure pipelines - UseMethod
app.MapGet("/todoitems", async (TodoDb db) 
    => await db.TodoItems.ToListAsync());

app.MapGet("/todoitems/{id}", async (TodoDb db, int id)
    => await db.TodoItems.FindAsync(id));

app.MapPost("/todoitems", async (TodoDb db, TodoItem item) =>
{
    db.TodoItems.Add(item);
    await db.SaveChangesAsync();
    return Results.Created($"/todoitems/{item.Id}", item);
});

app.MapPut("/todoitems/{id}", async (TodoDb db, TodoItem item, int id) =>
{
    var todo = await db.TodoItems.FindAsync(id);
    if (todo == null) return Results.NotFound();
    todo.Name = item.Name;
    todo.IsCompleted = item.IsCompleted;
    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.MapDelete("/todoitems/{id}", async (TodoDb db, int id) =>
{
    if (await db.TodoItems.FindAsync(id) is TodoItem todo)
    {
        db.TodoItems.Remove(todo);
        await db.SaveChangesAsync();
        return Results.NoContent() ;
    }
    return Results.NotFound();
});

app.Run();
