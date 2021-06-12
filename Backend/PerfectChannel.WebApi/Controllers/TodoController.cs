using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using PerfectChannel.WebApi.Data;
using PerfectChannel.WebApi.Data.Entities;
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
        [ProducesResponseType(typeof(TodoModel[]), 200)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        public async Task<ActionResult<TodoModel[]>> GetPending()
        {
            try
            {
                var pendingList = await _todosRepository.GetByStatus(false);
                return _mapper.Map<TodoModel[]>(pendingList);
            }
            catch (Exception exp)
            {
                return BadRequest(new ApiResponse { Status = false, Message = exp.Message});
            }
        }
        [HttpGet("completed")]
        [ProducesResponseType(typeof(TodoModel[]), 200)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        public async Task<ActionResult<TodoModel[]>> GetCompleted()
        {
            try
            {
                var completedList = await _todosRepository.GetByStatus(true);
                return _mapper.Map<TodoModel[]>(completedList);
            }
            catch (Exception exp)
            {
                return BadRequest(new ApiResponse { Status = false, Message = exp.Message});
            }
        }

        
        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse), 201)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        public async Task<ActionResult> CreateTodo([FromBody] TodoModel todo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse { Status = false, ModelState = ModelState });
            }

            try
            {
                var todoEntity = _mapper.Map<Todo>(todo);
                var newTodo = await _todosRepository.Insert(todoEntity);
                var newTodoModel = _mapper.Map<TodoModel>(newTodo);
                if (newTodoModel == null)
                {
                    return BadRequest(new ApiResponse { Status = false });
                }
                return CreatedAtRoute("GetTodoRoute", new { id = newTodoModel.Id },
                    new ApiResponse { Status = true, Todo = newTodoModel });
            }
            catch (Exception exp)
            {
                return BadRequest(new ApiResponse { Status = false, Message = exp.Message});
            }
        }

        
        [HttpGet("{id}", Name = "GetTodoRoute")]
        [ProducesResponseType(typeof(Todo), 200)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        public async Task<ActionResult> GetTodo(int id)
        {
            try
            {
                var todo = await _todosRepository.GetById(id);
                return Ok(_mapper.Map<TodoModel>(todo));
            }
            catch (Exception exp)
            {
                return BadRequest(new ApiResponse { Status = false, Message = exp.Message});
            }
        }
        

    }
}