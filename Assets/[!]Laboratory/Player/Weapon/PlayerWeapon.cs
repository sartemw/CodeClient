using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
	//префаб снаряда для стрельбы
	public GameObject ShotPrefab;
		
	//время перезарядки в секунду
	[SerializeField] private float _shootingRate = 0f;

	//public float ShootingRate
	//{
	//	set
	//	{
	//		_shootingRate = value;
	//	}

	//	private get
	//	{
	//		return _shootingRate;
	//	}
	//}

	[SerializeField] private GameObject _weapon;
	[SerializeField] private GameObject _aIM;
	[SerializeField] private WeaponJoystick _weaponJoystick;
	[SerializeField] private float _aimSpeed;

	//ПЕРЕЗАРЯДКА
	private float shotCooldown = 0;

	private Vector3 mousePosition;
	private Vector3 difference;

	private Vector3 bulletPosition;
	private Quaternion bulletRotation;
	private string bulletName;

	private List<GameObject> shotedBullets = new List<GameObject>();

	void Start()
	{
		_shootingRate = ShotPrefab.GetComponent<Projectile>().ShootingRate;

		//shotPrefab = playerStats.Bullets[0];
	}

	void Update()
    {
		RotateWeapon();
		AIMTranslate();
		if (shotCooldown > 0)
			shotCooldown -= Time.deltaTime;			
	}

	private void AIMTranslate()
	{
		Vector3 aimVelocitie = new Vector2(-_weaponJoystick.Direction.x , _weaponJoystick.Direction.y) ;

		var cam = Camera.main; //Камера на основе которой будем определять вышел ли объект за ее границы
		Vector3 point = cam.WorldToViewportPoint(_aIM.transform.position); //Записываем положение объекта к границам камеры, X и Y это будут как раз верхние и нижние границы камеры
				
		if (point.y < 0f && _weaponJoystick.Direction.y < 0)
		{
			aimVelocitie.y = 0;
		}

		if (_weaponJoystick.Direction.y > 0 && point.y > 1f)
		{
			aimVelocitie.y = 0;
		}

		if (point.x < 0f && _weaponJoystick.Direction.x < 0)
		{
			aimVelocitie.x = 0;
		}

		if (_weaponJoystick.Direction.x > 0 && point.x > 1f)
		{
			aimVelocitie.x = 0;
		}

		_aIM.transform.position += aimVelocitie*Time.deltaTime*_aimSpeed; 
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

	void RotateWeapon()
	{
		var AIM = _aIM.transform.position;
		var weapon_pos = _weapon.transform.position;
		var aimDelta = new Vector2();
		aimDelta.x = AIM.x - weapon_pos.x;
		aimDelta.y = AIM.y - weapon_pos.y;
		float angle = Mathf.Atan2(aimDelta.x, aimDelta.y) * Mathf.Rad2Deg;
		_weapon.transform.rotation = Quaternion.Euler(new Vector3(0, 0, -angle + 90));
	}

	//Показываем пулю всем
	public void RegisterBullet(GameObject target)
	{
		var bullet = Instantiate (ShotPrefab, _weapon.transform.position, _weapon.transform.rotation);
		bullet.GetComponent<Projectile>().SetTarget(target);
	}
}
