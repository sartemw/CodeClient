using System.Collections;
using UnityEngine;

public class AbillityPlayerContainer : MonoBehaviour
{
	[SerializeField] private Abillity[] _abillities;

	public Abillity[] Abillities
	{
		get { return _abillities; }
		private set { _abillities = value; }
	}
}