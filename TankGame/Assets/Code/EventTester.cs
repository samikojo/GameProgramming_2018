using System.Collections;
using System.Collections.Generic;
using TankGame;
using UnityEngine;

public class EventTester : MonoBehaviour
{
	public EnemyUnit Unit;

	void Start()
	{
		Unit.Health.UnitDied += OnUnitDied;
	}

	void OnDestroy()
	{
		Unit.Health.UnitDied -= OnUnitDied;
	}

	private void OnUnitDied( Unit obj )
	{
		Debug.Log( "Enemy died" );
		Destroy( gameObject );
	}
}
