using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommonModule.Entity.Base
{
	[Table("Publishers")]
	public class PublisherBase : EntityBase
	{
		[MaxLength(50)]
		[Required]
		public string Name { get; set; }

		[Required]
		public int CorporationType { get; set; }
	}
}
