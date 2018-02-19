using TankGame;

namespace TankGame
{
	public class EnemyHealth : Health
	{
		public EnemyHealth( Unit owner, int startingHealth )
			: base( owner, startingHealth )
		{
		}

		public override bool TakeDamage( int damage )
		{
			CurrentHealth = 0;
			// An event can't be fired from any other class than the one in which it is
			// declaired. Instead call a method which raises the event.
			RaiseUnitDiedEvent();
			return true;
		}
	}
}
