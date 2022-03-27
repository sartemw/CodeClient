using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
	[SerializeField] private Text _healthText;
	[SerializeField] private Text _experienceText;
	[SerializeField] private Player _player;

	public void Bind (Player player)
	{
		_player = player;

		enabled = true;
	}

	private void Awake()
	{
		if (_player == null) enabled = false;
	}

	private void OnEnable()
	{
		if (_player == null) return;

		_player.HealthChangeEvent += SetDamage;
		_player.ExperienceChangeEvent += SetExperience;
	}

	private void OnDisable()
	{
		if (_player == null) return;

		_player.HealthChangeEvent -= SetDamage;
		_player.ExperienceChangeEvent -= SetExperience;
	}

	private void SetDamage(float health)
	{
		_healthText.text = health.ToString();
	}

	private void SetExperience(float experience)
	{
		_experienceText.text = experience.ToString();
	}
}
