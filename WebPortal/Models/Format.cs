using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebPortal.Models
{
    public class Format
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public string Division { get; set; } = null!;
        [Required]
        [DisplayName("Format Path")]
        public string FormatPath { get; set; } = null!;
        [DisplayName("Last Updated By")]
        public string LastUpdatedBy { get; set; }
        [DisplayName("Last Updated On")]
        public DateTime LastUpdatedOn { get; set; }
    }
}
