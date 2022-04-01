using System.Collections.Generic;
using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Abillities/PlaceLogic/SingleTarget")]
public class AbillityPlaceLogicSingleTarget : AbillityPlaceLogic
{

    public override IEnumerable<Unit> TryPlace(Vector2 screenPoint)
    {
        RaycastHit2D hit = Physics2D.Raycast(screenPoint, Vector2.zero);
        if (hit.transform != null)
        {
            if (hit.transform.GetComponent<Unit>())
                return new Unit[] { hit.transform.GetComponent<Unit>() };
        }


        return null;
    }
}

