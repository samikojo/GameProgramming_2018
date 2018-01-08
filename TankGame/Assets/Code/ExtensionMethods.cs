using UnityEngine;
using System;

namespace TankGame
{
	public static class ExtensionMethods
	{
		public static TComponent GetOrAddComponent< TComponent >( this GameObject gameObject )
			where TComponent : Component
		{
			TComponent component = gameObject.GetComponent< TComponent >();
			if ( component == null )
			{
				component = gameObject.AddComponent< TComponent >();
			}
			return component;
		}

		public static Component GetOrAddComponent( this GameObject gameObject, Type type )
		{
			Component component = gameObject.GetComponent( type );
			if ( component == null )
			{
				component = gameObject.AddComponent( type );
			}
			return component;
		}
	}
}
