using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SciFiArsenal;
using System;



public class Projectile : MonoBehaviour
{
	[SerializeField] protected GameObject _impactParticle;
	[SerializeField] protected GameObject _projectileParticle;
	[SerializeField] protected GameObject _muzzleParticle;
	[SerializeField] protected GameObject[] _trailParticles;
	[SerializeField] protected Vector3 _impactNormal; //Used to rotate impactparticle.


	[SerializeField] protected float _damage;
	[SerializeField] protected float _shootingRate;
	[SerializeField] private float _deathTime;

	protected enum WhoIsTarget {enemy, himSelf, allies, all, friendly};
	[SerializeField] protected WhoIsTarget _whoIsTarget;

	protected GameObject _targets;

	private Transform _parent;

	public float ShootingRate
	{
		protected set
		{
			_shootingRate = value;
		}

		get
		{
			return _shootingRate;
		}
	}

	public float Damage
	{
		private set
		{
			_damage = value;
		}

		get
		{
			return _damage;
		}
	}
	private void Start()
	{
		_parent = transform;
		StartCoroutine(DeathBullet());

		_projectileParticle = Instantiate(_projectileParticle, transform.position, transform.rotation, _parent) as GameObject;
		if (_muzzleParticle)
		{
			_muzzleParticle = Instantiate(_muzzleParticle, transform.position, transform.rotation, _parent) as GameObject;
			_muzzleParticle.transform.rotation = transform.rotation * Quaternion.Euler(0, 0, 250);
			Destroy(_muzzleParticle, .2f); // Lifetime of muzzle effect.
		}
	}
	public void SetTarget(GameObject target)
	{
		_targets = target;
	}
	
	protected void Death(float deathTime)
	{
		StopCoroutine(DeathBullet());
		_impactParticle = Instantiate(_impactParticle, transform.position, Quaternion.FromToRotation(Vector3.up, _impactNormal)) as GameObject;
		//_impactParticle.transform.rotation = transform.rotation * Quaternion.Euler(0, 0, 90);
		Destroy(_impactParticle, 1f);
		Destroy(gameObject, deathTime);
	}
	private void EnemyTrigger(WhoIsTarget whoIsTarget)
	{
		switch (whoIsTarget)
		{
			case WhoIsTarget.enemy:
				{
					break;
				}

			case WhoIsTarget.himSelf:
				{
					break;
				}

			case WhoIsTarget.all:
				{
					break;
				}
			case WhoIsTarget.allies:
				{
					break;
				}

			case WhoIsTarget.friendly:
				{
					break;
				}

			default:
				{
					break;
				}
		}
	}

	IEnumerator DeathBullet()
	{
		yield return new WaitForSeconds(_deathTime);

		Destroy(gameObject, 0);
	}
}


