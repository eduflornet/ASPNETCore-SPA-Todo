using System.ComponentModel.DataAnnotations;

namespace PerfectChannel.WebApi.Models
{
    public class TodoModel
    {
        public int Id { get; set; }

        public bool IsDone { get; set; }

        [Required]
        [StringLength(128, MinimumLength = 1)]
        public string Description { get; set; }
    }
}
