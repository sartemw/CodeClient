using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
	[SerializeField] private bool _inmortal = false;

	public UnityEvent OnDeath;

	public event Action<float> HealthChangeEvent;
	public event Action<float> ExperienceChangeEvent;

	[SerializeField] private float _health;
	public float Health
	{
		private set
		{
			_health = value;
		}

		get
		{
			return _health;
		}
	}

	private float _experience;
	public float Experience
	{
		private set
		{
			_experience = value;
		}

		get
		{
			return _experience;
		}
	}

	/// <summary>
	/// Damage > 0
	/// </summary>
	/// <param name="damage"></param>
	/// 

	private void Start()
	{
		SetDamage(0);
		AddExperience(0);
	}
	public void SetDamage(float damage)
	{
		if (damage < 0) throw new ArgumentOutOfRangeException("damage");

		_health -= damage;

		HealthChangeEvent?.Invoke(_health);

		if (_health <= 0)
			Death();
	}

	private void Death()
	{
		if (!_inmortal)
			Destroy(gameObject);
	}

	/// <summary>
	/// Experience > 0
	/// </summary>
	/// <param name="exp"></param>
	/// 
	public void AddExperience(float exp)
	{
		if (exp < 0) throw new ArgumentOutOfRangeException("experience");

		_experience += exp;

		ExperienceChangeEvent?.Invoke(_experience);
	}
}
