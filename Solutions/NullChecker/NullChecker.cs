using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace _src.Scripts.OpenUnitySolutions
{
	public static class NullChecker
	{
		public static bool IsNull<T>(T checkingClass, bool debugMessages)
		{
			if (checkingClass is null)
			{
				if (debugMessages)
					UnityEngine.Debug.LogError($"{checkingClass} is null");

				return true;
			}

			return false;
		}


		public static bool IsNull<T>(T checkingClass, GameObject notify, bool debugMessages)
		{
			if (checkingClass is null)
			{
				if (debugMessages)
					UnityEngine.Debug.LogError($"{checkingClass} is null", notify);

				return true;
			}

			return false;
		}


		//When we are click on the error - problem gameObject is showing in inspector
		public static bool IsNullAll<T>(GameObject notify, bool debugMessages, params T[] classes)
		{
			if (classes.Length == 0)
			{
				UnityEngine.Debug.LogError("No have classes to check for null");

				return true;
			}

			bool oneMoreClassIsNull = false;

			foreach (var classExample in classes)
			{
				if (classExample is null)
				{
					if (debugMessages)
						UnityEngine.Debug.LogError($"{classExample} is null", notify);

					oneMoreClassIsNull = true;
				}
			}

			return oneMoreClassIsNull;
		}


		public static bool IsNullAll<T>(bool debugMessages, params T[] classes)
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
					if (debugMessages)
						UnityEngine.Debug.LogError($"{classExample} is null");

					oneMoreClassIsNull = true;
				}
			}

			return oneMoreClassIsNull;
		}


		public static bool IsNotNull<T>(T checkingClass, bool debugMessages)
		{
			if (checkingClass is null)
			{
				if (debugMessages)
					UnityEngine.Debug.LogError($"{checkingClass} is null");

				return false;
			}


			return true;
		}


		public static bool IsNotNull<T>(T checkingClass, GameObject notify, bool debugMessages)
		{
			if (checkingClass is null)
			{
				if (debugMessages)
					UnityEngine.Debug.LogError($"{checkingClass} is null", notify);

				return false;
			}


			return true;
		}


		//When we are click on the error - problem gameObject is showing in inspector
		public static bool IsNotNullAll<T>(GameObject notify, bool debugMessages, params T[] classes)
		{
			if (classes.Length == 0)
			{
				UnityEngine.Debug.LogError("No have classes to check for null");

				return false;
			}

			bool allClassesInNotNull = true;

			foreach (var classExample in classes)
			{
				if (classExample is null)
				{
					if (debugMessages)
						UnityEngine.Debug.LogError($"{classExample} is null", notify);

					allClassesInNotNull = false;
				}
			}

			return allClassesInNotNull;
		}


		public static bool IsNotNullAll<T>(bool debugMessages, params T[] classes)
		{
			if (classes.Length == 0)
			{
				UnityEngine.Debug.LogError("No have classes to check for null");

				return true;
			}

			bool allClassesInNotNull = true;

			foreach (var classExample in classes)
			{
				if (classExample is null)
				{
					if (debugMessages)
						UnityEngine.Debug.LogError($"{classExample} is null");

					allClassesInNotNull = false;
				}
			}

			return allClassesInNotNull;
		}
	}
}
