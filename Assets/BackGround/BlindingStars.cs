using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BlindingStars : MonoBehaviour
{
	[SerializeField] private float delay = 0.1f;
	[SerializeField] private  ParticleSystem[] particleObjs;
	[SerializeField] private List<float> objsStartSpeed;

	void Start()
    {
		particleObjs= GetComponentsInChildren<ParticleSystem>();
		objsStartSpeed = new List<float>();
		StartCoroutine(AfterStart());
		//StartCoroutine(AlfaDown());
	}

	IEnumerator AfterStart()
	{

		for (int i = 0; i < particleObjs.Length; i++)
		{
			var main = particleObjs[i].main;

			objsStartSpeed.Add(main.startSpeed.constant);

			main.simulationSpeed = 100;
			main.startSpeed = 0.1f;
		}
		yield return new WaitForSeconds(delay);

		for (int i = 0; i < particleObjs.Length; i++)
		{
			var main = particleObjs[i].main;

			main.simulationSpeed = 1;
			main.startSpeed = objsStartSpeed[i];
		}
	}
	//	//как заставить мерцать частицы ?

	//IEnumerator AlfaDown()
	//{

	//	//как заставить мерцать частицы ?
	//	yield return new WaitForSeconds(2f);
	//	foreach (ParticleSystem star in starsParticles)
	//	{
	//		var main = star.main;
	//		var alfaColor = main.startColor.color;
	//		float vel = 0;
	//		alfaColor.a = Mathf.SmoothDamp(alfaColor.a, 0, ref vel, 20, 0.01f);

	//	}
	//	print("DOWN");
	//	yield return AlfaUp();
	//	print("END");
	//}

	//IEnumerator AlfaUp()
	//{
	//	yield return new WaitForSeconds(1f);
	//	foreach (ParticleSystem star in starsParticles)
	//	{
	//		var main = star.main;
	//		var alfaColor = main.startColor.color.a;
	//		float vel = 2;
	//		Mathf.SmoothDamp(alfaColor, 1, ref vel, 20, 0.01f);
	//	}
	//	print("UP");
	//	yield return new WaitForSeconds(1f);
	//}

}
