using UnityEngine;

namespace TankGame
{
	public class Projectile : MonoBehaviour
	{
		[SerializeField]
		private float _damage;

		[SerializeField]
		private float _shootingForce;

		[SerializeField]
		private float _explosionForce;

		[SerializeField]
		private float _explosionRadius;
		
		private Rigidbody _rigidbody;
		private System.Action<Projectile> _collisionCallback;

		// Self initializing property. Gets the reference to the Rigidbody component when
		// used the first time.
		public Rigidbody Rigidbody
		{
			get
			{
				if ( _rigidbody == null )
				{
					_rigidbody = gameObject.GetOrAddComponent< Rigidbody >();
				}
				return _rigidbody;
			}
		}

		public void Init( System.Action< Projectile > collisionCallback )
		{
			_collisionCallback = collisionCallback;
		}

		public void Launch( Vector3 direction )
		{
			// TODO: Add particle effects.
			Rigidbody.AddForce( direction.normalized * _shootingForce, ForceMode.Impulse );
		}

		protected void OnCollisionEnter( Collision collision )
		{
			// TODO: Add particle effects.
			// TODO: Apply damage to enemies.
			Rigidbody.velocity = Vector3.zero;
			_collisionCallback( this );
		}

	}
}
