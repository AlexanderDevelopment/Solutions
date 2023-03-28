using System;
using System.Collections.Generic;
using System.Threading;
using _src.Scripts.Tutorial.Waves.Spawner;
using Cysharp.Threading.Tasks;
using GameCreator.Stats;
using Sirenix.OdinInspector;
using Solutions.Serialization;
using UnityEngine;
using UnityEngine.Events;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;



namespace _src.Scripts.Tutorial.Spawner
{
	[Serializable]
	public class Spawner : ISpawner
	{
		[SerializeField]
		private List<SpawnBlock> _spawnBlocks;


		public List<SpawnBlock> SpawnBlocks
			=> _spawnBlocks;


		[HideInInspector]
		public UnityEvent<Spawnable> OnSpawnedObjectDestroyed = new();



		private async UniTask<bool> Timer(float time)
		{
			await UniTask.Delay(TimeSpan.FromSeconds(time));
			return true;
		}


		public void SpawnAll()
		{

			foreach (var block in _spawnBlocks)
			{
				block.OnSpawn.AddListener(spawnedObject =>
				{
					spawnedObject.OnSpawnedObjectDestroyed().AddListener(spawnableObject =>
					{
						OnSpawnedObjectDestroyed.Invoke(spawnableObject);
						spawnedObject.OnSpawnedObjectDestroyed().RemoveAllListeners();
					});
				});
			}
			
			foreach (var block in _spawnBlocks)
				block.SpawnAll();

			
		}


		public void DestroyAllSpawnedObjects()
		{
			foreach (var block in _spawnBlocks)
			{
				block.DestroyAllSpawnedObjectsByBlock();
				block.OnSpawn.RemoveAllListeners();
			}
				
		}
	}
}
