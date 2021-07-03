using Prism.Regions;

namespace AuthorModule.Views
{
	/// <summary>
	/// Interaction logic for AuthorList
	/// </summary>
	public partial class AuthorList
	{
		public AuthorList(IRegionManager regionManager)
		{
			InitializeComponent();

			// なぜかドロワー上だけが ContentRegion に自動で登録できず、手動で登録する必要がある。
			// 二重登録を避けるため、一回追加後は登録しない。
			if (regionManager.Regions.ContainsRegionWithName("AuthorSingleRegion")) return;
			CommonModule.Logic.Utility.SetRegionManager(regionManager, Cc, "AuthorSingleRegion");
		}
	}
}
