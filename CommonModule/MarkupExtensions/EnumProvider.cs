using System;
using System.Collections;
using System.Linq;
using System.Windows.Markup;

namespace CommonModule.MarkupExtensions
{
	public class EnumProvider<T> : MarkupExtension where T : Enum
	{
		public IEnumerable Source { get; } = typeof(T)
			.GetEnumValues()
			.Cast<T>()
			.Select(n => new { Code = n, Name = n.GetName() });

		public override object ProvideValue(IServiceProvider serviceProvider) => this;
	}
}
