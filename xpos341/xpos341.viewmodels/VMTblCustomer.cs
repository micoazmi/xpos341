using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xpos341.viewmodels
{
	public class VMTblCustomer
	{
		public int Id { get; set; }

		public string NameCustomer { get; set; } = null!;

		public string Email { get; set; } = null!;

		public string Password { get; set; } = null!;

		public string Address { get; set; } = null!;

		public string Phone { get; set; } = null!;

		public int? IdRole { get; set; }

        public string? RoleName { get; set; }

        public bool IsDelete { get; set; }

		public int CreateBy { get; set; }

		public DateTime CreateDate { get; set; }

		public int? UpdateBy { get; set; }

		public DateTime? UpdateDate { get; set; }
	}
}
