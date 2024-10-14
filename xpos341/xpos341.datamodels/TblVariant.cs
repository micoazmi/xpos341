using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace xpos341.datamodels;

[Table("TblVariant")]
public partial class TblVariant
{
    [Key]
    public int Id { get; set; }

    public int IdCategory { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string NameVariant { get; set; } = null!;

    [Unicode(false)]
    public string? Description { get; set; }

    public bool? IsDelete { get; set; }

    public int CreateBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreateDate { get; set; }

    public int? UpdateBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdateDate { get; set; }
}
