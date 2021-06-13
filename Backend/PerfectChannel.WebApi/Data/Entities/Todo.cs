
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PerfectChannel.WebApi.Data.Entities
{
    public class Todo
    {
        [Key]
        public int Id { get; set; }
        [Column(TypeName = "bit")]
        public bool IsDone { get; set; }
        [Column(TypeName = "nvarchar(100)")]
        [Required]
        public string Description { get; set; }
    }
}
