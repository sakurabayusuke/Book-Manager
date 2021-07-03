using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace CommonModule.UserControls.Views
{
	/// <summary>
	/// Interaction logic for DeleteButton
	/// </summary>
	public partial class DeleteButton
	{

		public ICommand Command
		{
			get => (ICommand)GetValue(CommandProperty);
			set => SetValue(CommandProperty, value);
		}

		public static readonly DependencyProperty CommandProperty
			= DependencyProperty.Register(
				"Command",
				typeof(ICommand),
				typeof(UserControl),
				new UIPropertyMetadata(null));

		//public DelegateCommand GetCommand(DependencyObject target)
		//	=> (DelegateCommand)target.GetValue(CommandProperty);

		//public void SetCommand(DependencyObject target, DelegateCommand value)
		//	=> target.SetValue(CommandProperty, value);

		//private static void OnCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		//{
		//	Set((DelegateCommand)e.NewValue);
		//}

		//private static void Set(DelegateCommand dc)
		//{
		//	BtnDelete.Command = dc;
		//}

		public DeleteButton()
		{
			InitializeComponent();
		}
	}
}
