using CommonModule.Entity.Base;
using CommonModule.Logic;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CommonModule.Model
{
	public static class DbManager
	{
		/// <summary>
		/// 1件登録
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="entity"></param>
		public static void Merge<T>(T entity) where T : EntityBase
		{
			using var db = new BookManagerModel();
			SetDate(entity);
			if (Utility.IsNewEntity(entity))
			{
				db.Add(entity);
			}
			else
			{
				db.Update(entity);
			}
			Save(db);
		}

		///// <summary>
		///// 複数登録
		///// </summary>
		///// <typeparam name="T"></typeparam>
		///// <param name="list"></param>
		//public static void MergeList<T>(List<T> list) where T : EntityBase
		//{
		//	using var db = new BookManagerModel();
		//	SetDate(list);
		//	db.Add(list);
		//	Save(db);
		//}

		/// <summary>
		/// 1件取得
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="id"></param>
		/// <returns></returns>
		public static T Get<T>(int id) where T : EntityBase
		{
			using var db = new BookManagerModel();
			return db.Set<T>().FirstOrDefaultAsync(n => n.Id == id || n.IsDeleted == 0).Result;
		}

		/// <summary>
		/// 全件取得
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public static List<T> All<T>() where T : EntityBase
		{
			using var db = new BookManagerModel();
			return db.Set<T>().ToListAsync().Result.Where(n => n.IsDeleted == 0).ToList();
		}

		///// <summary>
		///// 1件削除
		///// </summary>
		///// <typeparam name="T"></typeparam>
		///// <param name="entity"></param>
		//public static void Delete<T>(T entity) where T : EntityBase
		//{
		//	using var db = new BookManagerModel();
		//	db.Set<T>().Remove(entity);
		//	Save(db);
		//}

		///// <summary>
		///// 複数削除
		///// </summary>
		///// <typeparam name="T"></typeparam>
		///// <param name="list"></param>
		//public static void DeleteList<T>(List<T> list) where T : EntityBase
		//{
		//	using var db = new BookManagerModel();
		//	db.Set<T>().RemoveRange(list);
		//	Save(db);
		//}

		//private static void SetDate<T>(List<T> list) where T : EntityBase
		//{
		//	var now = DateTime.Now;
		//	foreach (var entity in list)
		//	{
		//		entity.UpdatedAt = now;
		//		if (Utility.IsNewEntity(entity)) entity.CreatedAt = now;
		//	}
		//}

		private static void SetDate<T>(T entity) where T : EntityBase
		{
			var now = DateTime.Now;
			entity.UpdatedAt = now;
			if (Utility.IsNewEntity(entity)) entity.CreatedAt = now;
		}

		private static void Save(BookManagerModel db)
		{
			try
			{
				db.SaveChangesAsync();
			}
			catch (DbUpdateException e)
			{
				Console.WriteLine(e.Message);
			}

		}
	}
}
