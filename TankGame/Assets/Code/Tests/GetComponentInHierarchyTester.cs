using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankGame.Testing
{
	public class GetComponentInHierarchyTester : MonoBehaviour
	{
		private bool _includeInactive;

		public void Setup( bool includeInactive, bool componentInParent, bool setActive )
		{
			_includeInactive = includeInactive;
			GameObject go;
			if ( componentInParent )
			{
				go = transform.parent.gameObject;
			}
			else
			{
				go = transform.GetChild( 0 ).gameObject;
			}

			go.AddComponent< TestComponent >();
			go.SetActive( setActive );
		}

		public TestComponent Run()
		{
			return gameObject.GetComponentInHierarchy< TestComponent >( _includeInactive );
		}
	}
}
