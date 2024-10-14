using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xpos341.viewmodels
{
	public class VMTblMenuAccess
	{
		public int Id { get; set; }

        public string? MenuName { get; set; }

        public string? MenuController { get; set; }


        public int? RoleId { get; set; }

		public int? MenuId { get; set; }

		public bool? IsDelete { get; set; }

		public int CreatedBy { get; set; }

		public DateTime CreatedDate { get; set; }

		public int? UpdatedBy { get; set; }

		public DateTime? UpdatedDate { get; set; }

	}
}
