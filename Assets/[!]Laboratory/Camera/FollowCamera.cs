using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
	// Вешаем на игрока, чтобы камера следила за ним
	[SerializeField] private Camera _mainCamera;

	private GameObject _player;

    void Start()
    {
		_player = gameObject;
		_mainCamera = Camera.main;
    }

    
    void FixedUpdate()
    {
		var directionVector = new Vector3();
		directionVector = new Vector3 (_player.transform.position.x, 
									   _player.transform.position.y, 
									   _mainCamera.transform.position.z);
		
		_mainCamera.transform.position = Vector3.Lerp(_mainCamera.transform.position, directionVector, 0.07f);
    }
}
