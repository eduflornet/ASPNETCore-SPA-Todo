
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PerfectChannel.WebApi.Data.Entities;

namespace PerfectChannel.WebApi.Data
{
    public class TodosRepository: ITodosRepository
    {
        private readonly TodoContext _context;
        
        public TodosRepository(TodoContext context) => _context = context;


        public Task<Todo[]> GetByStatus(bool isDone)
        {
            return _context.Todos.Where(t => t.IsDone == isDone).ToArrayAsync();
        }
    }
}
