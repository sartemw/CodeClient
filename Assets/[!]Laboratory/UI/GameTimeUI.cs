using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTimeUI : MonoBehaviour
{
	private Text _gameTimeText;

	private void Start()
	{
		_gameTimeText = gameObject.GetComponent<Text>();

		StartCoroutine(Timer());
	}

	private IEnumerator Timer()
	{
		do
		{
			_gameTimeText.text = Time.time.ToString();
			yield return new WaitForSeconds(1f);
		} while (true);
	}
}
