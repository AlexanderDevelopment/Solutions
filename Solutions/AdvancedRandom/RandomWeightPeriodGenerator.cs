using System.Collections.Generic;


namespace Toc.Scripts
{
	public class RandomWeightPeriodGenerator<T>
	{
		struct RandomWeight
		{
			public float Min;
			public float Max;
			public T Result;


			public bool Check(float val) 
				=> (val >= Min && val < Max);
		}


		private float _currentMax = 0f;
		private readonly List<RandomWeight> _weightsList = new ();
		private readonly RandomTools _rnd = new ();


		public void Add(float weight, T obj)
		{
			RandomWeight newWeight = new RandomWeight();
			newWeight.Result = obj;
			newWeight.Min = _currentMax;
			_currentMax = newWeight.Max = _currentMax + weight;
			_weightsList.Add(newWeight);
		}


		public T GetRandomObject()
		{
			float rndVal = _rnd.Range(0f, _currentMax - 0.0000001f);

			foreach (var w in _weightsList)
				if (w.Check(rndVal))
					return w.Result;
			
			return default(T);
		}


		public int Count() 
			=> _weightsList.Count;
	}
}
