using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JetBrains.Annotations;
using UnityEngine;
using TankGame.AI;
using TankGame.WaypointSystem;

namespace TankGame
{
	public class EnemyUnit : Unit
	{
		[SerializeField]
		private float _detectEnemyDistance;

		[SerializeField]
		private float _shootingDistance;

		[SerializeField]
		private Path _path;

		[SerializeField]
		private float _waypointArriveDistance;

		[SerializeField]
		private Direction _direction;

		private IList<AIStateBase> _states = new List< AIStateBase >();

		public AIStateBase CurrentState { get; private set; }
		// How far the enemy can "see" the player.
		public float DetectEnemyDistance { get { return _detectEnemyDistance; } }
		// The distance the enemy shoots the player.
		public float ShootingDistance { get { return _shootingDistance; } }
		// The player unit this enemy is trying to shoot at.
		public PlayerUnit Target { get; set; }
		
		public Vector3? ToTargetVector
		{
			get
			{
				if ( Target != null )
				{
					return Target.transform.position - transform.position;
				}
				return null;
			}
		}

		public override void Init()
		{
			// Runs the base classes implementation of the Init method. Initializes Mover and 
			// Weapon.
			base.Init();
			// Initializes the state system.
			InitStates();
		}

		private void InitStates()
		{
			PatrolState patrol = 
				new PatrolState( this, _path, _direction, _waypointArriveDistance );
			_states.Add( patrol );

			FollowTargetState followTarget = new FollowTargetState( this );
			_states.Add( followTarget );

			CurrentState = patrol;
			CurrentState.StateActivated();
		}

		protected override void Update()
		{
			CurrentState.Update();
		}

		public bool PerformTransition( AIStateType targetState )
		{
			if ( !CurrentState.CheckTransition( targetState ) )
			{
				return false;
			}

			bool result = false;

			AIStateBase state = GetStateByType( targetState );
			if ( state != null )
			{
				CurrentState.StateDeactivating();
				CurrentState = state;
				CurrentState.StateActivated();
				result = true;
			}

			return result;
		}

		private AIStateBase GetStateByType( AIStateType stateType )
		{
			// Returns the first object from the list _states which State property's value
			// equals to stateType. If no object is found, returns null.
			return _states.FirstOrDefault( state => state.State == stateType );
			
			// Foreach version of the same thing.
			//foreach ( AIStateBase state in _states )
			//{
			//	if ( state.State == stateType )
			//	{
			//		return state;
			//	}
			//}
			//return null;
		}
	}
}
