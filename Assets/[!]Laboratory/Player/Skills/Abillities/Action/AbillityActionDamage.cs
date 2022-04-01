using UnityEngine;

[CreateAssetMenu(menuName = "Abillities/Action/Damage")]
class AbillityActionDamage: AbillityAction
{
	[SerializeField] private float _damage;
	public override void Action(Unit target)
	{
		target.SetDamage(_damage);
	}
}
