using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace _src.Scripts.OpenUnitySolutions
{
	public static class NullChecker
	{
		public static bool IsNull<T>(T checkingClass)
		{
			if (checkingClass is null)
				return true;

			UnityEngine.Debug.LogError($"{checkingClass} is null");

			return false;
		}


		public static bool IsNull<T>(T checkingClass, GameObject notify)
		{
			if (checkingClass is null)
				return true;

			UnityEngine.Debug.LogError($"{checkingClass} is null", notify);

			return false;
		}


		//When we are click on the error - problem gameObject is showing in inspector
		public static bool IsNullAll<T>(GameObject notify, params T[] classes)
		{
			if (classes.Length == 0)
			{
				UnityEngine.Debug.LogError("No have classes to check for null");

				return false;
			}

			bool oneMoreClassIsNull = false;

			foreach (var classExample in classes)
			{
				if (classExample is null)
				{
					UnityEngine.Debug.LogError($"{classExample} is null", notify);
					oneMoreClassIsNull = true;
				}
			}

			return oneMoreClassIsNull;
		}


		public static bool IsNullAll<T>(params T[] classes)
		{
			if (classes.Length == 0)
			{
				UnityEngine.Debug.LogError("No have classes to check for null");

				return false;
			}

			bool oneMoreClassIsNull = false;

			foreach (var classExample in classes)
			{
				if (classExample is null)
				{
					UnityEngine.Debug.LogError($"{classExample} is null");
					oneMoreClassIsNull = true;
				}
			}

			return oneMoreClassIsNull;
		}
	}
}
