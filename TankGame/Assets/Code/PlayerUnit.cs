using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankGame
{
	public class PlayerUnit : Unit
	{
		[SerializeField]
		private string _horizontalAxis = "Horizontal";
		[SerializeField]
		[Tooltip("The name of the vertical axis")]
		private string _verticalAxis = "Vertical";

		protected override void Update()
		{
			var input = ReadInput();
			Mover.Turn( input.x );
			Mover.Move( input.z );

			// TODO: Refactor me! Extract method.
			bool shoot = Input.GetButton( "Fire1" );
			if ( shoot )
			{
				Weapon.Shoot();
			}
		}

		private Vector3 ReadInput()
		{
			float movement = Input.GetAxis( _verticalAxis );
			float turn = Input.GetAxis( _horizontalAxis );
			return new Vector3(turn, 0, movement);
		}
	}
}
