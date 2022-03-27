using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToRound : MonoBehaviour
{
	[SerializeField] private float _rotateSpeed;

	[SerializeField] private float _speedMovement;

	private int _randomazer;
	public float SpeedMovement
	{
		get
		{
			return _speedMovement;
		}
	}

	private void Start()
	{
		StartCoroutine(Randomizer());
	}

	IEnumerator Randomizer()
	{
		while (true)
		{
			_randomazer = UnityEngine.Random.Range(-2, 2);
			if (_randomazer == 0)
				continue;
			yield return new WaitForSeconds(1.5f);
		}
	}

	private void FixedUpdate()
	{
		gameObject.transform.Rotate(0, 0, _rotateSpeed * _randomazer);
		gameObject.transform.Translate(Vector2.right * Time.deltaTime * _speedMovement);
	}
}
