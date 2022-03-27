using UnityEngine;

 public interface IInputMoveVector 
{
	/// <summary>
	/// Normlized direction
	/// </summary>
	/// <returns></returns>
	
	Vector3 GetDirection(PlayerMovement gO);	
}

public class KeyboardInputMoveVector : IInputMoveVector
{
	public Vector3 GetDirection(PlayerMovement player)
	{
		return new Vector3(-Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0).normalized;
	}
}

public class JoystickInputMoveVector : MonoBehaviour, IInputMoveVector
{
	public Vector3 GetDirection(PlayerMovement player)
	{
		if (player.Joystick == null) return Vector3.zero;

		var joystick = player.Joystick;

		var direction = Vector3.forward * joystick.Direction.x + Vector3.right * joystick.Direction.y;

		return new Vector3(-direction.z, direction.x, 0).normalized;
	}
}




