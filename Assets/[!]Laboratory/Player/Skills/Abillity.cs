using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Abillities/New Abillity")]
public class Abillity : ScriptableObject
{
    [SerializeField] private AbillityPlaceLogic _placeLogic;
    [SerializeField] private List<AbillityAction> _actions;

    public AbillityPlaceLogic PlaceLogic => _placeLogic;
    public List<AbillityAction> Actions => _actions;

    public void ApplyActions(IEnumerable<Unit> targets)
    {
        foreach (var action in _actions)
        {
            foreach (var target in targets)
            {
                action.Action(target);
            }
        }
    }

    public IEnumerable<Unit> SelectTargets(Vector2 screenPoint)
    {
        return _placeLogic.TryPlace(screenPoint);
    }
}
