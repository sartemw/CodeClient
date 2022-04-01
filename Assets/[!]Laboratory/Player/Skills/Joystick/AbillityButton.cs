using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AbillityButton : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
	private GameObjectsContainer _gameObjectsContainer;
	private AbillityExecuter _abillityExecuter;
	private Abillity[] _playerAbillities;
	private Abillity _abillity;

	[SerializeField] private int id;

	private Vector2 targetPosition;
	private Vector2 buttonPosition;
	private void Start()
	{
		_gameObjectsContainer = GameObjectsContainer.instance;
		_abillityExecuter = _gameObjectsContainer.AbillityExecuter;
		_playerAbillities = _gameObjectsContainer.Me.GetComponent<AbillityPlayerContainer>().Abillities;
		buttonPosition = transform.position;
		_abillity = _playerAbillities[id];
	}

	public void SetAbillity() 
	{
		_abillityExecuter.Execute(_abillity, targetPosition);
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		OnDrag(eventData);
	}
	public void OnDrag(PointerEventData eventData)
	{
		targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		//gameObject.transform.position = targetPosition;
	}
	public void OnPointerUp(PointerEventData eventData)
	{
		SetAbillity();
		transform.position = buttonPosition;
	}

	
}

