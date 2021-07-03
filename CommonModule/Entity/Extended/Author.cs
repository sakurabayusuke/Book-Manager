using CommonModule.Entity.Base;
using CommonModule.Logic;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using System;
using System.ComponentModel.DataAnnotations;
using System.Reactive.Disposables;

namespace CommonModule.Entity.Extended
{
	public class Author : AuthorBase, IDisposable
	{
		[Required(ErrorMessage = "必須")]
		public ReactiveProperty<string> RpName { get; }

		public ReactiveProperty<string> RpPenName { get; }

		public ReactiveProperty<DateTime?> RpBirthday { get; }

		public ReactiveProperty<int> RpAge { get; }

		private CompositeDisposable Disposable { get; }

		/// <summary>
		/// 通常コンストラクタ
		/// </summary>
		public Author(AuthorBase author = null)
		{
			Disposable = new CompositeDisposable();

			Id = author?.Id ?? 0;
			RpName = new ReactiveProperty<string>(author?.Name ?? string.Empty).AddTo(Disposable);
			RpPenName = new ReactiveProperty<string>(author?.PenName ?? string.Empty).AddTo(Disposable);
			RpBirthday = new ReactiveProperty<DateTime?>(author?.Birthday ?? DateTime.Now).AddTo(Disposable);
			RpAge = new ReactiveProperty<int>(Utility.CalAge(RpBirthday.Value)).AddTo(Disposable);
			IsDeleted = author?.IsDeleted ?? 0;
			CreatedAt = author?.CreatedAt ?? DateTime.Now;
			UpdatedAt = author?.UpdatedAt ?? DateTime.Now;

			RpBirthday.Subscribe(n =>
			{
				RpAge.Value = Utility.CalAge(n);
			});

		}

		/// <summary>
		/// DB保存のために Base の値にセットする
		/// </summary>
		/// <param name="author">null じゃない時は Delete の時</param>
		public void SetToBaseParam(Author author = null)
		{
			Name = author?.RpName.Value ?? RpName.Value;
			PenName = author?.RpPenName.Value ?? RpPenName.Value;
			Birthday = author?.RpBirthday.Value ?? RpBirthday.Value ?? DateTime.Now;
			IsDeleted = author == null ? 0 : 1;
		}

		public void Dispose() => Disposable.Dispose();
	}
}
