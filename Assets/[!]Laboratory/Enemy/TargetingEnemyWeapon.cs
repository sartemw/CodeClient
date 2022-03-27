using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetingEnemyWeapon : MonoBehaviour
{
	private EnemyWeapon _parentWeapon;
	private void Start()
	{
		_parentWeapon = transform.parent.GetComponent<EnemyWeapon>();
	}
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.GetComponent<Player>())
		{			
			_parentWeapon.Target = collision.gameObject.transform;
		}
	}
}
