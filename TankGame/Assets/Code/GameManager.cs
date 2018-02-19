using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace TankGame
{
	public class GameManager : MonoBehaviour
	{
		private static GameManager _instance;

		public static GameManager Instance
		{
			get
			{
				if ( _instance == null )
				{
					GameObject gameManagerObject = new GameObject(typeof(GameManager).Name);
					_instance = gameManagerObject.AddComponent< GameManager >();
				}
				return _instance;
			}
		}

		private List<Unit> _enemyUnit = new List< Unit >();
		private Unit _playerUnit = null;

		protected void Awake()
		{
			if ( _instance == null )
			{
				_instance = this;
			}
			else if ( _instance != this )
			{
				Destroy( gameObject );
				return;
			}

			Init();
		}

		private void Init()
		{
			Unit[] allUnits = FindObjectsOfType< Unit >();
			foreach ( Unit unit in allUnits )
			{
				AddUnit(unit);
			}
		}

		public void AddUnit(Unit unit)
		{
			if (unit is EnemyUnit)
			{
				_enemyUnit.Add(unit);
			}
			// Adding a player unit after the initialization really makes no sense because
			// we can have a reference to only one player unit. Be carefull with this
			else if (unit is PlayerUnit)
			{
				_playerUnit = unit;
			}
		}
	}
}
