using UnityEngine;
using System;
using System.Collections.Generic;

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

		/// <summary>
		/// Gets the component the GameObject or from active and inactive parents.
		/// </summary>
		/// <typeparam name="TComponent">The type of the component we are trying to 
		/// get. Has to be derived from UnityEngine.Component.</typeparam>
		/// <param name="gameObject">The GameObject this method extends.</param>
		/// <returns>A reference to the component of type TComponent in this
		/// GameObject or in one of its parents. Null if no component is found
		/// in parent hierarchy.</returns>
		public static TComponent GetComponentInInactiveParents< TComponent >
			( this GameObject gameObject )
			where TComponent : Component
		{
			return gameObject.GetComponentInInactiveParentsRecursive< TComponent >();
			//return gameObject.GetComponentInInactiveParentsIterative< TComponent >();
		}

		/// <summary>
		/// Adds an item to the list only if it doesn't exist on the list already.
		/// </summary>
		/// <typeparam name="T">The type of the item</typeparam>
		/// <param name="list">The list the item should be added to.</param>
		/// <param name="item">The item to be added to the list.</param>
		/// <returns>True, if the item was not on a list before and was 
		/// added to the list successfully. False otherwise.</returns>
		public static bool AddUnique< T >( this IList< T > list, T item )
		{
			if ( list.Contains( item ) )
			{
				return false;
			}

			list.Add( item );
			return true;
		}

		private static TComponent GetComponentInInactiveParentsIterative< TComponent >
			( this GameObject gameObject )
			where TComponent : Component
		{
			TComponent result;
			Transform transform = gameObject.transform;
			do
			{
				result = transform.GetComponent< TComponent >();
				transform = transform.parent;
			} while ( result == null && transform != null );

			return result;
		}

		private static TComponent GetComponentInInactiveParentsRecursive< TComponent >
			( this GameObject gameObject )
			where TComponent : Component
		{
			TComponent result = gameObject.GetComponent< TComponent >();
			if ( result == null )
			{
				Transform parentTransform = gameObject.transform.parent;
				if ( parentTransform != null )
				{
					result = parentTransform.gameObject.
						GetComponentInInactiveParentsRecursive< TComponent >();
				}
			}

			return result;
		}

		/// <summary>
		/// Searches the component of type TComponent from children and parents.
		/// </summary>
		/// <typeparam name="TComponent">The type of the component we are trying to 
		/// get. Has to be derived from UnityEngine.Component.</typeparam>
		/// <param name="gameObject">The GameObject this method extends.</param>
		/// <param name="includeInactive">Should the object be looked also from
		/// inactive children and parents or not.</param>
		/// <returns>A reference to the component of type TComponent in this
		/// GameObject or in one children or in one of its parents.
		/// Null if no component is found from the hierarchy.</returns>
		public static TComponent GetComponentInHierarchy< TComponent >
			( this GameObject gameObject, bool includeInactive = false )
			where TComponent : Component
		{
			return includeInactive
				? gameObject.GetComponentInInactiveHierarchy< TComponent >()
				: gameObject.GetComponentInActiveHierarchy< TComponent >();
		}

		private static TComponent GetComponentInActiveHierarchy< TComponent >
			( this GameObject gameObject )
			where TComponent : Component
		{
			TComponent result =
				gameObject.GetComponentInChildren< TComponent >( includeInactive: false );
			if ( result == null )
			{
				result = gameObject.GetComponentInParent< TComponent >();
			}
			return result;
		}

		private static TComponent GetComponentInInactiveHierarchy< TComponent >
			( this GameObject gameObject )
			where TComponent : Component
		{
			TComponent result =
				gameObject.GetComponentInChildren< TComponent >( includeInactive: true );
			if ( result == null )
			{
				result = gameObject.GetComponentInInactiveParents< TComponent >();
			}

			return result;
		}
	}
}
