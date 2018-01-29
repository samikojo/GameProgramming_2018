using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using TankGame.AI;

namespace TankGame
{
	public class EnemyUnit : Unit
	{
		private IList<AIStateBase> _states = new List< AIStateBase >();

		public AIStateBase CurrentState { get; private set; }

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
			// TODO: Implement me!
		}

		protected override void Update()
		{
			// TODO: Remove this.
			return;

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
				CurrentState.StateDecativating();
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
