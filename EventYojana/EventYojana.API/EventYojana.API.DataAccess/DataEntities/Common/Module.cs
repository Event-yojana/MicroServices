using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventYojana.API.DataAccess.DataEntities.Common
{
	public class Module
	{

		[Key]
		public int Id { get; set; }

		[Required]
		[StringLength(50)]
		[Column(TypeName = "VARCHAR")]
		public string ModuleName { get; set; }

		[Required]
		[ForeignKey("Application")]
		public int ApplicationId { get; set; }
		public Application Application { get; set; }

		[Required]
		public bool IsActive { get; set; }

		[Required]
		public DateTime CreatedDate { get; set; }
	}

}
