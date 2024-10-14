using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xpos341.viewmodels
{
    public class VMTblCategory
    {
		public int Id { get; set; }

		[Required(ErrorMessage = "Harap isi Name Category, jangan kosong...!!")]
		[StringLength(10)]
		[MinLength(3, ErrorMessage = "Isi minimum 3 karakter")]
		public string? NameCategory { get; set; }

		[Required(ErrorMessage = "Harap isi Description, jangan kosong...!!")]
		public string? Description { get; set; }

		public bool? IsDelete { get; set; }

		public int CreateBy { get; set; }

		public DateTime CreateDate { get; set; }

		public int? UpdateBy { get; set; }

		public DateTime? UpdateDate { get; set; }
	}
}

