using TankGame.Systems;
using TankGame.WaypointSystem;
using UnityEngine;

namespace TankGame.AI
{
	public class PatrolState : AIStateBase
	{
		private Path _path;
		private Direction _direction;
		private float _arriveDistance;

		public Waypoint CurrentWaypoint { get; private set; }

		public PatrolState( EnemyUnit owner, Path path,
			Direction direction, float arriveDistance )
			: base()
		{
			State = AIStateType.Patrol;
			Owner = owner;
			AddTransition( AIStateType.FollowTarget );
			_path = path;
			_direction = direction;
			_arriveDistance = arriveDistance;
		}

		public override void StateActivated()
		{
			base.StateActivated();
			CurrentWaypoint = _path.GetClosestWaypoint( Owner.transform.position );
		}

		public override void Update()
		{
			// 1. Should we change the state?
			//   1.1 If yes, change state and return.

			if ( !ChangeState() )
			{
				// 2. Are we close enough the current waypoint?
				//   2.1 If yes, get the next waypoint
				CurrentWaypoint = GetWaypoint();
				// 3. Move towards the current waypoint
				Owner.Mover.Move( Owner.transform.forward );
				// 4. Rotate towards the current waypoint
				Owner.Mover.Turn( CurrentWaypoint.Position );
			}
		}

		private Waypoint GetWaypoint()
		{
			Waypoint result = CurrentWaypoint;
			Vector3 toWaypointVector = CurrentWaypoint.Position - Owner.transform.position;
			float toWaypointSqr = toWaypointVector.sqrMagnitude;
			float sqrArriveDistance = _arriveDistance * _arriveDistance;
			if ( toWaypointSqr <= sqrArriveDistance )
			{
				result = _path.GetNextWaypoint( CurrentWaypoint, ref _direction );
			}

			return result;
		}

		private bool ChangeState()
		{
			//int mask = LayerMask.GetMask( "Player" );
			int playerLayer = LayerMask.NameToLayer( "Player" );
			int mask = Flags.CreateMask( playerLayer );

			Collider[] players = Physics.OverlapSphere( Owner.transform.position,
				Owner.DetectEnemyDistance, mask );
			if ( players.Length > 0 )
			{
				PlayerUnit player =
					players[ 0 ].gameObject.GetComponentInHierarchy< PlayerUnit >();

				if ( player != null )
				{
					Owner.Target = player;
					float sqrDistanceToPlayer = Owner.ToTargetVector.Value.sqrMagnitude;
					if ( sqrDistanceToPlayer <
					     Owner.DetectEnemyDistance * Owner.DetectEnemyDistance )
					{
						return Owner.PerformTransition( AIStateType.FollowTarget );
					}

					Owner.Target = null;
				}
			}
			return false;
		}
	}
}
