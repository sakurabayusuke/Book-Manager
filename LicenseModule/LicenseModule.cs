using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace LicenseModule
{
	public class LicenseModule : IModule
	{
		public void OnInitialized(IContainerProvider containerProvider)
		{
			containerProvider.Resolve<IRegionManager>();
		}

		public void RegisterTypes(IContainerRegistry containerRegistry)
		{

		}
	}
}