using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace TankGame.UI
{
	public class HealthUIItem : MonoBehaviour
	{
		// A reference to the unit which health this component draws to the UI.
		private Unit _unit;

		// The component which draws the text to the UI.
		private Text _text;

		public bool IsEnemy { get { return _unit != null && _unit is EnemyUnit; } }

		protected void OnDestroy()
		{
			UnregisterEventListeners();
		}

		public void Init( Unit unit )
		{
			_unit = unit;
			_text = GetComponentInChildren< Text >();
			// If the unit is an enemy unit the color of the text will be set to red.
			// Green text will be used otherwise.
			_text.color = IsEnemy ? Color.red : Color.green;
			_unit.Health.HealthChanged += OnUnitHealthChanged;
			_unit.Health.UnitDied += OnUnitDied;
			SetText( _unit.Health.CurrentHealth );
		}

		private void OnUnitDied( Unit obj )
		{
			UnregisterEventListeners();
		}

		private void UnregisterEventListeners()
		{
			_unit.Health.HealthChanged -= OnUnitHealthChanged;
			_unit.Health.UnitDied -= OnUnitDied;
		}

		private void OnUnitHealthChanged( Unit unit, int health )
		{
			SetText( health );
		}

		private void SetText( int health )
		{
			// C# 6 syntax for the same thing.
			//_text.text = $"{_unit.name} health: {health}";
			_text.text = string.Format( "{0} health: {1}", _unit.name, health );
		}
	}
}
