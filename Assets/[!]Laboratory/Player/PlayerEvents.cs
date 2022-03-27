using System;
using UnityEngine;
using UnityEngine.Events;

public class PlayerEvents : MonoBehaviour
{
	public UnityEvent OnDeath;

	public event Action<int> HealthChangeEvent;
	public event Action<int> ExperienceChangeEvent;

	private int _health;
	private int _experience;

	/// <summary>
	/// Damage > 0
	/// </summary>
	/// <param name="damage"></param>
	/// 
    public void ApplyDamage(int damage)
	{
		if (damage < 0) throw new ArgumentOutOfRangeException("damage");

		_health -= damage;

		HealthChangeEvent?.Invoke(_health);

		if (_health <= 0)
		{
			DeathPlayer();
		}
	}

	private void DeathPlayer()
	{		
		Destroy(gameObject);
		OnDeath.Invoke();
	}

	/// <summary>
	/// Experience > 0
	/// </summary>
	/// <param name="exp"></param>
	/// 
	public void AddExperience(int exp)
	{
		if (exp < 0) throw new ArgumentOutOfRangeException("experience");
		
		_experience += exp;

		ExperienceChangeEvent?.Invoke(_experience);
	}
}
