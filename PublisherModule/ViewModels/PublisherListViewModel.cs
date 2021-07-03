using CommonModule;
using CommonModule.Entity;
using CommonModule.Entity.Extended;
using CommonModule.Model;
using MaterialDesignThemes.Wpf;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using PublisherModule.Views;
using Reactive.Bindings;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Data;

namespace PublisherModule.ViewModels
{
	public class PublisherListViewModel : BindableBase, INavigationAware
	{
		// コマンド
		// New
		public DelegateCommand NewButton { get; }

		// DoubleClick
		public DelegateCommand RowDoubleClick { get; }

		// Search
		public DelegateCommand SearchButton { get; }

		// Condition Delete
		public DelegateCommand ConditionClearButton { get; }

		// Data
		public ReactiveCollection<Publisher> Publishers { get; } = new();

		public Publisher Publisher { get; set; }

		// Search
		public ReactiveProperty<string> KeyWord { get; } = new(string.Empty);

		public ReactiveCollection<ComboItem> Corporations { get; } = new();

		public ReactiveProperty<int?> CorporationType { get; } = new();

		// RegionManager
		public IRegionManager RegionManager { get; }

		public PublisherListViewModel(IRegionManager regionManager)
		{
			RegionManager = regionManager;

			// データセット
			Corporations.AddRange(EnumsExtensions.GetEnumsForItemsSource<Enums.CorporationType>());

			Publishers.AddRange(PublisherModel.Select());

			// ボタン
			NewButton = new DelegateCommand(NewButtonExecute);
			RowDoubleClick = new DelegateCommand(RowDoubleClickExecute);
			SearchButton = new DelegateCommand(SearchButtonExecute);
			ConditionClearButton = new DelegateCommand(ConditionDeleteExecute);
		}

		/// <summary>
		/// 入力 Drawer 表示
		/// </summary>
		private void NewButtonExecute()
		{
			RegionManager.RequestNavigate("PublisherSingleRegion", nameof(PublisherSingle));

			// ドロワーを開く
			DrawerHost.OpenDrawerCommand.Execute(Dock.Right, null);
		}

		private void SearchButtonExecute()
		{
			var collection = CollectionViewSource.GetDefaultView(Publishers);
			if (string.IsNullOrWhiteSpace(KeyWord.Value) && CorporationType.Value == null)
			{
				collection.Filter = null;
				return;
			}
			collection.Filter = n =>
			{
				var publisher = (Publisher)n;
				if (!string.IsNullOrWhiteSpace(KeyWord.Value))
				{
					if (!publisher.RpName.Value.Contains(KeyWord.Value)) return false;
				}

				if (CorporationType.Value != null)
				{
					if (publisher.RpCorporationType.Value != CorporationType.Value) return false;
				}
				return true;
			};

		}

		private void RowDoubleClickExecute()
		{
			var p = new NavigationParameters();
			if (Publisher != null)
			{
				p.Add(nameof(CommonModule.Entity.Extended.Publisher), Publisher);
			}

			RegionManager.RequestNavigate("PublisherSingleRegion", nameof(PublisherSingle), p);

			// ドロワーを開く
			DrawerHost.OpenDrawerCommand.Execute(Dock.Right, null);
		}

		private void ConditionDeleteExecute()
		{
			KeyWord.Value = string.Empty;
			CorporationType.Value = null;

			SearchButtonExecute();
		}

		public void OnNavigatedTo(NavigationContext navigationContext)
		{
			Publishers.Clear();
			Publishers.AddRange(PublisherModel.Select());
			SearchButtonExecute();
		}

		public bool IsNavigationTarget(NavigationContext navigationContext) => true;

		public void OnNavigatedFrom(NavigationContext navigationContext)
		{

		}
	}
}
