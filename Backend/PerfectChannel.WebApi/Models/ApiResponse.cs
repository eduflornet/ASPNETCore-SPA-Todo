
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace PerfectChannel.WebApi.Models
{
    public class ApiResponse
    {
        public bool Status { get; set; }
        public TodoModel Todo { get; set; }
        public ModelStateDictionary ModelState { get; set; }
        public string Message { get; set; }
    }
}
