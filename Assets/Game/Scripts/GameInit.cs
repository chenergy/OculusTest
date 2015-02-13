using UnityEngine;
using System.Collections;

public class GameInit : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
		StartCoroutine ("Wait");
	}

	IEnumerator Wait (){
		yield return new WaitForSeconds (4.0f);

		Application.LoadLevel ("leaptest");
	}
}

