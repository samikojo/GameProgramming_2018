using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TankGame.Persistence;
using TankGame.Messaging;

namespace TankGame
{
	public abstract class Unit : MonoBehaviour, IDamageReceiver
	{
		#region Statics

		private static int s_idCounter = 0;

		public static int GetNextId()
		{
			Unit[] allUnits = FindObjectsOfType< Unit >();
			foreach ( var unit in allUnits )
			{
				if ( unit.Id >= s_idCounter )
				{
					s_idCounter = unit.Id + 1;
				}
			}

			return s_idCounter++;
		}

		#endregion Statics

		[SerializeField]
		private float _moveSpeed;

		[SerializeField]
		private float _turnSpeed;

		[SerializeField]
		private int _startingHealth;

		private IMover _mover;

		[SerializeField, HideInInspector]
		private int _id = -1;

		public Weapon Weapon
		{
			get;
			protected set;
		}

		public IMover Mover { get { return _mover; } }

		public Health Health { get; protected set; }

		public int Id
		{
			get { return _id; }
			private set { _id = value; }
		}

		protected void OnDestroy()
		{
			Health.UnitDied -= HandleUnitDied;
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

			Health = new Health( this, _startingHealth );
			Health.UnitDied += HandleUnitDied;
		}

		public virtual void Clear()
		{
			
		}

		// An abstract method has to be defined in a non-abstract child class.
		protected abstract void Update();

		public void RequestId()
		{
			if ( Id < 0 )
			{
				Id = GetNextId();
			}
		}

		public void TakeDamage( int amount )
		{
			Health.TakeDamage( amount );
		}

		protected virtual void HandleUnitDied( Unit unit )
		{
			GameManager.Instance.MessageBus.Publish( new UnitDiedMessage( this ) );
			gameObject.SetActive( false );
		}

		public virtual UnitData GetUnitData()
		{
			return new UnitData
			{
				Health = Health.CurrentHealth,
				Position = transform.position,
				YRotation = transform.eulerAngles.y,
				Id = Id
			};
		}

		public virtual void SetUnitData( UnitData data )
		{
			Health.SetHealth( data.Health );
			transform.position = data.Position;
			transform.eulerAngles = new Vector3(0, data.YRotation, 0);

			// TODO: Set gameobject active?
		}
	}
}
