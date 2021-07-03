using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CommonModule.Entity.Base;

namespace CommonModule.Entity.Extended
{
	[Table("Books")]
	public class Book : BookBase
	{
		[MaxLength(100)]
		[Required]
		public string Name { get; set; }

		public string Description { get; set; }

		[Required]
		public int Price { get; set; }

		[Required]
		public Author Author { get; set; }

		[Required]
		public Publisher Publisher { get; set; }
	}
}
