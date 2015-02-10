using UnityEngine;
using System.Collections;

public class GalaxyStarter : MonoBehaviour
{
	public Galaxy target;

	// Use this for initialization
	void Start ()
	{
		StartCoroutine ("Wait");
	}

	IEnumerator Wait (){
		yield return new WaitForSeconds (1.0f);
		target.enabled = true;
	}
}

