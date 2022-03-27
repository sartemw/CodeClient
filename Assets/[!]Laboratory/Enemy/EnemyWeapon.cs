using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
	//префаб снаряда для стрельбы
	public GameObject ShotPrefab;

	//время перезарядки в секунду
	[SerializeField] private float _shootingRate = 0f;
	//public float ShootingRate
	//{
	//	private set
	//	{
	//		_shootingRate = value;
	//	}

	//	get
	//	{
	//		return _shootingRate;
	//	}
	//}

	[SerializeField] private GameObject _weapon;
	
	//ПЕРЕЗАРЯДКА
	private float shotCooldown = 0;

	[HideInInspector] public Transform Target;

	void Start()
	{
		_shootingRate = ShotPrefab.GetComponent<Projectile>().ShootingRate;

		//shotPrefab = playerStats.Bullets[0];
	}

	void Update()
	{
		RotateWeapon();

		if (shotCooldown > 0)
			shotCooldown -= Time.deltaTime;
	}

	void RotateWeapon()
	{
		if (Target == null) return;

		var target = Target.position;
		var weapon_pos = _weapon.transform.position;
		var aimDelta = new Vector2();
		aimDelta.x = target.x - weapon_pos.x;
		aimDelta.y = target.y - weapon_pos.y;
		float angle = Mathf.Atan2(aimDelta.x, aimDelta.y) * Mathf.Rad2Deg;
		_weapon.transform.rotation = Quaternion.Euler(new Vector3(0, 0, -angle + 90));
		
		Attack(Target.gameObject);
	}

	//3 -СТРЕЛЬБА ИЗ ShootableArea в прицеле AIM
	public void Attack(GameObject target)
	{
		if (CanAttack)
		{
			_shootingRate = ShotPrefab.GetComponent<Projectile>().ShootingRate;
			shotCooldown = _shootingRate;

			//создайте новый выстрел
			RegisterBullet(target);
		}
	}

	//готово ли оружие выпустить новый снаряд ?
	public bool CanAttack
	{
		get { return shotCooldown <= 0f; }
	}

	//Показываем пулю всем
	public void RegisterBullet(GameObject target)
	{
		var bullet = Instantiate(ShotPrefab, _weapon.transform.position, _weapon.transform.rotation);
		
		bullet.GetComponent<Projectile>().SetTarget(target);
	}
}
