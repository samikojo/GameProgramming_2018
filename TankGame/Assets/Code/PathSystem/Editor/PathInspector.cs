using TankGame.WaypointSystem;
using UnityEngine;
using UE = UnityEditor;

namespace TankGame.Editor
{
	[UE.CustomEditor( typeof( Path ) )]
	public class PathInspector : UE.Editor
	{
		private Path _target;

		protected void OnEnable()
		{
			_target = target as Path;
		}

		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();

			if ( GUILayout.Button( "Add waypoint" ) )
			{
				int waypointCount = _target.transform.childCount;
				string waypointName =
					string.Format( "Waypoint{0}", ( waypointCount + 1 ).ToString( "D3" ) );
				GameObject waypoint = new GameObject( waypointName );
				waypoint.AddComponent< Waypoint >();
				waypoint.transform.SetParent( _target.transform );
			}
		}
	}
}
