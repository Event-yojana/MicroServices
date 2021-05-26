using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventYojana.API.DataAccess.DataEntities.Common
{
	public class RoleModule
	{

		[Key]
		public int Id { get; set; }

		[Required]
		[ForeignKey("UserRoles")]
		public int RoleId { get; set; }
		public UserRoles UserRoles { get; set; }

		[Required]
		[ForeignKey("Module")]
		public int ModuleId { get; set; }
		public Module Module { get; set; }

		[Required]
		public bool IsView { get; set; }

		[Required]
		public bool IsAdd { get; set; }

		[Required]
		public bool IsEdit { get; set; }

		[Required]
		public bool IsDelete { get; set; }

		[Required]
		public DateTime CreatedDate { get; set; }
	}

}
