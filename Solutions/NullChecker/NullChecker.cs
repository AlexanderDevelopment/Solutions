public static class NullChecker
	{
		public static bool CheckForNull<T>(params T[] classes)
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
