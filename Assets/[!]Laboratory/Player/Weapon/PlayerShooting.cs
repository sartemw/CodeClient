using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
	//префаб снаряда для стрельбы
	GameObject shotPrefab;

	public GameObject[] Bullets;

	//время перезарядки в секунду
	[HideInInspector]
	public float shootingRate = 0.25f;
	//ПЕРЕЗАРЯДКА
	[HideInInspector]
	public float shotCooldown;

	Vector3 mousePosition;
	Vector3 difference;

	void Update()
    {
		RotateWeapon();

		if (shotCooldown > 0)
			shotCooldown -= Time.deltaTime;

		if (Input.GetButton("Fire1"))
			Attack();
	}

	//3 -СТРЕЛЬБА ИЗ ДРУГОГО СКРИПТА
	public void Attack()
	{
		if (CanAttack)
		{
			shotCooldown = shootingRate;

			//создайте новый выстрел
			Instantiate(shotPrefab, transform.position, transform.rotation);
		}
	}


	//готово ли оружие выпустить новый снаряд ?
	public bool CanAttack
	{
		get { return shotCooldown <= 0f; }
	}

	void RotateWeapon()
	{
		var mousePosition = Input.mousePosition;
		//mousePosition.z = transform.position.z - Camera.main.transform.position.z; // это только для перспективной камеры необходимо 
		mousePosition = Camera.main.ScreenToWorldPoint(mousePosition); //положение мыши из экранных в мировые координаты 
		var angle = Vector2.Angle(Vector2.right, mousePosition - transform.position);//угол между вектором от объекта к мыше и осью х 
		transform.eulerAngles = new Vector3(0f, 0f, transform.position.y > mousePosition.y ? -angle : angle);//немного магии на последок 
	}
}
