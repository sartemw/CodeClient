using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbillityExecuter : MonoBehaviour
{
    [SerializeField] private Abillity _currentAbillity;

    public void Execute(Abillity abillity, Vector2 targetPosition)
    {
        _currentAbillity = abillity;

        var targets = _currentAbillity.SelectTargets(targetPosition);

        if (targets != null)
        {
            _currentAbillity.ApplyActions(targets);
            //_currentAbillity = null;
        }
    }

    /*private void Update()
    {
        if (_currentAbillity != null)
        {
            var targets = _currentAbillity.SelectTargets((Vector2)Input.mousePosition);

            //??? ????? ???????, ????????? ????? ?? ??????? ????????

            if (Input.GetMouseButtonDown(0))
            {
                if (targets != null)
                {
                    _currentAbillity.ApplyActions(targets);
                    //_currentAbillity = null;
                }
            }
            else if (Input.GetMouseButtonDown(1))
            {
                //_currentAbillity = null;
            }
        }
    }*/
}
