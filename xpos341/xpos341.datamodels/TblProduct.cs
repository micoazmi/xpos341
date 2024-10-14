using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace xpos341.datamodels;

[Table("TblProduct")]
public partial class TblProduct
{
    [Key]
    public int Id { get; set; }

    public int IdVariant { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string NameProduct { get; set; } = null!;

    [Column(TypeName = "decimal(18, 0)")]
    public decimal Price { get; set; }

    public int Stock { get; set; }

    [Unicode(false)]
    public string? Image { get; set; }

    public bool? IsDelete { get; set; }

    public int CreateBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreateDate { get; set; }

    public int? UpdateBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdateDate { get; set; }
}
