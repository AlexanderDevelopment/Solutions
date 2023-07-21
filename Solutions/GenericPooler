using System.Collections.Generic;
using System.Linq;
using UnityEngine;
	public class Pooler<T> where T : Component
	{
		private int _poolSize;
		private List<T> _poolCache;


		public Pooler(int poolSize)
		{
			_poolSize = poolSize;
			_poolCache = new List<T>(_poolSize);

			Initialisation();
		}


		private void Initialisation()
		{
			for (int i = 0; i < _poolSize; i++)
			{
				GameObject newObj = new GameObject(typeof(T).Name + "_" + (i + 1));
				T component = newObj.AddComponent<T>();
				_poolCache.Add(component);
			}
		}


		public T GetFromPool()
		{
			foreach (var component in _poolCache.Where(component => !component.gameObject.activeInHierarchy))
			{
				component.gameObject.SetActive(true);
				return component;
			}
			return null;
		}


		public void ReturnToPool(T component, int index)
		{
			if (index < 0 || index >= _poolSize)

				UnityEngine.Debug.LogError("Index out of bounds");

			component.gameObject.SetActive(false);
		}
	}
