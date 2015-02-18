using UnityEngine;
using System.Collections;

public class Rotate_Constant : MonoBehaviour
{
	public Vector3 direction = Vector3.zero;
	public float speed = 1.0f;

	private Vector3 directionN = Vector3.zero;

	void Start (){
		this.directionN = direction.normalized;
	}
		
	void Update ()
	{
		this.transform.Rotate (this.directionN * Time.deltaTime * speed);
	}
}

