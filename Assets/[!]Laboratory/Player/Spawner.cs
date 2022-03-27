using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
	[SerializeField] private PlayerEvents _player;
	[SerializeField] private PlayerUI _playerUI;
	[SerializeField] private Transform _uiContainer;
	[SerializeField] private Transform[] _spawnPoints;
	[SerializeField] private int _playerCountOnScen;

	private void OnValidate()
	{
		if (_spawnPoints.Length < _playerCountOnScen) Debug.LogError("Need more spawn points"); 
	}

	private void Start()
	{
		for (int i = 0; i < _playerCountOnScen; i++)
		{
			var spawnPoint = _spawnPoints[i];

			var playerGO = Instantiate(_player.gameObject, spawnPoint.position, Quaternion.identity);
			var playerUIGO = Instantiate(_playerUI.gameObject, _uiContainer);

			var playerComponent = playerGO.GetComponent<Player>();
			var playerUIComponent = playerUIGO.GetComponent<PlayerUI>();

			playerUIComponent.Bind(playerComponent);
		}
	}
}
