using UnityEngine;
using UnityEngine.EventSystems;

public class TranslateJoystickToPoint : MonoBehaviour, IPointerEnterHandler
{
	[SerializeField] private RectTransform _movementJoystick;
	[SerializeField] private RectTransform _weaponJoystick;

	private Vector2 _startingMovementJoystickPosition;
	private Vector2 _startingWeaponJoystickPosition;

	private Vector2 _mousePos;
	private Vector2 _returnPos;
	private RectTransform _joystickTransform = new RectTransform();

	private Joystick _joystick;


	private void Start()
	{
		_startingMovementJoystickPosition = _movementJoystick.anchoredPosition;
		_startingWeaponJoystickPosition = _weaponJoystick.anchoredPosition;
	}

	void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			_mousePos = Input.mousePosition;

			if (_mousePos.x > Screen.width / 2)
			{
				//"Right"
				_joystickTransform = _weaponJoystick;
				_returnPos = _startingWeaponJoystickPosition;
				_mousePos.x = -(Screen.width - _mousePos.x);

				TranslateJoystick(_joystickTransform, _mousePos);

			}
			else
			{
				//"Left"
				_joystickTransform = _movementJoystick;
				_returnPos = _startingMovementJoystickPosition;

				TranslateJoystick(_joystickTransform, _mousePos);
			}
		}

		if (Input.GetMouseButtonUp(0))
		{
			ReturnJoystick(_joystickTransform, _returnPos);
		}
	}

	private void TranslateJoystick(RectTransform joystick, Vector2 pos)
	{
		_joystick = _joystickTransform.GetComponent<Joystick>();
		//добавить анимацию перемещения джостика
		joystick.anchoredPosition = pos;
	}

	private void ReturnJoystick(RectTransform joystick, Vector2 pos)
	{
		//добавить анимацию перемещения джостика
		joystick.anchoredPosition = pos;

		_joystick = null;
	}

	public void OnPointerEnter (PointerEventData eventData)
	{		
		if (_joystick == null) return;

		eventData.pointerDrag = _joystick.background.gameObject;
		_joystick.GetComponent<Joystick>().OnDrag(eventData);	
	}
	
}
