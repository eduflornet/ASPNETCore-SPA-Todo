using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PerfectChannel.WebApi.Data;
using PerfectChannel.WebApi.Models;


namespace PerfectChannel.WebApi.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("AllowOrigin")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly ITodosRepository _todosRepository;
        private readonly IMapper _mapper;

        public TodoController(ITodosRepository todosRepository, IMapper mapper)
        {
            _todosRepository = todosRepository;
            _mapper = mapper;
        }

        [HttpGet("pending")]
        public async Task<ActionResult<TodoModel[]>> GetPending()
        {
            try
            {
                var pendingList = await _todosRepository.GetByStatus(false);
                return _mapper.Map<TodoModel[]>(pendingList);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }
        [HttpGet("completed")]
        public async Task<ActionResult<TodoModel[]>> GetCompleted()
        {
            try
            {
                var completedList = await _todosRepository.GetByStatus(true);
                return _mapper.Map<TodoModel[]>(completedList);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

    }
}