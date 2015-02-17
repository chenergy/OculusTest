using UnityEngine;
using System.Collections;

public class PanelTouchDetection : MonoBehaviour
{
	public PanelTouchAnimation[] touchAnimations;

	
	// Update is called once per frame
	/*void Update ()
	{
		foreach (PanelTouchAnimation touch in this.touchAnimations) {
			if (touch.IsPlaying) {
				this.touchAnimations [this.triggerNum].Activate (finger.transform);
			}
		}


		//this.touchAnimations [this.triggerNum].transform.localPosition = 
		//new Vector3 (0.0f, 0.0f, this.transform.InverseTransformPoint (other.transform.position).z);
		//this.touchAnimations [this.triggerNum].transform.position = other.transform.position;

		//this.triggerNum++;

	}*/

	void OnTriggerEnter (Collider other){
		RigidFinger finger = other.transform.parent.GetComponent <RigidFinger> ();

		if (finger != null) {
			int index = (int)finger.fingerType;

			if (index < this.touchAnimations.Length) {
				if (this.touchAnimations [index].TargetTransform == null) {
					this.touchAnimations [index].Activate (other.transform);
				}
			}
		}
	}

	void OnTriggerExit (Collider other){
		RigidFinger finger = other.transform.parent.GetComponent <RigidFinger> ();

		if (finger != null) {
			int index = (int)finger.fingerType;

			if (this.touchAnimations[index].TargetTransform == other.transform) {
				this.touchAnimations[index].DeActivate ();
			}
		}
	}
}

