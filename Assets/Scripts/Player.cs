using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	private Vector3 _velocity;

	[SerializeField] private float _maxVelocity;
	[SerializeField] private float _acceleration;
	[SerializeField] private float _turnSpeed;

	public void Move(Vector3 move, float deltaTime)
	{
		if (move.sqrMagnitude > 0)
		{
			_velocity += move * _acceleration;
			transform.rotation = Quaternion.Lerp(transform.rotation, 
												 Quaternion.LookRotation(_velocity), _turnSpeed * deltaTime);

		}
		else
			_velocity = Vector3.Lerp(_velocity, Vector3.zero, _acceleration);

		if (_velocity.magnitude > _maxVelocity)
		{
			_velocity.Normalize();
			_velocity *= _maxVelocity;
		}

		transform.position += _velocity * deltaTime;
	}
}
