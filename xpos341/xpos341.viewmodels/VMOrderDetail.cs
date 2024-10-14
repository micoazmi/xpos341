using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xpos341.viewmodels
{
	public class VMOrderDetail
	{
		public int Id { get; set; }

		public int IdHeader { get; set; }

		public int IdProduct { get; set; }

        public string? NameProduct { get; set; }

        public int Qty { get; set; }

		public int Stock { get; set; }

		public decimal SumPrice { get; set; }

        public decimal Price { get; set; }

        public bool? IsDelete { get; set; }

		public int CreateBy { get; set; }

		public DateTime CreateDate { get; set; }

		public int? UpdateBy { get; set; }

		public DateTime? UpdateDate { get; set; }
	}
}
