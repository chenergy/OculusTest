using UnityEngine;
using System.Collections;

public class DataArranged : MonoBehaviour
{
	public float speed = 1.0f;
	Vector3 worldTarget = Vector3.zero;

	// Use this for initialization
	/*void Start ()
	{
	
	}*/
	
	// Update is called once per frame
	void Update ()
	{
		if ((this.transform.position - this.worldTarget).sqrMagnitude > 0.01f) {
			this.transform.position = Vector3.Lerp (this.transform.position, this.worldTarget, Time.deltaTime * this.speed);
		}
	}

	public void SetTarget (Vector3 target){
		this.worldTarget = target;
	}
}

