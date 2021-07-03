using AuthorModule.Views;
using CommonModule.Entity.Extended;
using CommonModule.Model;
using MaterialDesignThemes.Wpf;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using Reactive.Bindings;
using System;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Data;

namespace AuthorModule.ViewModels
{
	public class AuthorListViewModel : BindableBase, INavigationAware
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
		public ReactiveCollection<Author> Authors { get; }

		public Author Author { get; set; }

		// Search
		public ReactiveProperty<string> KeyWord { get; } = new(string.Empty);

		public ReactiveProperty<DateTime?> From { get; } = new();

		public ReactiveProperty<DateTime?> To { get; } = new();

		// RegionManager
		public IRegionManager RegionManager { get; }

		public AuthorListViewModel(IRegionManager regionManager)
		{
			RegionManager = regionManager;

			// ボタン
			NewButton = new DelegateCommand(NewButtonExecute);
			RowDoubleClick = new DelegateCommand(RowDoubleClickExecute);
			SearchButton = new DelegateCommand(SearchButtonExecute);
			ConditionClearButton = new DelegateCommand(ConditionDeleteExecute);

			// データセット
			Authors = new ReactiveCollection<Author>();
			Authors.AddRange(AuthorModel.Select());
		}

		/// <summary>
		/// 入力 Drawer 表示
		/// </summary>
		private void NewButtonExecute()
		{
			RegionManager.RequestNavigate("AuthorSingleRegion", nameof(AuthorSingle));

			// ドロワーを開く
			DrawerHost.OpenDrawerCommand.Execute(Dock.Right, null);
		}

		private void SearchButtonExecute()
		{
			var collection = CollectionViewSource.GetDefaultView(Authors);
			if (string.IsNullOrWhiteSpace(KeyWord.Value) && From.Value == null && To.Value == null)
			{
				collection.Filter = null;
				return;
			}
			collection.Filter = n =>
			{
				var author = (Author)n;
				if (!string.IsNullOrWhiteSpace(KeyWord.Value))
				{
					if (!author.RpName.Value.Contains(KeyWord.Value) && !author.RpPenName.Value.Contains(KeyWord.Value)) return false;
				}
				if (From.Value != null)
				{
					if (!(From.Value <= author.RpBirthday.Value)) return false;
				}
				if (To.Value != null)
				{
					if (!(author.RpBirthday.Value <= To.Value)) return false;
				}
				return true;
			};

		}

		private void RowDoubleClickExecute()
		{
			var p = new NavigationParameters();
			if (Author != null)
			{
				p.Add(nameof(CommonModule.Entity.Extended.Author), Author);
			}

			RegionManager.RequestNavigate("AuthorSingleRegion", nameof(AuthorSingle), p);

			// ドロワーを開く
			DrawerHost.OpenDrawerCommand.Execute(Dock.Right, null);
		}

		private void ConditionDeleteExecute()
		{
			KeyWord.Value = string.Empty;
			From.Value = null;
			To.Value = null;
			SearchButtonExecute();
		}

		public void OnNavigatedTo(NavigationContext navigationContext)
		{
			Authors.Clear();
			Authors.AddRange(AuthorModel.Select());
			SearchButtonExecute();
		}

		public bool IsNavigationTarget(NavigationContext navigationContext) => true;

		public void OnNavigatedFrom(NavigationContext navigationContext) { }
	}
}
