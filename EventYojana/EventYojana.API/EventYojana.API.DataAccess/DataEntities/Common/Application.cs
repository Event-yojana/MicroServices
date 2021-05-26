using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EventYojana.API.DataAccess.DataEntities.Common
{
	public class Application
	{

		[Key]
		public int Id { get; set; }

		[Required]
		[StringLength(10)]
		[Column(TypeName = "VARCHAR")]
		public string ApplicationCode { get; set; }

		[StringLength(50)]
		public string ApplicationName { get; set; }

		[Required]
		public DateTime CreatedDate { get; set; }
	}

}
