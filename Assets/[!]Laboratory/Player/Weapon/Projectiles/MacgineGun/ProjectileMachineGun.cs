using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMachineGun : Projectile
{
    [SerializeField] private float _speed = 1;
	private Vector3 _destinationPoint = Vector3.zero;
		
	private void FixedUpdate()
    {
		if (_targets)
		{
			Move(_targets.transform.position);
		}
		else if (_destinationPoint != Vector3.zero)
		{
			Move(_destinationPoint);

			if (transform.position == _destinationPoint) Destroy(gameObject);
		}
    }

	private void Move(Vector3 targetPosition)
	{		
		Vector3 direction = targetPosition - transform.position;
		var improvedDirection = new Vector3(direction.x, direction.y, 0).normalized;

		transform.position += improvedDirection * _speed;

		_destinationPoint = targetPosition;
	}


	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.GetComponent<Player>())
		{
			var target = collision.GetComponent<Player>();

			if (target.TeamObject == Player.TeamFlag.EnemyTeam ||
				target.TeamObject == Player.TeamFlag.EnemyNeutral)
			{
				target.SetDamage(_damage);

				Death(0);
			}
		}
	}
}
