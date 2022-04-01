using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ShootableArea : MonoBehaviour
{

	[SerializeField] private PlayerWeapon _playerWeapon;
	[SerializeField] private float _timeTargetLock = 0.5f;
	[SerializeField] private float _speed = 0.5f;

	private Transform _aim;
	private GameObject _target;
	private float _timeTargeting = 0;
	
	public event Action<bool> LockTargetChangeEvent;

	private GameObject _imageAIM;
	private Vector3 _initScale, _smallScale;
	private Color _initColor, _greenColor;
	private void Start()
	{
		_imageAIM = transform.parent.GetComponentInChildren<Image>().gameObject;
		_initScale = _imageAIM.GetComponent<RectTransform>().localScale;
		_smallScale = new Vector3(_imageAIM.GetComponent<RectTransform>().localScale.x - 0.2f,
									 _imageAIM.GetComponent<RectTransform>().localScale.y - 0.2f,
									 _imageAIM.GetComponent<RectTransform>().localScale.z);
		_initColor = _imageAIM.GetComponent<Image>().color;
		_greenColor = new Color(25, 230, 30);

		_aim = transform;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
			OnTriggerStay2D(collision);
	}

	private void OnTriggerStay2D(Collider2D collision)
	{
		if (collision.GetComponent<Player>())
		{
			var target = collision.GetComponent<Player>();

			if (target.TeamObject == Player.TeamFlag.EnemyTeam ||
				target.TeamObject == Player.TeamFlag.EnemyNeutral)
			{
				_target = target.gameObject;
				_timeTargeting += Time.deltaTime;
				LockTarget(true);
				if (_timeTargeting >= _timeTargetLock)
				{
					_playerWeapon.Attack(_target);
					MoveToTarget(_target);
					StartCoroutine(DownScaleAIM());
				}
			}
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.gameObject == _target)
			{
				_timeTargeting = 0;
				LockTarget(false);
				StartCoroutine(UpScaleAIM());
			}
	}
	
	private void MoveToTarget(GameObject target)
	{
		Vector3 direction = target.transform.position - transform.position;
		var improvedDirection = new Vector3(direction.x, direction.y).normalized;

		//MoveToRound надо заменить на врага
		if (target.GetComponent<MoveToRound>())
			_speed = target.GetComponent<MoveToRound>().SpeedMovement + 0.1f;
		else _speed = target.GetComponent<PlayerMovement>().SpeedMovement + 0.1f;

		var moveVector = Vector3.Lerp(_aim.parent.position, _aim.parent.position + improvedDirection * _speed * Time.deltaTime, 0f);
		_aim.parent.position = moveVector;				
	}
	public void LockTarget(bool isLocked)
	{
		LockTargetChangeEvent?.Invoke(isLocked);
	}

	private IEnumerator DownScaleAIM()
	{
		while (_imageAIM.GetComponent<RectTransform>().localScale !=_smallScale) {
			yield return null;
			Vector3 speed = new Vector3();
			_imageAIM.GetComponent<RectTransform>().localScale = Vector3.SmoothDamp(_imageAIM.GetComponent<RectTransform>().localScale, _smallScale, ref speed, .1f);
			_imageAIM.GetComponent<Image>().color = Color.Lerp(_initColor, _greenColor, .1f);
		}
		
	}

	private IEnumerator UpScaleAIM()
	{
		while (_imageAIM.GetComponent<RectTransform>().localScale != _initScale) {
			yield return null;

			Vector3 speed = new Vector3();
			_imageAIM.GetComponent<RectTransform>().localScale = Vector3.SmoothDamp(_imageAIM.GetComponent<RectTransform>().localScale, _initScale, ref speed, .1f);
			_imageAIM.GetComponent<Image>().color = Color.Lerp(_greenColor, _initColor, .1f);
		}
	}
}
