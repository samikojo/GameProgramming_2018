using TankGame.Localization;
using TankGame.Messaging;
using UnityEngine;
using UnityEngine.UI;
using l10n = TankGame.Localization.Localization;

namespace TankGame.UI
{
	public class HealthUIItem : MonoBehaviour
	{
		// A reference to the unit which health this component draws to the UI.
		private Unit _unit;

		// The component which draws the text to the UI.
		private Text _text;

		private const string HealthKey = "health";

		private ISubscription< UnitDiedMessage > _unitDiedSubscription;

		public bool IsEnemy
		{
			get { return _unit != null && _unit is EnemyUnit; }
		}

		protected void OnDestroy()
		{
			UnregisterEventListeners();
		}

		public void Init( Unit unit )
		{
			l10n.LanguageLoaded += OnLanguageLoaded;
			_unit = unit;
			_text = GetComponentInChildren< Text >();
			// If the unit is an enemy unit the color of the text will be set to red.
			// Green text will be used otherwise.
			_text.color = IsEnemy ? Color.red : Color.green;
			_unit.Health.HealthChanged += OnUnitHealthChanged;
			//_unit.Health.UnitDied += OnUnitDied;
			_unitDiedSubscription =
				GameManager.Instance.MessageBus.Subscribe< UnitDiedMessage >( OnUnitDied );
			SetText( _unit.Health.CurrentHealth );
		}

		private void OnLanguageLoaded( LangCode currentLang )
		{
			SetText( _unit.Health.CurrentHealth );
		}

		private void OnUnitDied( UnitDiedMessage msg )
		{
			if ( msg.DeadUnit == _unit )
			{
				UnregisterEventListeners();
				gameObject.SetActive( false );
			}
		}

		//private void OnUnitDied(Unit obj)
		//{
		//	UnregisterEventListeners();
		//}

		private void UnregisterEventListeners()
		{
			l10n.LanguageLoaded -= OnLanguageLoaded;
			_unit.Health.HealthChanged -= OnUnitHealthChanged;
			if ( !GameManager.IsClosing )
				GameManager.Instance.MessageBus.Unsubscribe( _unitDiedSubscription );
			//_unit.Health.UnitDied -= OnUnitDied;
		}

		private void OnUnitHealthChanged( Unit unit, int health )
		{
			SetText( health );
		}

		private void SetText( int health )
		{
			// C# 6 syntax for the same thing.
			//_text.text = $"{_unit.name} health: {health}";

			string translation = l10n.CurrentLanguage.GetTranslation( HealthKey );

			string unitKey = IsEnemy ? "enemy" : "player";
			string unitTranslation = l10n.CurrentLanguage.GetTranslation( unitKey );

			_text.text = string.Format( translation, unitTranslation, health );
		}
	}
}
