using CommonModule.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace CommonModule
{
	public static class EnumsExtensions
	{
		/// <summary>
		/// Enum To Name (Enum の Description 属性を取得する)
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="value"></param>
		/// <returns></returns>
		public static string GetName<T>(this T value) where T : Enum
		{
			var infos = typeof(T).GetField(value.ToString());
			var attribute = (DescriptionAttribute)infos?.GetCustomAttributes(typeof(DescriptionAttribute), false).FirstOrDefault();
			return attribute == null ? string.Empty : attribute.Description;
		}

		/// <summary>
		/// Get Enums
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public static List<ComboItem> GetEnumsForItemsSource<T>() where T : Enum
		{
			return typeof(T).GetEnumValues()
				.Cast<T>()
				.Select(n => new ComboItem { Code = (int)(object)n, Name = n.GetName() }).ToList();
		}

		/// <summary>
		/// Get ComboItem (int から ComboBoxItem を取得)
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="value"></param>
		/// <returns></returns>
		public static ComboItem GetComboItem<T>(int value) where T : Enum
			=> new() { Code = value, Name = ((T)Enum.ToObject(typeof(T), value)).GetName() };

		/// <summary>
		/// int to Name (int から Description 属性を取得)
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="value"></param>
		/// <returns></returns>
		public static string GetName<T>(int value) where T : Enum => ((T)Enum.ToObject(typeof(T), value)).GetName();
	}
}
