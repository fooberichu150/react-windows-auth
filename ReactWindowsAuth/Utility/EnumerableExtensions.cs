using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReactWindowsAuth.Utility
{
	public static class EnumerableExtensions
	{
		public static bool IsNullOrEmpty<T>(this IEnumerable<T> collection)
		{
			return collection == null || collection.Count() == 0;
		}
	}
}
