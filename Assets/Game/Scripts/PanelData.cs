using UnityEngine;
using System.Collections;

public class PanelData : MonoBehaviour
{
	public SelfCompleteImage[] images;
	public SelfCompleteText[] texts;

	// Use this for initialization
	void Start ()
	{
		this.DisableData ();
	}

	public void DisableData () {
		foreach (SelfCompleteImage image in this.images) {
			image.gameObject.SetActive (false);
		}

		foreach (SelfCompleteText text in this.texts) {
			text.gameObject.SetActive (false);
		}
	}

	public void EnableData (){
		foreach (SelfCompleteImage image in this.images) {
			image.gameObject.SetActive (true);
			image.PlayImage ();
		}

		foreach (SelfCompleteText text in this.texts) {
			text.gameObject.SetActive (true);
			text.PlayText ();
		}
	}
}

