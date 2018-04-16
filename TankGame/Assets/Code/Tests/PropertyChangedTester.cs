using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TankGame;
using UnityEngine;

public class PropertyChangedTester : MonoBehaviour
{
	private void Start()
	{
		var unit = gameObject.GetComponentInHierarchy< Unit >( true );
		unit.Health.PropertyChanged += HandlePropertyChanged;
	}

	private void HandlePropertyChanged( object sender, PropertyChangedEventArgs e )
	{
		Debug.Log( string.Format("Property {0} changed", e.PropertyName) );
	}
}
