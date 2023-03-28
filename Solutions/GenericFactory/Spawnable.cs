using System;
using _src.Scripts.Tutorial.Spawner;
using _src.Scripts.Tutorial.Waves.Spawner;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;


namespace _src.Scripts.Tutorial
{
	public class Spawnable : GenericFactory<Spawnable>
	{
		private UnityEvent<Spawnable> _onDestroyEvent = new();


		[SerializeField]
		private SpawnableObjectType _spawnableObjectType;
		

		public  UnityEvent<Spawnable> OnSpawnedObjectDestroyed() => _onDestroyEvent;


		public  SpawnableObjectType SpawnableObjectType
			=> _spawnableObjectType;


		private void OnDestroy()
		{
			_onDestroyEvent.Invoke(this);
		}
	}
}
