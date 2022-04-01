using UnityEngine;

public abstract class AbillityAction : ScriptableObject
{
	[Tooltip("Позиция скилла 1-5")]
	[SerializeField] private int _id;

	[Tooltip("Скорость снаряда")]
	[SerializeField] private float _speed;
	public abstract void Action(Unit target);
}