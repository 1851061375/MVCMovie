using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Policy;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace MvcMovie.Models;

public class MenuCustom
{

    public int Id { get; set; }
    public string? DisplayName { get; set; }
    [StringLength(60, MinimumLength = 3), Required]
    public required string Controller { get; set; }
    public string? Action { get; set; } = "Index";
    public int? ParentId { get; set; }
    public string? Icon { get; set; } = "bi bi-dot";
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int? Order { get; set; }
    [DefaultValue(false)]
    public bool? Status { get; set; }

    [ForeignKey("ParentId")]
    public List<MenuCustom> Childrens { get; set; }
}