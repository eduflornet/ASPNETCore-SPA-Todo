
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PerfectChannel.WebApi.Data.Entities;

namespace PerfectChannel.WebApi.Data
{
    public class TodosRepository: ITodosRepository
    {
        private readonly TodoContext _context;
        private readonly ILogger _logger;

        public TodosRepository(TodoContext context, ILoggerFactory loggerFactory)
        {
            _context = context;
            _logger = loggerFactory.CreateLogger("TodosRepository");
        }

        public Task<Todo[]> GetByStatus(bool isDone)
        {
            return _context.Todos.Where(t => t.IsDone == isDone).ToArrayAsync();
        }

        public async Task<Todo> Insert(Todo todo)
        {
            _context.Add(todo);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception exp)
            {
                _logger.LogError($"Error in {nameof(Insert)}: " + exp.Message);
            }

            return todo;
        }

        public Task<Todo> GetById(int id)
        {
            return _context.Todos.FirstOrDefaultAsync(t => t.Id == id);
        }
    }
}
