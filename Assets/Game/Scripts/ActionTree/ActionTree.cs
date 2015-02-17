using UnityEngine;
using System.Collections;

public class ActionTree : MonoBehaviour
{
	public ActionTreeNode parentNode;
	//public float maxTime = 1.0f;
	[Range (0.0f, 10.0f)]
	public float speed = 1.0f;
	private bool active = false;



	// Update is called once per frame
	void Update (){
		if (Input.GetKeyDown (KeyCode.Q)) {
			this.Activate ();
		} else if (Input.GetKeyDown (KeyCode.W)) {
			this.DeActivate ();
		}
	}
	/*void Update (){
		if (Input.GetKeyDown (KeyCode.Q)) {
			this.parentNode.maxTime = this.maxTime;
			this.parentNode.speed = this.speed;
			this.parentNode.ActionDirection (0);
		}
		if (Input.GetKeyDown (KeyCode.W)) {
			this.parentNode.maxTime = this.maxTime;
			this.parentNode.speed = this.speed;
			this.parentNode.ActionDirection (1);
		}
	}*/

	public void Activate (){
		if (!this.active) {
			//this.parentNode.maxTime = this.maxTime;
			//this.parentNode.speed = this.speed;
			this.parentNode.ActionDirection (AnimationDirection.FORWARD, speed);
			this.active = true;
		}
	}

	public void DeActivate() {
		if (this.active) {
			//this.parentNode.maxTime = this.maxTime;
			//this.parentNode.speed = this.speed;
			this.parentNode.ActionDirection (AnimationDirection.BACKWARD, speed);
			this.active = false;
		}
	}

	IEnumerator DisableSelf (){
		yield return new WaitForSeconds (2.0f);
		this.gameObject.SetActive (false);
	}
}

