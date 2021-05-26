using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventYojana.API.DataAccess.DataEntities.Common
{
	public class UserLogin
	{

		[Key]
		public int LoginId { get; set; }

		[Required]
		[ForeignKey("UserRoles")]
		public int UserType { get; set; }
		public UserRoles UserRoles { get; set; }

		[Required]
		[StringLength(50)]
		public string Username { get; set; }

		[Required]
		public string Password { get; set; }

		[Required]
		public string PasswordSalt { get; set; }

		public bool? IsVerifiedUser { get; set; }

		[Required]
		public DateTime CreatedDate { get; set; }
	}

}
