using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace TankGame.UI
{
	public class HealthUI : MonoBehaviour
	{
		[SerializeField]
		private HealthUIItem _healthUIItemPrefab;

		public void Init()
		{
			Debug.Log( "Health UI initialized" );
		}

		public void AddUnit( Unit unit )
		{
			var healthItem = Instantiate( _healthUIItemPrefab, transform );
			healthItem.Init( unit );
			healthItem.gameObject.SetActive( true );
		}
	}
}
