using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankGame.AI
{
	public class ShootState : AIStateBase
	{
		public ShootState( EnemyUnit owner )
			: base( owner, AIStateType.Shoot )
		{
			AddTransition( AIStateType.FollowTarget );
			AddTransition( AIStateType.Patrol );
		}

		public override void StateActivated()
		{
			base.StateActivated();
			// Start listening target's UnitDied event. When target dies, go back to the
			// patrol state.
			Owner.Target.Health.UnitDied += OnTargetDied;
		}

		private void OnTargetDied( Unit target )
		{
			Owner.PerformTransition( AIStateType.Patrol );
			Owner.Target = null;
		}


		public override void StateDeactivating()
		{
			base.StateDeactivating();
			Owner.Target.Health.UnitDied -= OnTargetDied;
		}

		public override void Update()
		{
			if ( !ChangeState() )
			{
				Owner.Mover.Turn( Owner.Target.transform.position );
				Owner.Weapon.Shoot();
			}
		}

		private bool ChangeState()
		{
			bool result = false;

			float distanceToTarget = Vector3.Distance( Owner.Target.transform.position,
				Owner.transform.position );
			if ( distanceToTarget > Owner.ShootingDistance )
			{
				Owner.PerformTransition( AIStateType.FollowTarget );
				result = true;
			}

			return result;
		}
	}
}
