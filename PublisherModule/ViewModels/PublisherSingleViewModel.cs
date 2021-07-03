using CommonModule;
using CommonModule.Entity;
using CommonModule.Entity.Extended;
using CommonModule.Logic;
using CommonModule.Model;
using MaterialDesignThemes.Wpf;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using PublisherModule.Views;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using System;
using System.Collections.ObjectModel;
using System.Reactive.Disposables;
using System.Windows;
using System.Windows.Controls;

namespace PublisherModule.ViewModels
{
	public class PublisherSingleViewModel : BindableBase, INavigationAware, IDisposable
	{
		// コンストラクタ内でインスタンスを生成しないと上手くバインドできない？ OnNavigatedTo 内で 代入してもバインドできなかった.
		public Publisher Publisher { get; } = new();

		// Delete用
		private Publisher PublisherForDelete { get; set; }

		// Title
		public ReactiveProperty<string> Title { get; } = new();

		private readonly CompositeDisposable _disposable = new();

		// Button
		public DelegateCommand SaveButton { get; }

		public DelegateCommand DeleteButton { get; }

		// DeleteButton の表示
		public ReactiveProperty<Visibility> VisibilityDeleteButton { get; } = new();

		// コンボボックスの値
		public ReactiveCollection<ComboItem> Corporations { get; } = new();

		private IRegionManager RegionManager { get; }


		public PublisherSingleViewModel(IRegionManager regionManager)
		{
			RegionManager = regionManager;

			Title.AddTo(_disposable);

			SaveButton = new DelegateCommand(SaveButtonExecute);
			DeleteButton = new DelegateCommand(DeleteButtonExecute);

			Corporations.AddRange(EnumsExtensions.GetEnumsForItemsSource<Enums.CorporationType>());
			Corporations.AddTo(_disposable);
		}

		public void OnNavigatedTo(NavigationContext navigationContext)
		{
			var publisher = navigationContext.Parameters[nameof(CommonModule.Entity.Extended.Publisher)] as Publisher;

			if (publisher == null)
			{
				Title.Value = "Create New Publisher";
				VisibilityDeleteButton.Value = Visibility.Collapsed;
			}
			else if (!Utility.IsNewEntity(publisher))
			{
				Title.Value = "Edit Publisher";
				VisibilityDeleteButton.Value = Visibility.Visible;
				Publisher.Id = publisher.Id;
				Publisher.RpName.Value = publisher.RpName.Value;
				Publisher.RpCorporationType.Value = publisher.RpCorporationType.Value;
				PublisherForDelete = publisher;
			}
		}

		public bool IsNavigationTarget(NavigationContext navigationContext) => false;

		public void OnNavigatedFrom(NavigationContext navigationContext) { }

		private void SaveButtonExecute()
		{
			Publisher.SetToBaseParam();
			Save();
		}

		private void DeleteButtonExecute()
		{
			Publisher.SetToBaseParam(PublisherForDelete);
			Save();
		}

		private void Save()
		{
			PublisherModel.Merge(Publisher);

			DrawerHost.CloseDrawerCommand.Execute(Dock.Right, null);
			Dispose();

			RegionManager.RequestNavigate("MainRegion", nameof(PublisherList));
		}

		public void Dispose()
		{
			_disposable.Dispose();
			Publisher.Dispose();
			Corporations.Dispose();
		}
	}
}
