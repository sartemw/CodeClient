using System;
using UnityEngine;
using UnityEngine.Events;

public class ShootableArea : MonoBehaviour
{

	[SerializeField] private PlayerWeapon _playerWeapon;
	[SerializeField] private float _timeTargetLock = 0.5f;
	[SerializeField] private float _speed = 0.5f;

	private Transform _aim;
	private GameObject _target;
	private float _timeTargeting = 0;
	
	public event Action<bool> LockTargetChangeEvent;	

	private void Start()
	{
		_aim = transform;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
			OnTriggerStay2D(collision);
	}

	private void OnTriggerStay2D(Collider2D collision)
	{
		if (collision.GetComponent<Player>())
		{
			var target = collision.GetComponent<Player>();

			if (target.TeamObject == Player.TeamFlag.EnemyTeam ||
				target.TeamObject == Player.TeamFlag.EnemyNeutral)
			{
				_target = target.gameObject;
				_timeTargeting += Time.deltaTime;
				LockTarget(true);
				if (_timeTargeting >= _timeTargetLock)
				{
					_playerWeapon.Attack(_target);
					MoveToTarget(_target);
				}
			}
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.gameObject == _target)
			{
				_timeTargeting = 0;
				LockTarget(false);
			}
	}
	
	private void MoveToTarget(GameObject target)
	{
		Vector3 direction = target.transform.position - transform.position;
		var improvedDirection = new Vector3(direction.x, direction.y).normalized;

		if (target.GetComponent<MoveToRound>())
			_speed = target.GetComponent<MoveToRound>().SpeedMovement + 0.1f;
		else _speed = target.GetComponent<PlayerMovement>().SpeedMovement + 0.1f;

		_aim.parent.position += improvedDirection * _speed * Time.deltaTime;				
	}
	public void LockTarget(bool isLocked)
	{
		LockTargetChangeEvent?.Invoke(isLocked);
	}
}
