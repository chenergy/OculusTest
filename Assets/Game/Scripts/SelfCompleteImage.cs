using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SelfCompleteImage : MonoBehaviour
{
	public Image image;
	public float completionSpeed = 1.0f;

	[Range (0.0f, 1.0f)]
	public float finalValue = 0.5f;

	public void PlayImage (){
		StopCoroutine ("ImageRoutine");
		if (image != null)
			StartCoroutine ("ImageRoutine");
	}

	IEnumerator ImageRoutine (){
		this.image.fillAmount = 0.0f;

		while (Mathf.Abs (this.finalValue - this.image.fillAmount) > 0.01f) {
			this.image.fillAmount = Mathf.Lerp (this.image.fillAmount, this.finalValue, Time.deltaTime * completionSpeed);
			yield return new WaitForEndOfFrame ();
		}

		this.image.fillAmount = this.finalValue;
	}
}

