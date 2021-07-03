using CommonModule.Entity.Base;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using System;
using System.Reactive.Disposables;

namespace CommonModule.Entity.Extended
{
	public class Publisher : PublisherBase
	{
		public ReactiveProperty<string> RpName { get; }

		public ReactiveProperty<int> RpCorporationType { get; }

		public ReactiveProperty<string> RpCorporationTypeName { get; }

		private CompositeDisposable Disposable { get; }

		/// <summary>
		/// 通常コンストラクタ
		/// </summary>
		/// <param name="publisher"></param>
		public Publisher(PublisherBase publisher = null)
		{
			Disposable = new CompositeDisposable();

			Id = publisher?.Id ?? 0;
			RpName = new ReactiveProperty<string>(publisher?.Name ?? string.Empty).AddTo(Disposable);
			RpCorporationType = new ReactiveProperty<int>(publisher?.CorporationType ?? 0).AddTo(Disposable);
			RpCorporationTypeName = new ReactiveProperty<string>(EnumsExtensions.GetName<Enums.CorporationType>(RpCorporationType.Value)).AddTo(Disposable);
			IsDeleted = publisher?.IsDeleted ?? 0;
			CreatedAt = publisher?.CreatedAt ?? DateTime.Now;
			UpdatedAt = publisher?.UpdatedAt ?? DateTime.Now;

			RpCorporationType.Subscribe(n =>
			{
				RpCorporationTypeName.Value = EnumsExtensions.GetName<Enums.CorporationType>(n);
			});
		}

		/// <summary>
		/// DB保存時のために Base に値をセットする
		/// </summary>
		/// <param name="publisher">null じゃない時は Delete の時</param>
		public void SetToBaseParam(Publisher publisher = null)
		{
			Name = publisher?.RpName.Value ?? RpName.Value;
			CorporationType = publisher?.RpCorporationType.Value ?? RpCorporationType.Value;
			// CorporationType = (int)(publisher?.RpCorporationType.Value ?? RpCorporationType.Value);
			IsDeleted = publisher == null ? 0 : 1;
		}

		public void Dispose() => Disposable.Dispose();
	}
}
