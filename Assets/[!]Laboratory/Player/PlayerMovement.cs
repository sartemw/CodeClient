using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
	//Вешаем на оболочку игрока, которая имеет в себе модельку корабля и тд.
	//На модельку корабля вешаем компонент <ShipModel>

	[SerializeField] private float _speedMovement;
	public float SpeedMovement
	{
		get
		{
			return _speedMovement;
		}
	}

	[SerializeField] protected float _speedRotation;

	[SerializeField] private float _angleTilt;

	public Joystick Joystick;

	private float _direction;
	public float Direction
	{
		get
		{
			return _direction;
		}
	}

	private float _rotation;
	public float Rotation
	{
		get
		{
			return _rotation;
		}
	}

	protected Rigidbody2D _rigidbody2D;
	private GameObject _shipModel;
	protected IInputMoveVector _inputMoveVector;
	private float _tilt;

	//выбор интерфейса управления
	protected JoystickInputMoveVector _input;

	private void Start()
	{
		_rigidbody2D = GetComponent<Rigidbody2D>();
		_shipModel = GetComponentInChildren<ShipModel>().gameObject;

		_input = new JoystickInputMoveVector();
		Bind(_input);  //определяем интерфейс управления
	}

	void FixedUpdate()
    {
		if (_inputMoveVector == null) return;

		var direction = _inputMoveVector.GetDirection(this);

		//Иду! Передвигаюсь!
		Move(direction);
		Rotate(direction);
	}

	private void Move(Vector3 direction)
	{	
		_rigidbody2D.AddForce(direction * _speedMovement * Time.deltaTime);
	}

	private void Rotate(Vector3 direction)
	{
		float angle = Vector2.Dot(-transform.up.normalized, direction.normalized);

		_rigidbody2D.MoveRotation(Mathf.Lerp(_rigidbody2D.rotation, _rigidbody2D.rotation - angle * _speedRotation, 1));

		var dotValue = Vector3.Dot(transform.up, direction);
		Tilt(dotValue);
	}

	//наклоняед модель в сторону при повороте
	private void Tilt(float dotValue)
	{
		var rotX = new Vector3(-TiltValue(dotValue), 0, 0);
		_shipModel.transform.localRotation = Quaternion.Euler(rotX);
	}

	private float TiltValue(float dotValue)
	{
		if (dotValue < 0.2f && dotValue > -0.2f)
		{   //возвращяет в 0	
			float t = 0;
			t += 2f * Time.deltaTime;
			_tilt = Mathf.Lerp(_tilt, 0, t);
		}
		else
		{   //наклоняет
			_tilt = Mathf.Clamp(_tilt += dotValue, -_angleTilt, +_angleTilt);			
		}

		return _tilt;
	}

	public void Bind(IInputMoveVector inputMoveVector)
	{
		_inputMoveVector = inputMoveVector;
	}	
}
