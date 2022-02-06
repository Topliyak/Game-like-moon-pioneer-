using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	[SerializeField] private Player _player;
	[SerializeField] private Transform _cameraTransform;
	[SerializeField] private VirtualStick _stick;

	private void FixedUpdate()
	{
		Vector2 input = _stick.Vector;
		Vector3 cameraForward = _cameraTransform.forward - Vector3.up * _cameraTransform.forward.y;
		Vector3 cameraRight = _cameraTransform.right - Vector3.up * _cameraTransform.right.y;
		Vector3 move = cameraForward * input.y + cameraRight * input.x;
		_player.Move(move.normalized, Time.deltaTime);
	}
}
