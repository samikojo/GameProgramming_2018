using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace TankGame.Editor
{
	[CustomEditor(typeof(Projectile))]
	public class ProjectileInspector : UnityEditor.Editor
	{
		private const string HitMaskName = "_hitMask";
		private SerializedProperty _hitMaskProperty;

		protected void OnEnable()
		{
			_hitMaskProperty = serializedObject.FindProperty( HitMaskName );
		}

		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();

			EditorGUILayout.BeginVertical();

			List<string> labels = new List< string >(32);
			for ( int i = 0; i < 32; ++i )
			{
				labels.Add( LayerMask.LayerToName( i ) );
			}

			_hitMaskProperty.intValue = EditorGUILayout.MaskField( "Hit layers",
				_hitMaskProperty.intValue, labels.ToArray() );

			serializedObject.ApplyModifiedProperties();

			EditorGUILayout.EndVertical();
		}
	}
}
