using Sirenix.OdinInspector;
using UnityEngine;

	public enum GizmosColliderType
	{
		BoxCollider = 0,
		SphereCollider = 1,
	}


	public enum GizmosColliderWireless
	{
		Wired = 0,
		NotWired = 1,
	}
	
	public class ColliderGizmos : MonoBehaviour
	{
		[SerializeField]
		[BoxGroup("Debug gizmos settings")]
		private Color _gizmosColor = Color.cyan;


		[SerializeField]
		[BoxGroup("Debug gizmos settings")]
		[OnValueChanged("ChangeColliderCenter")]
		private Vector3 _colliderCenter;


		[SerializeField]
		[BoxGroup("Debug gizmos settings")]
		private GizmosColliderWireless _colliderWireless = GizmosColliderWireless.Wired;


		[SerializeField]
		[OnValueChanged("ChangeColliderType")]
		[BoxGroup("Debug gizmos settings")]
		[EnumToggleButtons]
		private GizmosColliderType _gizmosColliderType = GizmosColliderType.BoxCollider;

		[SerializeField]
		[ShowIf("_gizmosColliderType", GizmosColliderType.BoxCollider)]
		[OnValueChanged("ChangeColliderSize")]
		[BoxGroup("Debug gizmos settings")]
		private Vector3 _colliderSize;

		[SerializeField]
		[ShowIf("_gizmosColliderType", GizmosColliderType.SphereCollider)]
		[OnValueChanged("ChangeColliderSize")]
		[BoxGroup("Debug gizmos settings")]	
		private float _colliderRadius;


		private Collider _collider;


		[OnInspectorInit]
		public void Initialize()
		{
			if (!_collider)
			{
				_collider = gameObject.AddComponent<BoxCollider>();
				_collider.isTrigger = true;
				_gizmosColliderType = GizmosColliderType.BoxCollider;
			}
		}


		protected void ChangeColliderSize()
		{
			if (!_collider)
				return;

			if (_collider is BoxCollider boxCollider)
			{
				boxCollider.size = _colliderSize;
			}

			if (_collider is SphereCollider sphereCollider)
			{
				sphereCollider.radius = _colliderRadius;
			}
		}


		protected void ChangeColliderCenter()
		{
			if (!_collider)
				return;

			if (_collider is BoxCollider boxCollider)
			{
				boxCollider.center = _colliderCenter;
			}

			if (_collider is SphereCollider sphereCollider)
			{
				sphereCollider.center = _colliderCenter;
			}
		}


		protected void ChangeColliderType()
		{
			gameObject.TryGetComponent(out Collider currentCollider);

			if (currentCollider)
			{
				DestroyImmediate(currentCollider);
			}

			if (_gizmosColliderType == GizmosColliderType.BoxCollider)
			{
				_collider = gameObject.AddComponent<BoxCollider>();
				_colliderSize = Vector3.one;
			}

			if (_gizmosColliderType == GizmosColliderType.SphereCollider)
			{
				_collider = gameObject.AddComponent<SphereCollider>();
				_colliderRadius = 0.5f;
			}

			_collider.isTrigger = true;
		}


		public void OnDrawGizmosSelected()
		{
			if (_collider != null)
			{
				Gizmos.color = _gizmosColor;

				if (_colliderWireless == GizmosColliderWireless.NotWired)
				{
					if (_collider is BoxCollider boxCollider)
					{
						Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, transform.lossyScale);
						Gizmos.DrawCube(boxCollider.center, boxCollider.size);
					}
					else if (_collider is SphereCollider sphereCollider)
					{
						Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, transform.lossyScale);
						Gizmos.DrawSphere(sphereCollider.center, sphereCollider.radius);
					}
				}
				else
				{
					if (_collider is BoxCollider boxCollider)
					{
						Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, transform.lossyScale);
						Gizmos.DrawWireCube(boxCollider.center, boxCollider.size);
					}
					else if (_collider is SphereCollider sphereCollider)
					{
						Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, transform.lossyScale);
						Gizmos.DrawWireSphere(sphereCollider.center, sphereCollider.radius);
					}
				}
			}
		}
	}
