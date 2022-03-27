using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindTargets : MonoBehaviour
{
	[SerializeField] private GameObject _aim;
	[SerializeField] private WeaponJoystick _weaponJoystick;

	private GameObject _target;
	private Vector2 _joystickDirection = Vector2.zero;
		
	//private void Update()
	//{
	//	if (_weaponJoystick.Direction !=  Vector2.zero )
	//	{
	//		_joystickDirection = _weaponJoystick.Direction;
	//	}
			
	//}
}
