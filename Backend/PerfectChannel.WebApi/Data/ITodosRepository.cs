using System.Threading.Tasks;
using PerfectChannel.WebApi.Data.Entities;

namespace PerfectChannel.WebApi.Data
{
    public interface ITodosRepository
    {
        Task<Todo[]> GetByStatus(bool isDone);
        Task<Todo> Insert(Todo todo);
        Task<Todo> GetById(int id);

    }
}