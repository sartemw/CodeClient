using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleCruiserMoveForward : MonoBehaviour
{
    [SerializeField] private Vector3 _navagationPoint;
	[SerializeField] private Vector3 _objPos;
	private float _moveLeft = 5;
	private float _moveRight = -5;


    void Start()
    {
		_objPos = transform.position;
		var posX = _objPos.x;

		if (posX < 0)
		{
			_navagationPoint = new Vector3(_moveRight, _objPos.y, transform.parent.position.z);
		}
		else
		{
			_navagationPoint = new Vector3(_moveLeft, _objPos.y, transform.parent.position.z);
		}
    }

    void FixedUpdate()
    {
		if (_objPos != _navagationPoint)
		{
			transform.position = Vector3.Lerp(transform.position, _navagationPoint, 0.005f);
		}
    }
}
