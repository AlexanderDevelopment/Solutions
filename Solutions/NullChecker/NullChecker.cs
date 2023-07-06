using System.Collections.Generic;
using UnityEngine;


namespace _src.Scripts.OpenUnitySolutions
{
	public static class NullChecker
	{
		public static bool CheckForNull<T>(T checkingClass)
		{
			if (checkingClass is not null)
				return true;

			UnityEngine.Debug.LogError($"{checkingClass} is null");

			return false;
		}


		public static bool CheckForNull<T>(T checkingClass, GameObject notify)
		{
			if (checkingClass is not null)
				return true;

			UnityEngine.Debug.LogError($"{checkingClass} is null", notify);

			return false;
		}


		public static bool CheckForNullAll<T>(GameObject notify, params T[] classes)
		{
			if (classes.Length <= 0)
				return true;

			foreach (var classExample in classes)
			{
				if (classExample is not null)
					continue;

				UnityEngine.Debug.LogError($"{classExample} is null", notify);

				return false;
			}

			return true;
		}


		public static bool CheckForNullAll<T>(params T[] classes)
		{
			if (classes.Length <= 0)
				return true;

			foreach (var classExample in classes)
			{
				if (classExample is not null)
					continue;

				UnityEngine.Debug.LogError($"{classExample} is null");

				return false;
			}

			return true;
		}
	}
}
