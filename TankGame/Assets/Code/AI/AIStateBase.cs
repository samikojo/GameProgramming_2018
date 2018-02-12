using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankGame.AI
{
	public enum AIStateType
	{
		Error = 0,
		Patrol = 1,
		FollowTarget = 2,
		Shoot = 3
	}

	public abstract class AIStateBase
	{
		// The state related to this object.
		public AIStateType State { get; protected set; }
		// The target states to which we can transition from this state.
		public IList<AIStateType> TargetStates { get; protected set; }
		// The owner Unit of this state (Unit is the state controller class)
		public EnemyUnit Owner { get; protected set; }

		protected AIStateBase()
		{
			TargetStates = new List<AIStateType>();
		}

		// A constructor which sets the Owner and State properties and calles
		// the default constructor.
		protected AIStateBase( EnemyUnit owner, AIStateType state )
			: this()
		{
			Owner = owner;
			State = state;
		}

		/// <summary>
		/// Add a valid state to which we can go from this state.
		/// </summary>
		/// <param name="targetState">The target state</param>
		/// <returns>True, if the state was added succesfully (not present in our 
		/// datastructure already). False otherwise.</returns>
		public bool AddTransition( AIStateType targetState )
		{
			// Use the extension method AddUnique to add a target state. Will return false
			// if the state was already added.
			return TargetStates.AddUnique( targetState );
		}

		/// <summary>
		/// Removes a target state from TargetStates data structure.
		/// </summary>
		/// <param name="targetState">The state to be removed.</param>
		/// <returns>True, if the target state was succesfully removed from the 
		/// data structure. False otherwise.</returns>
		public bool RemoveTransition( AIStateType targetState )
		{
			return TargetStates.Remove( targetState );
		}

		/// <summary>
		/// Checks if it is legal to go from this state to the target state.
		/// </summary>
		/// <param name="targetState">The target state to go to.</param>
		/// <returns>True, if the transition is legal, false otherwise.</returns>
		public virtual bool CheckTransition( AIStateType targetState )
		{
			return TargetStates.Contains( targetState );
		}

		/// <summary>
		/// Called just after the state is activated.
		/// </summary>
		public virtual void StateActivated()
		{
		}

		/// <summary>
		/// Called just before state is deactivated.
		/// </summary>
		public virtual void StateDeactivating()
		{
		}

		/// <summary>
		/// Called every frame the AI system is in this state.
		/// </summary>
		public abstract void Update();
	}
}
