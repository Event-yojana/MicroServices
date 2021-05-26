using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventYojana.API.DataAccess.DataEntities.Common
{
	public class UserRoles
	{

		[Key]
		public int RoleId { get; set; }

		[StringLength(25)]
		public string RoleName { get; set; }

		[StringLength(50)]
		public string RoleDescription { get; set; }

		[Required]
		public DateTime CreatedDate { get; set; }
	}

}
