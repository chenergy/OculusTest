using UnityEngine;
using System.Collections;

public class PanelTouchAnimation : MonoBehaviour
{
	public float activationSpeed = 1.0f;
	public float rotationSpeed = 1.0f;
	public float followSpeed = 1.0f;
	public Vector3 localScale = Vector3.one;


	private Transform targetTransform = null;
	public Transform TargetTransform {
		get { return this.targetTransform; }
	}


	private bool isPlaying = false;
	public bool IsPlaying {
		get { return this.isPlaying; }
	}


	private HandController handController;

	// Use this for initialization
	void Start ()
	{
		this.transform.localScale = Vector3.zero;
		if (Object.FindObjectOfType <HandController> () != null) {
			this.handController = Object.FindObjectOfType <HandController> ();
		}
	}


	void Update (){
		if (this.targetTransform != null) {
			this.transform.position = Vector3.Lerp (
				this.transform.position, 
				targetTransform.position,
				Time.deltaTime * this.followSpeed);

			this.transform.localPosition = new Vector3 (
				this.transform.localPosition.x, 
				this.transform.localPosition.y, 
				0.0f);

			this.transform.Rotate (0.0f, 0.0f, Time.deltaTime * this.rotationSpeed);
		}

		if (this.handController != null){
			if (this.handController.GetAllPhysicsHands ().Length == 0) {
				this.DeActivate ();
			}
		}
	}


	public void Activate (Transform target){
		this.isPlaying = true;
		this.targetTransform = target;
		StopCoroutine ("ToggleActivation");
		StartCoroutine ("ToggleActivation");
	}

	public void DeActivate () {
		this.isPlaying = false;
		this.targetTransform = null;
		StopCoroutine ("ToggleActivation");
		StartCoroutine ("ToggleActivation");
	}

	IEnumerator ToggleActivation (){
		Vector3 targetScale = (this.isPlaying) ? localScale : Vector3.zero;

		while ((this.transform.localScale - targetScale).sqrMagnitude > 0.0005f) {
			yield return new WaitForEndOfFrame ();
			this.transform.localScale = Vector3.Lerp (this.transform.localScale, 
				targetScale, 
				Time.deltaTime * this.activationSpeed);
		}

		this.transform.localScale = targetScale;
	}
}

