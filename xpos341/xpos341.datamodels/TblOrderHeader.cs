using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace xpos341.datamodels;

[Table("TblOrderHeader")]
public partial class TblOrderHeader
{
    [Key]
    public int Id { get; set; }

    [StringLength(20)]
    public string CodeTransaction { get; set; } = null!;

    public int IdCustomer { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal Amount { get; set; }

    public int TotalQty { get; set; }

    public bool IsCheckout { get; set; }

    public bool? IsDelete { get; set; }

    public int CreateBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreateDate { get; set; }

    public int? UpdateBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdateDate { get; set; }
}
