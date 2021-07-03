using AuthorModule.Views;
using LicenseModule.Views;
using MainModule.Views;
using Prism.Ioc;
using Prism.Modularity;
using Reactive.Bindings;
using System.Globalization;
using System.Reactive.Concurrency;
using System.Threading;
using System.Windows;
using System.Windows.Markup;
using PublisherModule.Views;

namespace MainModule
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App
	{
		protected override Window CreateShell()
		{
			// ReactivePropertyの即時反映設定
			ReactivePropertyScheduler.SetDefault(ImmediateScheduler.Instance);
			SetCultureInfo();
			return Container.Resolve<Main>();
		}

		protected override void RegisterTypes(IContainerRegistry containerRegistry)
		{
			containerRegistry.RegisterForNavigation<AuthorList>();
			containerRegistry.RegisterForNavigation<Licenses>();
			containerRegistry.RegisterForNavigation<PublisherList>();
		}

		protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
		{
			moduleCatalog.AddModule<AuthorModule.AuthorModule>();
			moduleCatalog.AddModule<LicenseModule.LicenseModule>();
			moduleCatalog.AddModule<PublisherModule.PublisherModule>();
		}

		private void SetCultureInfo()
		{
			Thread.CurrentThread.CurrentCulture = new CultureInfo("ja-JP");
			Thread.CurrentThread.CurrentUICulture = new CultureInfo("ja-JP");
			FrameworkElement.LanguageProperty.OverrideMetadata(typeof(FrameworkElement), new FrameworkPropertyMetadata(
				XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag)));
		}
	}
}
