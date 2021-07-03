using CommonModule.Entity.Base;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Prism.Regions;

namespace CommonModule.Logic
{
	public static class Utility
	{

		/// <summary>
		/// 年齢計算
		/// </summary>
		/// <param name="birthday"></param>
		public static int CalAge(DateTime? birthday) => birthday == null ? 0 : DateTime.Now.Year - birthday?.Year ?? 0;

		/// <summary>
		/// 新規データ?
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="entity"></param>
		/// <returns></returns>
		public static bool IsNewEntity<T>(T entity) where T : EntityBase => entity.Id == 0;

		/// <summary>
		/// 手動でRegionManagerにセットする
		/// </summary>
		/// <param name="regionManager"></param>
		/// <param name="regionTarget"></param>
		/// <param name="regionName"></param>
		public static void SetRegionManager(IRegionManager regionManager, DependencyObject regionTarget, string regionName)
		{
			RegionManager.SetRegionName(regionTarget, regionName);
			RegionManager.SetRegionManager(regionTarget, regionManager);
		}

		/// <summary>
		/// 指定した Region から View を削除
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="regionName"></param>
		/// <param name="manager"></param>
		public static void RemoveView<T>(string regionName, IRegionManager manager) where T : UserControl
		{
			// regionName から Viewを探し出す
			var view = manager.Regions[regionName].Views
				.FirstOrDefault(x => x.GetType().Name == typeof(T).Name);

			if (view == null) return;
			// RegionName から View を削除
			manager.Regions[regionName].Remove(view);
		}

		/// <summary>
		/// 指定した Region を削除
		/// </summary>
		/// <param name="regionName"></param>
		/// <param name="manager"></param>
		public static void RemoveRegion(string regionName, IRegionManager manager)
		{
			// Regionを削除
			manager.Regions.Remove(regionName);
		}
	}
}
