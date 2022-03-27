using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetingArea : MonoBehaviour
{
	[SerializeField] float _speed = 1;

	[SerializeField] Transform _aim ;

	private GameObject _target;
	private GameObject _currentTarget;
	private Vector3 _currentPosition;
	private IEnumerator _aimTargering;
	private Vector3 _refSpeed = Vector3.zero;

	private ShootableArea _shootableArea;
	private bool _isLocked = false;
	//private bool _isMoved = false;
	private bool _isActionCoroutine = true;

	private SearchingTargetArea _searchingTargetArea;

	private void Awake()
	{
		_shootableArea = transform.parent.GetComponentInChildren<ShootableArea>();
	}

	private void OnEnable()
	{
		if (_shootableArea == null) return;

		_shootableArea.LockTargetChangeEvent += LockTarget;
	}

	private void OnDisable()
	{
		if (_shootableArea == null) return;

		_shootableArea.LockTargetChangeEvent -= LockTarget;
	}

	private void Start()
	{		
		_currentPosition = transform.position;
		_searchingTargetArea = GetComponentInChildren<SearchingTargetArea>();
	}

	private void Update()
	{
		if (transform.position != _currentPosition)
		{
			//_isMoved = true;
			_currentPosition = transform.position;
		}		
		else
		{
			//_isMoved = false;
		}		
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		OnTriggerStay2D(collision);
	}

	private void OnTriggerStay2D(Collider2D collision)
	{
		if (!collision && _searchingTargetArea._target)
		{
			_target = _searchingTargetArea._target;
		}
		//IsLocked выключается, когда заканчивается стрельба в ShootableArea
		if (!_isLocked) 
		{
			if (collision != null && collision.GetComponent<Player>())
				_target = collision.gameObject;

			if (_target)
			{
				if (_target.GetComponent<Player>().Identety == false)
				{
					if (_isActionCoroutine == true)
					{
						_isActionCoroutine = false;

						_aimTargering = AimTargeting(_target);
						StartCoroutine(_aimTargering);
					}
				}
			}
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.gameObject == _target)
		{
			if (_aimTargering != null)
			{
				StopCoroutine(_aimTargering);
				_target = null;
				_isActionCoroutine = true;
			}
		}
	}

	private IEnumerator AimTargeting(GameObject target)
	{	
		do
		{
			Vector3 direction = target.transform.position - _aim.transform.position;
			
			var normalizedDirection = new Vector3(direction.x, direction.y).normalized;

			yield return null;

			//_aim.position += improvedDirection * _speed * Time.deltaTime;
			_aim.position = Vector3.Lerp(_aim.position, _aim.position + normalizedDirection * _speed * Time.deltaTime, .6f);

		} while ((!_isLocked) || (_aim.position != target.transform.position));

		
		_isActionCoroutine = true;
	}
	private void LockTarget(bool targetLocked)
	{
		_isLocked = targetLocked;
	}
}
