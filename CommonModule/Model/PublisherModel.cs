using CommonModule.Entity.Base;
using CommonModule.Entity.Extended;
using System.Collections.Generic;

namespace CommonModule.Model
{
	public static class PublisherModel
	{
		public static void Merge(Publisher author)
		{
			DbManager.Merge(author);
		}

		public static List<Publisher> Select()
		{
			var list = DbManager.All<PublisherBase>();

			var ret = new List<Publisher>();
			foreach (var entity in list)
			{
				ret.Add(new Publisher(entity));
			}

			return ret;
		}
	}
}
