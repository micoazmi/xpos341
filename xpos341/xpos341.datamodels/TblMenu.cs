using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace xpos341.datamodels;

[Table("TblMenu")]
public partial class TblMenu
{
    [Key]
    public int Id { get; set; }

    [StringLength(80)]
    [Unicode(false)]
    public string MenuName { get; set; } = null!;

    [StringLength(80)]
    [Unicode(false)]
    public string MenuAction { get; set; } = null!;

    [StringLength(80)]
    [Unicode(false)]
    public string MenuController { get; set; } = null!;

    [StringLength(80)]
    [Unicode(false)]
    public string? MenuIcon { get; set; }

    public int? MenuSorting { get; set; }

    public bool? IsParent { get; set; }

    public int? MenuParent { get; set; }

    public bool? IsDelete { get; set; }

    public int CreatedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreatedDate { get; set; }

    public int? UpdatedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedDate { get; set; }
}
