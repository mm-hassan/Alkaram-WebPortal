using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebPortal.Models
{
    public class ProcedureViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public string Division { get; set; } = null!;
        [Required]
        [DisplayName("File Path")]
        public IFormFile Path { get; set; } = null!;
    }
}
