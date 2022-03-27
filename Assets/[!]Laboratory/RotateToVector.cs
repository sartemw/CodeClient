using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateToVector : MonoBehaviour
{
	[SerializeField] private Joystick _inputJoystick;
	[SerializeField] float _speedRotation;

	private Vector2 _inputVector;
	private Rigidbody2D _rigidbody2D;

	private void Start()
	{
		_inputVector = _inputJoystick.Direction;
		_rigidbody2D = GetComponent<Rigidbody2D>();
	}

	private	void FixedUpdate()
	{
		_inputVector = new Vector2 (-_inputJoystick.Direction.x, _inputJoystick.Direction.y);
		Rotate(_inputVector);
	}

	private void Rotate(Vector3 direction)
	{
		float angle = Vector2.Dot(-transform.up.normalized, direction.normalized);

		_rigidbody2D.MoveRotation(Mathf.Lerp(_rigidbody2D.rotation, _rigidbody2D.rotation - angle * _speedRotation, 1));
	}
}
