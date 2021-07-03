using AuthorModule.Views;
using LicenseModule.Views;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using PublisherModule.Views;

namespace MainModule.ViewModels
{
	public class MainViewModel : BindableBase
	{
		// 画面領域マネージャー
		private readonly IRegionManager _regionManager;

		// window タイトル
		public string Title => "Book Manager";

		// AuthorListButton
		public DelegateCommand ShowAuthorListButton { get; }

		// PublisherListButton
		public DelegateCommand ShowPublisherListButton { get; }

		// LicensesButton
		public DelegateCommand ShowLicensesButton { get; }

		public MainViewModel(IRegionManager regionManager)
		{
			_regionManager = regionManager;

			ShowAuthorListButton = new DelegateCommand(ShowAuthorListButtonExecute);
			ShowPublisherListButton = new DelegateCommand(ShowPublisherListButtonExecute);
			ShowLicensesButton = new DelegateCommand(ShowLicensesButtonExecute);
		}

		/// <summary>
		/// AuthorList に画面遷移
		/// </summary>
		private void ShowAuthorListButtonExecute()
		{
			// ContentRegion に View の AuthorList を挿入する
			_regionManager.RequestNavigate("MainRegion", nameof(AuthorList));
		}

		/// <summary>
		/// PublisherList に画面遷移
		/// </summary>
		private void ShowPublisherListButtonExecute()
		{
			// ContentRegion に View の AuthorList を挿入する
			_regionManager.RequestNavigate("MainRegion", nameof(PublisherList));
		}

		/// <summary>
		/// LicensesList に画面遷移
		/// </summary>
		private void ShowLicensesButtonExecute()
		{
			// ContentRegion に View の AuthorList を挿入する
			_regionManager.RequestNavigate("MainRegion", nameof(Licenses));
		}


	}
}
