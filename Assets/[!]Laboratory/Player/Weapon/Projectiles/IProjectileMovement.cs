using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 public interface IProjectileMovement
{
	/// <summary>
	/// Normlized bullet direction
	/// </summary>
	/// <returns></returns>
	
	Vector3 GetDirection(ProjectileMachineGun gO);	
}
