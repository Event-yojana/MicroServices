using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EventYojana.API.DataAccess.DataEntities.Common
{
	public class Address
	{

		[Key]
		public int AddressId { get; set; }

		[Required]
		public string AddressLine { get; set; }

		[Required]
		[StringLength(50)]
		public string City { get; set; }

		[Required]
		[StringLength(50)]
		public string State { get; set; }

		[Required]
		public int PinCode { get; set; }
	}

}
