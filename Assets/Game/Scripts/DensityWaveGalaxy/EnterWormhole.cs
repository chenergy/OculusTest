using UnityEngine;
using System.Collections;

public class EnterWormhole : MonoBehaviour
{
	public float speed = 1.0f;
	public Transform target;
	public MotionBlur leftMB;
	public MotionBlur rightMB;

	private bool started = false;

	// Use this for initialization
	/*void Start ()
	{

	}*/

	void Update (){
		if (!this.started) {
			if (Input.GetKeyDown (KeyCode.Space)) {
				this.started = true;
				StartCoroutine ("Move");
			}
		}
	}

	IEnumerator Move (){
		leftMB.enabled = true;
		rightMB.enabled = true;

		while (this.transform.position.z < target.position.z) {
			yield return new WaitForEndOfFrame ();
			//this.transform.position = Vector3.Lerp (this.transform.position, target.position, Time.deltaTime * speed);
			this.rigidbody.AddForce ((target.position - this.transform.position).normalized * speed);
		}

		//Debug.Break ();
		this.leftMB.enabled = false;
		this.rightMB.enabled = false;
	}
}

