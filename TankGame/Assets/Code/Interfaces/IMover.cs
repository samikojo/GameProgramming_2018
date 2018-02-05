using UnityEngine;

namespace TankGame
{
	public interface IMover
	{
		/// <summary>
		/// Initializes the Mover
		/// </summary>
		/// <param name="moveSpeed">Move speed (units/s)</param>
		/// <param name="turnSpeed">Turn speed (degrees/s)</param>
		void Init( float moveSpeed, float turnSpeed );

		void Move( float amount );
		void Turn( float amount );
		// Moves to the direction 'direction'.
		void Move( Vector3 direction );
		// Turns towards the target point in the game world.
		void Turn( Vector3 target );
	}
}
