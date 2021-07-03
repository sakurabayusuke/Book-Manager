using System;
using Microsoft.Xaml.Behaviors;
using System.Windows;
using System.Windows.Controls;

namespace CommonModule.Behavior
{
	public class ValidationErrorBehavior : Behavior<DependencyObject>
	{
		// View内でのエラーの数
		private int _errorCount;

		// View内でのエラーの有無
		public bool HasViewError
		{
			get => (bool)GetValue(HasViewErrorProperty);
			set => SetValue(HasViewErrorProperty, value);
		}

		public static readonly DependencyProperty HasViewErrorProperty =
			DependencyProperty.Register("HasViewError", typeof(bool), typeof(ValidationErrorBehavior), new UIPropertyMetadata(false));

		protected override void OnAttached()
		{
			base.OnAttached();
			// イベントハンドラ登録
			Validation.AddErrorHandler(AssociatedObject, ErrorHandler);
		}

		protected override void OnDetaching()
		{
			// イベントハンドラ解除
			Validation.RemoveErrorHandler(AssociatedObject, ErrorHandler);
			base.OnDetaching();
		}

		private void ErrorHandler(object sender, ValidationErrorEventArgs e)
		{
			switch (e.Action)
			{
				case ValidationErrorEventAction.Added:
					_errorCount++;
					break;
				case ValidationErrorEventAction.Removed:
					_errorCount--;
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}

			// エラーの有無をHasViewErrorにセット
			HasViewError = _errorCount != 0;
		}
	}
}
