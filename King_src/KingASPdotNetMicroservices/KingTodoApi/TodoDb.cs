using King_TodoApi;
using Microsoft.EntityFrameworkCore;

namespace KingTodoApi
{
    public class TodoDb : DbContext
    {
        public TodoDb(DbContextOptions<TodoDb> options) : base(options) { }
        public DbSet<TodoItem> TodoItems { get; set; }
    }
}
