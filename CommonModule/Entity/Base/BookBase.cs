using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommonModule.Entity.Base
{
	[Table("Books")]
	public class BookBase : EntityBase
	{
		[MaxLength(100)]
		[Required]
		public string Name { get; set; }

		public string Description { get; set; }

		[Required]
		public int Price { get; set; }

		[Required]
		public AuthorBase Author { get; set; }

		[Required]
		public PublisherBase Publisher { get; set; }
	}
}
