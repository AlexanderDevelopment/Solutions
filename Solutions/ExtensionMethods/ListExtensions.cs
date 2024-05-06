using System;
using System.Collections.Generic;
using System.Linq;


namespace _src.Scripts.ExtentionMethods
{
	public static class ListExtensions
	{
		public static T GetRandomElement<T>(this IEnumerable<T> list)
		{
			// If there are no elements in the collection, return the default value of T
			if (list.Count() == 0)
				return default(T);
 
			// Guids as well as the hash code for a guid will be unique and thus random        
			int hashCode = Math.Abs(Guid.NewGuid().GetHashCode());
			return list.ElementAt(hashCode % list.Count());
		}
	}
}
