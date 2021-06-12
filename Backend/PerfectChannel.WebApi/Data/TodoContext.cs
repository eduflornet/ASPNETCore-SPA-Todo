
using Microsoft.EntityFrameworkCore;
using PerfectChannel.WebApi.Data.Entities;

namespace PerfectChannel.WebApi.Data
{
    public class TodoContext:DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options) : base(options) { }
        public DbSet<Todo> Todos { get; set; }
    }
}
