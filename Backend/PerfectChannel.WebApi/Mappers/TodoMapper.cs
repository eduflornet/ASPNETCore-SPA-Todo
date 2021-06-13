
using AutoMapper;
using PerfectChannel.WebApi.Data.Entities;
using PerfectChannel.WebApi.Models;

namespace PerfectChannel.WebApi.Mappers
{
    public class TodoMapper:Profile
    {
        public TodoMapper()
        {
            CreateMap<Todo, TodoModel>().ReverseMap();
        }
    }
}
