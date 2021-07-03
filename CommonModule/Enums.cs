using System.ComponentModel;

namespace CommonModule
{
	public class Enums
	{
		public enum CorporationType
		{
			[Description("Inc.")]
			Inc = 0,
			[Description("LLC.")]
			Llc,
			[Description("NPO")]
			Npo,
		}
	}
}
