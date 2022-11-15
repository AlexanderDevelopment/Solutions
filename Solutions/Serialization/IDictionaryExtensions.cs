using System.Collections.Generic;


namespace OpenUnitySolutions.Serialization.Extensions
{
	// ReSharper disable once InconsistentNaming
	public static class IDictionaryExtensions
	{
		public static UnityDictionary<TKey, TElement> ToUnityDictionary<TKey, TElement>(this IDictionary<TKey, TElement> source)
			=> new (source);
	}
}
