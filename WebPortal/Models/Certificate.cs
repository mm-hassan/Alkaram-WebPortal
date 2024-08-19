using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebPortal.Models;

public partial class Certificate
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; } = null!;
    [Required]
    public string Division { get; set; } = null!;
    [Required]
    [DisplayName("File Path")]
    public string ImagePath { get; set; } = null!;
    [DisplayName("Last Updated By")]
    public string LastUpdatedBy { get; set; }
    [DisplayName("Last Updated On")]
    public DateTime LastUpdatedOn { get; set; }
}
