using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankGame
{
	public class Weapon : MonoBehaviour
	{
		[SerializeField]
		private Projectile _projectilePrefab;

		private Pool< Projectile > _projectiles;
		private Unit _owner;

		public void Init( Unit owner )
		{
			_owner = owner;
			_projectiles = new Pool< Projectile >( 4, false, _projectilePrefab );
		}
	}
}
