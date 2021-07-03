using Prism.Regions;

namespace PublisherModule.Views
{
	/// <summary>
	/// Interaction logic for PublisherList.xaml
	/// </summary>
	public partial class PublisherList
	{
		public PublisherList(IRegionManager regionManager)
		{
			InitializeComponent();

			// なぜかドロワー上だけが ContentRegion に自動で登録できず、手動で登録する必要がある。
			// 二重登録を避けるため、一回追加後は登録しない。
			if (regionManager.Regions.ContainsRegionWithName("PublisherSingleRegion")) return;
			CommonModule.Logic.Utility.SetRegionManager(regionManager, Cc, "PublisherSingleRegion");
		}
	}
}
