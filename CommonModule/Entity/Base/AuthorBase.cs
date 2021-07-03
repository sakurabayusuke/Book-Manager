using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommonModule.Entity.Base
{
	[Table("Authors")]
	public class AuthorBase : EntityBase
	{
		[MaxLength(50)]
		[Required]
		public string Name { get; set; }

		[MaxLength(50)]
		public string PenName { get; set; }

		[Required]
		public DateTime Birthday { get; set; }
	}
}
