using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbillityButtonContainer : MonoBehaviour
{
	[SerializeField] private GameObject[] _buttons;

	public GameObject[] Buttons
	{
		get { return _buttons; }
		private set { _buttons = value; }
	}

	private void Start()
	{
		StartCoroutine(CheckMe());
	}

	IEnumerator CheckMe()
	{
		while (true)
		{
			if (GameObjectsContainer.instance.Me)
			{
				var abillities = GameObjectsContainer.instance.Me.GetComponent<AbillityPlayerContainer>().Abillities;

				for (int i = 0; i < abillities.Length; i++)
				{
					_buttons[i].SetActive(true);
				}
				break;
			}
		yield return null;
		}
	}
}
