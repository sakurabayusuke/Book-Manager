using Prism.Mvvm;
using System;
using System.ComponentModel.DataAnnotations;

namespace CommonModule.Entity.Base
{
	public class EntityBase : BindableBase
	{
		[Key, Required]
		public int Id { get; set; }

		[Required]
		public int IsDeleted { get; set; }

		public DateTime CreatedAt { get; set; }

		public DateTime UpdatedAt { get; set; }
	}
}
