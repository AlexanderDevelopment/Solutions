using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace _src.Scripts.OpenUnitySolutions.GenericPooler
{
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
			GameObject newPooler = new GameObject($"{typeof(T)} pooler");

			for (int i = 0; i < _poolSize; i++)
			{
				GameObject newObj = new GameObject(typeof(T).Name + "_" + (i + 1));
				T component = newObj.AddComponent<T>();
				newObj.gameObject.transform.SetParent(newPooler.transform);
				_poolCache.Add(component);
			}
		}


		public T GetFromPool()
		{
			foreach (var component in _poolCache.Where(component => !component.gameObject.activeInHierarchy))
			{
				component.gameObject.SetActive(true);
				component.gameObject.transform.position = Vector3.zero;
				return component;
			}

			return null;
		}
		
		public T GetFromPool(Vector3 position)
		{
			foreach (var component in _poolCache.Where(component => !component.gameObject.activeInHierarchy))
			{
				component.gameObject.SetActive(true);
				component.gameObject.transform.position = position;
				return component;
			}

			return null;
		}


		public void ReturnToPool(T component)
		{
			if (_poolCache.Contains(component))
				component.gameObject.SetActive(false);
			else
				UnityEngine.Debug.LogError("This component not be initialized in Pooler", component.gameObject);
		}
	}
}
