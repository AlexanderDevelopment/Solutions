using System;
using MoreMountains.Feedbacks;
using Sirenix.OdinInspector;
using UnityEngine;


namespace _src.Scripts.Tutorial.Spawner
{
	public class SpawnPoint : MonoBehaviour
	{
		[SerializeField, Required, ChildGameObjectsOnly]
		private MMF_Player _mmfPlayer;


		[SerializeField, Required]
		[ColorPalette("Breeze")]
		private Color _gizmosColor = Color.magenta;


		[SerializeField]
		[Range(0, 5)]
		private float _radius = 1;

		public void PlaySpawnFeedback()
			=> _mmfPlayer.PlayFeedbacks();
		
		private void OnDrawGizmos()
		{
			Gizmos.color = _gizmosColor;
			Gizmos.DrawSphere(transform.position, _radius);
		}


		[Button("PutOnGround")]
		private void PutOnGround()
		{
			Ray ray = new Ray(this.transform.position, Vector3.down * 100);

			if (Physics.Raycast(ray, out RaycastHit hit ))
				transform.position = hit.point;
		}
	}
}
