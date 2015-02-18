using UnityEngine;
using System.Collections;

[System.Serializable]
public class TransformElement {
	public Vector3 value;
	public float duration = 1.0f;
	public float delay = 0.0f;
}

public class Rotate_Sequence : MonoBehaviour
{
	public Transform target;
	public TransformElement[] sequence;

	private int curIndex = 0;
	private Quaternion startRotation;
	private Quaternion targetRotation;


	void Start (){
		StartCoroutine ("RotationRoutine");
	}


	IEnumerator RotationRoutine (){
		float timer = 0.0f;
		float duration = this.sequence [this.curIndex].duration;
		float delay = this.sequence [this.curIndex].delay;

		this.startRotation = this.target.localRotation;
		this.targetRotation = Quaternion.Euler (this.sequence [this.curIndex].value);

		while (timer < duration) {
			yield return new WaitForEndOfFrame ();
			this.target.localRotation = Quaternion.Lerp (this.startRotation, this.targetRotation, (timer / duration));

			timer += Time.deltaTime;
		}

		yield return new WaitForSeconds (delay);

		this.curIndex++;
		if (this.curIndex >= this.sequence.Length)
			this.curIndex = 0;

		StartCoroutine ("RotationRoutine");
	}
}

