using UnityEngine;


namespace _src.Scripts.Tutorial.Spawner
{
	public class GenericFactory<T> : MonoBehaviour where T : MonoBehaviour
	{
		[SerializeField]
		private T _prefab;


		public T GetNewInstance(Vector3 position)
			=> Instantiate(_prefab, position, Quaternion.identity);
	}
}
