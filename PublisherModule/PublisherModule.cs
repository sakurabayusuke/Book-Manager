using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using PublisherModule.Views;

namespace PublisherModule
{
	public class PublisherModule : IModule
	{
		public void OnInitialized(IContainerProvider containerProvider)
		{
			containerProvider.Resolve<IRegionManager>();
		}

		public void RegisterTypes(IContainerRegistry containerRegistry)
		{
			containerRegistry.RegisterForNavigation<PublisherSingle>();
			containerRegistry.RegisterForNavigation<PublisherList>();
		}
	}
}