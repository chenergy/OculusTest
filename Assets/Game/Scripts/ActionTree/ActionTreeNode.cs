using UnityEngine;
using System.Collections;


public enum AnimationDirection {
	FORWARD,
	BACKWARD
}

/*[System.Serializable]
public class StartEndVector {
	public Vector3 start;
	public Vector3 end;
}*/

public class ActionTreeNode : MonoBehaviour
{
	//public float maxTime = 1.0f;
	//public float speed = 1.0f;
	//public StartEndVector localPositionVec;
	//public StartEndVector localRotationVec;
	//public StartEndVector localScaleVec;
	//public Transform targetTriangle;
	public Animation targetAnimation;
	public string animationName = "";
	public float childAnimationDelay = 0.0f;
	public ActionTreeNode[] childNodes;

	private bool childActivated = false;
	private Coroutine coroutine = null;

	// Use this for initialization
	void Start ()
	{
		/*this.targetTriangle.localPosition = localPositionVec.start;
		this.targetTriangle.localRotation = Quaternion.Euler (localRotationVec.start);
		this.targetTriangle.localScale = localScaleVec.start;*/
	}
	
	public void ActionDirection (AnimationDirection direction, float speed){
		//float animSpeed = (direction == AnimationDirection.FORWARD) ? Mathf.Abs (speed) : -Mathf.Abs (speed);
		if (this.coroutine != null)
			StopCoroutine (this.coroutine);
		this.coroutine = StartCoroutine (ActionRoutine (direction, speed));
	}

	IEnumerator ActionRoutine (AnimationDirection direction, float speed){
		if (this.targetAnimation != null) {
			if (this.targetAnimation.GetClip (this.animationName) != null) {
				if (direction == AnimationDirection.BACKWARD)
					this.targetAnimation [this.animationName].normalizedTime = 1.0f;
				float animSpeed = (direction == AnimationDirection.FORWARD) ? Mathf.Abs (speed) : -Mathf.Abs (speed);
				this.targetAnimation [this.animationName].speed = animSpeed;
				this.targetAnimation.Play (this.animationName);
			}
		}

		yield return new WaitForSeconds (this.childAnimationDelay);

		foreach (ActionTreeNode t in this.childNodes) {
			if (t != null) {
				//t.maxTime = this.maxTime;
				//t.speed = this.speed;
				t.ActionDirection (direction, speed);
			}
		}

		this.coroutine = null;

		Debug.Log ((this.coroutine == null));
		/*
		float timer = 0.0f;

		while (timer < maxTime) {
			yield return new WaitForEndOfFrame ();
			timer += Time.deltaTime;

			this.targetTriangle.localPosition = (direction == 0) ? 
				Vector3.Lerp (localPositionVec.start, localPositionVec.end, timer / maxTime) :
				Vector3.Lerp (localPositionVec.end, localPositionVec.start, timer / maxTime);

			this.targetTriangle.localRotation = (direction == 0) ? 
				Quaternion.Lerp (Quaternion.Euler (localRotationVec.start), Quaternion.Euler (localRotationVec.end), timer / maxTime) :
				Quaternion.Lerp (Quaternion.Euler (localRotationVec.end), Quaternion.Euler (localRotationVec.start), timer / maxTime);

			this.targetTriangle.localScale = (direction == 0) ? 
				Vector3.Lerp (localScaleVec.start, localScaleVec.end, timer / maxTime) :
				Vector3.Lerp (localScaleVec.end, localScaleVec.start, timer / maxTime);

			if (!this.childActivated && timer > maxTime / this.speed) {
				this.childActivated = true;
				foreach (ActionTreeNode t in this.childNodes) {
					if (t != null) {
						t.maxTime = this.maxTime;
						t.speed = this.speed;
						t.ActionDirection (direction);
					}
				}
			}
		}

		this.childActivated = false;
		*/
	}
}

