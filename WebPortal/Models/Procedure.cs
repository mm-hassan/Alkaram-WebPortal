using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebPortal.Models
{
    public class Procedure
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public string Division { get; set; } = null!;
        [Required]
        [DisplayName("Procedure Path")]
        public string ProcedurePath { get; set; } = null!;
    }
}
