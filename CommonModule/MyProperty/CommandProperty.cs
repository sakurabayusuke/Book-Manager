using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CommonModule.MyProperty
{
	public class CommandProperty
	{
		public static readonly DependencyProperty _command
			= DependencyProperty.RegisterAttached(
				"Command",
				typeof(ICommand),
				typeof(UserControl),
				new FrameworkPropertyMetadata());

		public static ICommand GetCommand(DependencyObject target)
			=> (ICommand)target.GetValue(_command);

		public static void SetCommand(DependencyObject target, ICommand value)
			=> target.SetValue(_command, value);
	}
}
