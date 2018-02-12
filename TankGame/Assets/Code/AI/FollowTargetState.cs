using UnityEngine;

namespace TankGame.AI
{
	public class FollowTargetState : AIStateBase
	{
		public float SqrShootingDistance
		{
			get { return Owner.ShootingDistance * Owner.ShootingDistance; }
		}

		public float SqrDetectEnemyDistance
		{
			get { return Owner.DetectEnemyDistance * Owner.DetectEnemyDistance; }
		}

		public FollowTargetState( EnemyUnit owner )
			: base( owner, AIStateType.FollowTarget )
		{
			AddTransition( AIStateType.Patrol );
			AddTransition( AIStateType.Shoot );
		}

		public override void Update()
		{
			if ( !ChangeState() )
			{
				Owner.Mover.Move( Owner.transform.forward );
				Owner.Mover.Turn( Owner.Target.transform.position );
			}
		}

		private bool ChangeState()
		{
			// 1. Are we at the shooting range?
			// If yes, go to shoot state.
			Vector3 toPlayerVector =
				Owner.transform.position - Owner.Target.transform.position;
			float sqrDistanceToPlayer = toPlayerVector.sqrMagnitude;
			if ( sqrDistanceToPlayer < SqrShootingDistance )
			{
				return Owner.PerformTransition( AIStateType.Shoot );
			}

			// 2. Did the player get away?
			// If yes, go to patrol state.
			if ( sqrDistanceToPlayer > SqrDetectEnemyDistance )
			{
				Owner.Target = null;
				return Owner.PerformTransition( AIStateType.Patrol );
			}

			// Otherwise return false.
			return false;
		}
	}
}
