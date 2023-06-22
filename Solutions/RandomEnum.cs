public static class RandomEnum
	{
		private static Random _Random = new Random(Environment.TickCount);


		public static T Of<T>()
		{
			if (!typeof(T).IsEnum)
				throw new InvalidOperationException("Must use Enum type");

			Array enumValues = Enum.GetValues(typeof(T));

			return (T)enumValues.GetValue(_Random.Next(enumValues.Length));
		}
	}
