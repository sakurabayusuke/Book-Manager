using Prism.Mvvm;

namespace LicenseModule.ViewModels
{
	public class LicenseViewModel : BindableBase
	{
		private string _message;
		public string Message
		{
			get { return _message; }
			set { SetProperty(ref _message, value); }
		}

		public LicenseViewModel()
		{
			Message = "View A from your Prism Module";
		}
	}
}
