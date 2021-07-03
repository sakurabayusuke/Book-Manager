using AuthorModule.Views;
using CommonModule.Entity.Extended;
using CommonModule.Logic;
using CommonModule.Model;
using MaterialDesignThemes.Wpf;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using System;
using System.Linq;
using System.Reactive.Disposables;
using System.Windows;
using System.Windows.Controls;

namespace AuthorModule.ViewModels
{
	public class AuthorSingleViewModel : BindableBase, INavigationAware, IDisposable
	{
		// コンストラクタ内でインスタンスを生成しないと上手くバインドできない？ OnNavigatedTo 内で 代入してもバインドできなかった.
		public Author Author { get; } = new();

		// Delete用
		private Author AuthorForDelete { get; set; }

		public ReactiveProperty<string> Title { get; } = new();

		private readonly CompositeDisposable _disposable = new();

		public DelegateCommand SaveButton { get; }

		public DelegateCommand DeleteButton { get; }

		public ReactiveProperty<Visibility> VisibilityDeleteButton { get; } = new();

		private IRegionManager RegionManager { get; }


		public AuthorSingleViewModel(IRegionManager regionManager)
		{
			RegionManager = regionManager;

			Title.AddTo(_disposable);

			SaveButton = new DelegateCommand(SaveButtonExecute);
			DeleteButton = new DelegateCommand(DeleteButtonExecute);

		}

		public void OnNavigatedTo(NavigationContext navigationContext)
		{
			var author = navigationContext.Parameters[nameof(CommonModule.Entity.Extended.Author)] as Author;

			if (author == null)
			{
				Title.Value = "Create New Author";
				VisibilityDeleteButton.Value = Visibility.Collapsed;
			}
			else if (!Utility.IsNewEntity(author))
			{
				Title.Value = "Edit Author";
				VisibilityDeleteButton.Value = Visibility.Visible;
				Author.Id = author.Id;
				Author.RpName.Value = author.RpName.Value;
				Author.RpPenName.Value = author.RpPenName.Value;
				Author.RpBirthday.Value = author.RpBirthday.Value;

				AuthorForDelete = author;
			}
		}

		public bool IsNavigationTarget(NavigationContext navigationContext) => false;

		public void OnNavigatedFrom(NavigationContext navigationContext)
		{

		}

		private void SaveButtonExecute()
		{
			Author.SetToBaseParam();
			Save();
		}

		private void DeleteButtonExecute()
		{
			Author.SetToBaseParam(AuthorForDelete);
			Save();
		}

		private void Save()
		{
			AuthorModel.Merge(Author);

			DrawerHost.CloseDrawerCommand.Execute(Dock.Right, null);
			Dispose();

			RegionManager.RequestNavigate("MainRegion", nameof(AuthorList));
		}

		public void Dispose()
		{
			_disposable.Dispose();
			Author.Dispose();
		}
	}
}
