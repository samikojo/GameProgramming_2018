using UnityEngine;
using System.Collections;

namespace TankGame.WaypointSystem
{
	public class Waypoint : MonoBehaviour
	{
		public Vector3 Position
		{
			get { return transform.position; }
		}
	}
}
