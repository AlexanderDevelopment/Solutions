using System;
using System.Collections.Generic;
using _src.Scripts.Tutorial;
using _src.Scripts.Tutorial.Spawner;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;


namespace _src.Scripts.Tutorial.Waves.Spawner
{
	[Serializable]
	public class SpawnBlock
	{
		[SerializeField, Required]
		private List<SpawnPoint> _spawnPoints = new();


		[SerializeField, Required, AssetsOnly]
		private List<Spawnable> _spawnableObjects = new();


		[SerializeField]
		private bool _respawn;


		[SerializeField, ShowIf("_respawn")]
		private float _spawnReloadDuration;


		[SerializeField, ReadOnly]
		private List<Spawnable> _spawnedObjects;


		public bool IsSpawnedObjectsDestroyed
			=> _spawnableObjects.Count == 0;


		[HideInInspector]
		public UnityEvent<Spawnable> OnSpawn = new();


		private bool _respawnIsActive;


		public void SpawnAll()
		{
			_respawnIsActive = true;

			foreach (var spawnPoint in _spawnPoints)
				SpawnObject(PickRandomObject(), spawnPoint);
		}


		private async UniTask Respawn(Spawnable spawnableObject, SpawnPoint spawnPoint)
		{
			await UniTask.Delay(TimeSpan.FromSeconds(_spawnReloadDuration));

			if (_respawnIsActive)
				SpawnObject(PickRandomObject(), spawnPoint);
		}


		private void SpawnObject(Spawnable spawnableObject, SpawnPoint spawnPoint)
		{
			var newSpawnableObject = spawnableObject.GetNewInstance(spawnPoint.transform.position);
			_spawnedObjects.Add(newSpawnableObject);
			spawnPoint.PlaySpawnFeedback();
			OnSpawn.Invoke(newSpawnableObject);

			if (_respawn)
				newSpawnableObject.OnSpawnedObjectDestroyed().AddListener(spawnable =>
					{
						_spawnedObjects.Remove(spawnableObject);
						spawnableObject.OnSpawnedObjectDestroyed().RemoveAllListeners();

						if (_respawn)
							Respawn(spawnable, spawnPoint).Forget();
					}
				);
		}


		private Spawnable PickRandomObject()
			=> _spawnableObjects[Random.Range(0, _spawnableObjects.Count)];


		public void DestroyAllSpawnedObjectsByBlock()
		{
			_respawnIsActive = false;

			foreach (var spawnedObject in _spawnedObjects)
			{
				if (spawnedObject)
				{
					spawnedObject.OnSpawnedObjectDestroyed().RemoveAllListeners();
					Object.Destroy(spawnedObject.gameObject);
				}
			}

			_spawnedObjects.Clear();
		}


		public void AddSpawnPoints(SpawnPoint spawnPoint)
		{
			_spawnPoints.RemoveAll(item => item is null);

			if (!_spawnPoints.Contains(spawnPoint))
				_spawnPoints.Add(spawnPoint);
		}
	}
}
