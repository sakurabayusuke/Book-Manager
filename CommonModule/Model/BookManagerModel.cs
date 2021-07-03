using System;
using System.IO;
using CommonModule.Entity.Base;
using Microsoft.EntityFrameworkCore;

namespace CommonModule.Model
{
	public class BookManagerModel : DbContext
	{
		private const string BookPath = @"BookManager\\BookManager.db";

		public DbSet<BookBase> Books { get; set; }
		public DbSet<AuthorBase> Authors { get; set; }
		public DbSet<PublisherBase> Publishers { get; set; }

		// 場所が悪いと db が作成できない。C 直下だと権限が無くて作成できない可能性がある
		protected override void OnConfiguring(DbContextOptionsBuilder options)
		{
			var appPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
			
			options.UseSqlite(@$"Data Source={Path.Combine(appPath, BookPath)}");
		}
			
	}
}

