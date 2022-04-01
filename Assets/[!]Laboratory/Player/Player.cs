using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : Unit
{	
	[SerializeField] private bool _identety;
	public bool Identety
	{
		private set
		{
			_identety = value;
		}

		get
		{
			return _identety;
		}
	}
	
	private void Start()
	{
		SetDamage(0);
		AddExperience(0);

		var gop = GameObjectsContainer.instance;
		gop.Me = this.gameObject;
	}
	
}
