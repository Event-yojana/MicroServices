using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EventYojana.API.DataAccess.DataEntities.Common
{
	public class EmailLogs
	{

		[Key]
		public int EmailLogId { get; set; }

		[Required]
		[StringLength(50)]
		public string FromEmailAddress { get; set; }

		[Required]
		[StringLength(50)]
		public string ToEmailAddress { get; set; }

		public string Subject { get; set; }

		public string Body { get; set; }

		[Required]
		public bool IsProduction { get; set; }

		[Required]
		public bool IsSend { get; set; }

		[Required]
		[ForeignKey("Application")]
		public int ApplicationId { get; set; }
		public Application Application { get; set; }

		[Required]
		[StringLength(25)]
		public string FromUserType { get; set; }

		public int? FromUserId { get; set; }

		[Required]
		[StringLength(25)]
		public string ToUserType { get; set; }

		public int? ToUserId { get; set; }

		[Required]
		public DateTime CreatedDate { get; set; }
	}

}
