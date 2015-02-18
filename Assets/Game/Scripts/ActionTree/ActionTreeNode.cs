using UnityEngine;
using System.Collections;


public enum AnimationDirection {
	FORWARD,
	BACKWARD
}

public class ActionTreeNode : MonoBehaviour
{
	public Animation targetAnimation;
	public string animationName = "";
	public float childAnimationDelay = 0.0f;
	public ActionTreeNode[] childNodes;

	private bool childActivated = false;
	private Coroutine coroutine = null;

	// Use this for initialization
	void Start ()
	{
	}
	
	public void ActionDirection (AnimationDirection direction, float speed){
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
				t.ActionDirection (direction, speed);
			}
		}

		this.coroutine = null;
	}
}

