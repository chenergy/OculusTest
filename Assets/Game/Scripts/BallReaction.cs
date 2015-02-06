using UnityEngine;
using System.Collections;

public class BallReaction : MonoBehaviour
{
	private Vector3 startPos;

	// Use this for initialization
	void Start ()
	{
		this.startPos = this.transform.position;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if ((this.transform.position - this.startPos).sqrMagnitude > 0.01f) {
			this.transform.position = Vector3.Lerp (this.transform.position, this.startPos, Time.deltaTime * 10);
			Debug.Log ("moving back");
		}
	}
}

