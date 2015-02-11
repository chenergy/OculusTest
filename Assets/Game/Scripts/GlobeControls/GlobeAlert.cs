using UnityEngine;
using System.Collections;

public class GlobeAlert : MonoBehaviour
{
	public SpriteRenderer childAlert;
	public Transform alertEndpoint;
	public float speed = 1.0f;

	public void GoToEndpoint (){
		StartCoroutine ("GoToEndpointRoutine");
	}


	void Update (){
		this.childAlert.color = new Color (this.childAlert.color.r, this.childAlert.color.g, this.childAlert.color.b,
			Mathf.Abs (TrigLookup.Sin (Time.time * 10.0f)));
	}


	IEnumerator GoToEndpointRoutine (){
		this.childAlert.transform.parent = null;

		while ((this.childAlert.transform.position - alertEndpoint.position).sqrMagnitude > 0.01f ||
		       (Quaternion.Angle (this.childAlert.transform.rotation, alertEndpoint.rotation) > 0.01f) ||
		       ((this.childAlert.transform.localPosition - Vector3.one * 0.5f).sqrMagnitude > 0.01f)) {
			yield return new WaitForEndOfFrame ();
			this.childAlert.transform.position = Vector3.Lerp (this.childAlert.transform.position, alertEndpoint.position, Time.deltaTime * this.speed);
			this.childAlert.transform.rotation = Quaternion.Lerp (this.childAlert.transform.rotation, alertEndpoint.rotation, Time.deltaTime * this.speed);
			this.childAlert.transform.localScale = Vector3.Lerp (this.childAlert.transform.localScale, Vector3.one * 0.5f, Time.deltaTime * this.speed);
		}
	}
}

