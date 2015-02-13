using UnityEngine;
using System.Collections;

[System.Serializable]
public class StartEndVector {
	public Vector3 start;
	public Vector3 end;
}

public class ActionTreeNode : MonoBehaviour
{
	public float maxTime = 1.0f;
	public float speed = 1.0f;
	public StartEndVector localPositionVec;
	public StartEndVector localRotationVec;
	public StartEndVector localScaleVec;
	public Transform targetTriangle;
	public ActionTreeNode[] childNodes;

	private bool childActivated = false;


	// Use this for initialization
	void Start ()
	{
		this.targetTriangle.localPosition = localPositionVec.start;
		this.targetTriangle.localRotation = Quaternion.Euler (localRotationVec.start);
		this.targetTriangle.localScale = localScaleVec.start;
	}
	
	public void ActionDirection (int direction){
		StartCoroutine ("ActionRoutine", direction);
	}

	IEnumerator ActionRoutine (int direction){
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
	}
}

