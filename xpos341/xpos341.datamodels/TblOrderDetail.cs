using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace xpos341.datamodels;

[Table("TblOrderDetail")]
public partial class TblOrderDetail
{
    [Key]
    public int Id { get; set; }

    public int IdHeader { get; set; }

    public int IdProduct { get; set; }

    public int Qty { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal SumPrice { get; set; }

    public bool? IsDelete { get; set; }

    public int CreateBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreateDate { get; set; }

    public int? UpdateBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdateDate { get; set; }
}
