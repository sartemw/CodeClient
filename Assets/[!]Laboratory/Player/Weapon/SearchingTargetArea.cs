using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchingTargetArea : TargetingArea
{
    private Rigidbody2D _rigidbody2D;
    protected Transform _target;
    void Start()
    {
		_target = null;
        _rigidbody2D = GetComponent<Rigidbody2D>();    
    }

	private void OnTriggerStay2D(Collider2D collision)
	{
		if (!_target)
		{
			if (collision.GetComponent<Player>())
			{
				if(collision.GetComponent<Player>().Identety == false)
				{
					_target = collision.transform;
				}
			}
		}
	}
}
