using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankGame
{
	public class CameraFollow : MonoBehaviour
	{
		[SerializeField]
		private float _distance;

		[SerializeField]
		private float _angle;

		[SerializeField]
		private Transform _target;

		private float _angleRad;

		private void Awake()
		{
			SetAngle( _angle );
		}

		private void Update()
		{
			if ( _target == null )
			{
				return;
			}

			transform.position = CalculatePosition();
			transform.eulerAngles = CalculateDirection();
		}

		public void SetAngle( float angle )
		{
			// The angle in radians can be calculated by multiplying the angle in degrees by
			// Mathf.Deg2Rad.
			_angle = angle;
			_angleRad = _angle * Mathf.Deg2Rad;
		}

		public void SetDistance( float distance )
		{
			_distance = distance;
		}

		public void SetTarget( Transform target )
		{
			_target = target;
		}

		private Vector3 CalculatePosition()
		{
			float x = _distance * Mathf.Sin( _angleRad );
			float y = _distance * Mathf.Cos( _angleRad );

			return _target.position + _target.forward * -1 * x + _target.up * y;
		}

		private Vector3 CalculateDirection()
		{
			Vector3 rotation = transform.eulerAngles;
			rotation.y = _target.eulerAngles.y;
			rotation.x = 90 - _angle;
			return rotation;
		}
	}
}
