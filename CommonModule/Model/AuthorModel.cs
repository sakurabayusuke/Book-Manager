using CommonModule.Entity.Base;
using CommonModule.Entity.Extended;
using System.Collections.Generic;

namespace CommonModule.Model
{
	public static class AuthorModel
	{
		public static void Merge(Author author)
		{
			DbManager.Merge(author);
		}

		//public static void MergeList(List<Author> author)
		//{
		//	DbManager.MergeList(author);
		//}


		public static List<Author> Select()
		{
			var list = DbManager.All<AuthorBase>();

			var ret = new List<Author>();
			foreach (var entity in list)
			{
				ret.Add(new Author(entity));
			}

			return ret;
		}

		public static Author Get(int id)
		{

			return DbManager.Get<Author>(id);
		}

		//public static void Delete(Author author)
		//{
		//	DbManager.Delete(author);
		//}

		//public static void Delete(List<Author> author)
		//{
		//	DbManager.DeleteList(author);
		//}
	}
}
