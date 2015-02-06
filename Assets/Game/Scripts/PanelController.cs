using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PanelController : MonoBehaviour
{
	public DataController controller;

	public ButtonData flocking;
	public ButtonData arranged;
	public ButtonData other;
	public SliderData slider;

	public float panelOpenTime = 0.25f;
	public Image panelBG;
	public PanelData[] panels;

	private Vector3 startTarget = Vector3.zero;


	void OnEnable () {
		if (flocking != null)
			flocking.OnButtonEnabled += this.OnFlockingEnabled;

		if (arranged != null)
			arranged.OnButtonEnabled += this.OnArrangedEnabled;

		if (slider != null)
			slider.OnChangeSlider += this.OnChangePosition;
	}

	void OnDisable () {
		if (flocking != null)
			flocking.OnButtonEnabled -= this.OnFlockingEnabled;

		if (arranged != null)
			arranged.OnButtonEnabled -= this.OnArrangedEnabled;

		if (slider != null)
			slider.OnChangeSlider -= this.OnChangePosition;
	}


	// Use this for initialization
	void Start ()
	{
		this.startTarget = this.controller.target.position;
		//if (this.controller != null) {
			//this.ToggleButtons (DataState.FLOCKING);
			//flocking.TurnOn ();
		//}
	}

	public void ToggleButtons (DataState state){
		Debug.Log ("toggled to " + state.ToString ());
		switch (controller.State) {
		case DataState.FLOCKING:
			flocking.TurnOn ();
			arranged.TurnOff ();
			other.TurnOff ();
			break;
		case DataState.BAR_ARRANGED:
			flocking.TurnOff ();
			arranged.TurnOn ();
			other.TurnOff ();
			break;
		default:
			break;
		}
	}

	void OnFlockingEnabled (){
		if (controller.State != DataState.FLOCKING) {
			controller.EnableFlocking ();
			this.ToggleButtons (DataState.FLOCKING);

			foreach (PanelData panel in this.panels) {
				panel.DisableData ();
			}

			this.panelBG.gameObject.SetActive (false);

			Debug.Log ("flocking on");
		}
	}

	void OnArrangedEnabled (){
		if (controller.State != DataState.BAR_ARRANGED) {
			controller.EnableArranged ();
			this.ToggleButtons (DataState.BAR_ARRANGED);

			this.panelBG.gameObject.SetActive (true);

			StopCoroutine ("ArrangeRoutine");
			StartCoroutine ("ArrangeRoutine");

			Debug.Log ("arranged on");
		}
	}


	IEnumerator ArrangeRoutine (){
		float timer = 0.0f;
		float halfOpenTime = this.panelOpenTime / 2;
		Vector3 startScale = new Vector3 (0.1f, 0f, 1f);
		Vector3 midScale = new Vector3 (0.1f, 1f, 1f);
		Vector3 endScale = Vector3.one;

		this.panelBG.transform.localScale = Vector3.zero;

		while (timer < halfOpenTime) {
			this.panelBG.transform.localScale = Vector3.Lerp (startScale, midScale, timer / halfOpenTime);
			timer += Time.deltaTime;
			yield return new WaitForEndOfFrame ();
		}
		this.panelBG.transform.localScale = midScale;

		timer = 0.0f;

		while (timer < halfOpenTime) {
			this.panelBG.transform.localScale = Vector3.Lerp (midScale, endScale, timer / halfOpenTime);
			timer += Time.deltaTime;
			yield return new WaitForEndOfFrame ();
		}
		this.panelBG.transform.localScale = endScale;

		foreach (PanelData panel in this.panels) {
			panel.EnableData ();
		}
	}


	void OnChangePosition (float displacement){
		//Debug.Log (percent);
		this.controller.target.transform.localPosition = new Vector3 ((-0.5f + displacement) * 10,
				this.controller.target.transform.localPosition.y, 
				this.controller.target.transform.localPosition.z);
	}
}

