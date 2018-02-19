using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace TankGame
{
	public class Health
	{
		public event Action< Unit > UnitDied;

		public int CurrentHealth { get; protected set; }
		public Unit Owner { get; private set; }

		public Health( Unit owner, int startingHealth )
		{
			Owner = owner;
			CurrentHealth = startingHealth;
		}

		/// <summary>
		/// Applies damage to the Unit.
		/// </summary>
		/// <param name="damage">Amount of damage</param>
		/// <returns>True, if the unit dies. False otherwise</returns>
		public virtual bool TakeDamage( int damage )
		{
			CurrentHealth = Mathf.Clamp( CurrentHealth - damage, 0, CurrentHealth );
			bool didDie = CurrentHealth == 0;
			if ( didDie )
			{
				RaiseUnitDiedEvent();
			}
			return didDie;
		}

		protected void RaiseUnitDiedEvent()
		{
			if ( UnitDied != null )
			{
				UnitDied( Owner );
			}
		}
	}
}
