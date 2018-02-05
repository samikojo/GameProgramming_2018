using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankGame
{
	public class TransformMover : MonoBehaviour, IMover
	{
		private float _moveSpeed;
		private float _turnSpeed;

		public void Init( float moveSpeed, float turnSpeed )
		{
			_moveSpeed = moveSpeed;
			_turnSpeed = turnSpeed;
		}
		
		public void Turn( float amount )
		{
			Vector3 rotation = transform.localEulerAngles;
			rotation.y += amount * _turnSpeed * Time.deltaTime;
			transform.localEulerAngles = rotation;
		}

		public void Move( Vector3 direction )
		{
			direction = direction.normalized;
			Vector3 position = transform.position + direction * _moveSpeed * Time.deltaTime;
			transform.position = position;
		}

		public void Turn( Vector3 target )
		{
			Vector3 direction = target - transform.position;
			direction.y = transform.position.y;
			direction = direction.normalized;
			float turnSpeedRad = Mathf.Deg2Rad * _turnSpeed * Time.deltaTime;
			Vector3 rotation = Vector3.RotateTowards( transform.forward,
				direction, turnSpeedRad, 0f );
			transform.rotation = Quaternion.LookRotation( rotation, transform.up );
		}

		public void Move( float amount )
		{
			Vector3 position = transform.position;
			Vector3 movement = transform.forward * amount * _moveSpeed * Time.deltaTime;
			position += movement;
			transform.position = position;
		}
	}
}
