using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TankGame.Persistence;

namespace TankGame.Testing
{
	public class OperatorTesting : MonoBehaviour
	{
		void Start()
		{
			var first = new SerializableVector3( 1, 2, 3 );
			var second = new Vector3( 3, 2, 1 );

			Vector3 result = second + first;

			Debug.Log( result );
		}
	}
}
