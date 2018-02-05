using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankGame
{
	public abstract class Unit : MonoBehaviour
	{
		[SerializeField]
		private float _moveSpeed;

		[SerializeField]
		private float _turnSpeed;

		private IMover _mover;

		public Weapon Weapon
		{
			get;
			protected set;
		}

		public IMover Mover { get { return _mover; } }

		protected void Awake()
		{
			Init();
		}

		public virtual void Init()
		{
			_mover = gameObject.GetOrAddComponent< TransformMover >();
			_mover.Init( _moveSpeed, _turnSpeed );

			Weapon = GetComponentInChildren< Weapon >();
			if ( Weapon != null )
			{
				Weapon.Init( this );
			}
		}

		public virtual void Clear()
		{
			
		}

		// An abstract method has to be defined in a non-abstract child class.
		protected abstract void Update();
	}
}
