using AuthorModule.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace AuthorModule
{
	public class AuthorModule : IModule
	{
		public void OnInitialized(IContainerProvider containerProvider)
		{
			containerProvider.Resolve<IRegionManager>();
		}

		public void RegisterTypes(IContainerRegistry containerRegistry)
		{
			containerRegistry.RegisterForNavigation<AuthorSingle>();
			containerRegistry.RegisterForNavigation<AuthorList>();
		}
	}
}