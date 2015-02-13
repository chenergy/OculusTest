using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SelfCompleteText : MonoBehaviour
{
	public Text text;
	public float textDelay = 0.1f;
	public string finalText = "";

	public void PlayText (){
		StopCoroutine ("TextRoutine");
		if (text != null)
			StartCoroutine ("TextRoutine");
	}

	IEnumerator TextRoutine (){
		int curCharNum = 0;
		int maxCharNum = this.finalText.Length;
		this.text.text = "";

		while (curCharNum < maxCharNum) {
			this.text.text += finalText [curCharNum].ToString ();
			curCharNum++;

			yield return new WaitForSeconds (textDelay);
		}
	}
}

