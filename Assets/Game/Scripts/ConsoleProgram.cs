using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Text;

public class ConsoleProgram : MonoBehaviour
{
	public UnityEngine.Object textLog;
	public Text canvasText;
	public float delay = 0.05f;
	public int charsPerDelay = 1;
	public ActionTree action;

	private TextAsset textAsset;
	private StringBuilder stringBuilder = new StringBuilder ();
	private int stringLength = 0;

	// Use this for initialization
	void Start ()
	{
		this.ReadInText ();
		StartCoroutine ("StartTextRoutine");
	}


	IEnumerator StartTextRoutine (){
		float timer = 0.0f;
		int curChar = 0;

		while ((this.textAsset != null) && (curChar < this.stringLength)) {
			yield return new WaitForEndOfFrame ();

			if (timer > delay) {
				for (int i = 0; i < charsPerDelay; i++) {
					if (curChar < this.stringLength) {
						this.stringBuilder.Append (this.textAsset.text [curChar]);
						curChar++;
					} else {
						break;
					}
				}

				this.canvasText.text = this.stringBuilder.ToString ();
				timer = 0.0f;
			}

			timer += Time.deltaTime;
		}

		yield return new WaitForSeconds (1.0f);

		if (this.action != null) {
			this.action.Activate ();
			this.transform.parent.gameObject.SetActive (false);
			yield return new WaitForSeconds (1.0f);
			GameObject.Destroy (this.action.gameObject);
		}
	}


	void ReadInText (){
		this.textAsset = Resources.Load (textLog.name) as TextAsset;

		if (this.textAsset != null)
			this.stringLength = textAsset.text.Length;
	}
}

