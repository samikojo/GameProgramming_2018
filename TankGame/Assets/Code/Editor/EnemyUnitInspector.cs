using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace TankGame.Editor
{
	[CustomEditor(typeof(EnemyUnit))]
	public class EnemyUnitInspector : UnitInspector
	{
		private EnemyUnit _target;

		private int _damageAmount = 10;

		protected override void OnEnable()
		{
			base.OnEnable();
			_target = target as EnemyUnit;
		}

		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();
			// This syntax will be available in C# 6 (the experimental runtime of Unity)
			//string text = $"Take {_damageAmount} damage";

			// When application is not playing, elements defined below GUI.enabled line
			// are disabled.
			bool oldEnabled = GUI.enabled;
			GUI.enabled = Application.isPlaying;
			GUILayout.Label( "Provide damage to the unit", EditorStyles.boldLabel );
			_damageAmount = EditorGUILayout.IntField("Damage amount", _damageAmount);
			if ( GUILayout.Button( string.Format( "Take {0} damage", _damageAmount ) ) )
			{
				_target.TakeDamage( _damageAmount );
			}
			GUI.enabled = oldEnabled;
		}
	}
}
