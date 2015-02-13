using UnityEngine;
using System.Collections;

public class ActionTree : MonoBehaviour
{
	public ActionTreeNode parentNode;
	public float maxTime = 1.0f;
	public float speed = 1.0f;


	// Update is called once per frame
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
		this.parentNode.maxTime = this.maxTime;
		this.parentNode.speed = this.speed;
		this.parentNode.ActionDirection (0);
		StartCoroutine ("DisableSelf");
	}

	public void DeActivate() {
		this.parentNode.maxTime = this.maxTime;
		this.parentNode.speed = this.speed;
		this.parentNode.ActionDirection (1);
		StartCoroutine ("DisableSelf");
	}

	IEnumerator DisableSelf (){
		yield return new WaitForSeconds (2.0f);
		this.gameObject.SetActive (false);
	}
}

