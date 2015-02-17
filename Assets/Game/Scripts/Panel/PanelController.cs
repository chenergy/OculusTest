using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PanelController : MonoBehaviour
{
	public DataController controller;

	public ButtonData flocking;
	public ButtonData arranged;
	public ButtonData triangle;
	public SliderData slider;
	public GameObject fingerTracker;

	public float panelOpenTime = 0.25f;
	public Image panelBG;
	public ActionTree triangleTree;
	public PanelData[] panels;

	private Vector3 startTarget = Vector3.zero;


	void OnEnable () {
		if (flocking != null)
			flocking.OnButtonEnabled += this.OnFlockingEnabled;

		if (arranged != null)
			arranged.OnButtonEnabled += this.OnArrangedEnabled;

		if (triangle != null)
			triangle.OnButtonEnabled += this.OnTriangleEnabled;

		if (slider != null)
			slider.OnChangeSlider += this.OnChangePosition;
	}

	void OnDisable () {
		if (flocking != null)
			flocking.OnButtonEnabled -= this.OnFlockingEnabled;

		if (arranged != null)
			arranged.OnButtonEnabled -= this.OnArrangedEnabled;

		if (triangle != null)
			triangle.OnButtonEnabled -= this.OnTriangleEnabled;

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
		this.fingerTracker.SetActive (false);
	}

	public void ToggleButtons (DataState state){
		Debug.Log ("toggled to " + state.ToString ());
		switch (controller.State) {
		case DataState.FLOCKING:
			flocking.TurnOn ();
			arranged.TurnOff ();
			triangle.TurnOff ();
			break;
		case DataState.BAR_ARRANGED:
			flocking.TurnOff ();
			arranged.TurnOn ();
			triangle.TurnOff ();
			break;
		case DataState.TRIANGLE:
			flocking.TurnOff ();
			arranged.TurnOff ();
			triangle.TurnOn ();
			break;
		default:
			break;
		}
	}

	void OnFlockingEnabled (){
		if (controller.State != DataState.FLOCKING) {
			controller.EnableFlocking ();
			this.ToggleButtons (DataState.FLOCKING);

			this.OnArrangedDisabled ();
			this.OnTriangleDisabled ();

			Debug.Log ("flocking on");
		}
	}


	void OnFlockingDisabled (){
	}


	void OnArrangedEnabled (){
		if (controller.State != DataState.BAR_ARRANGED) {
			controller.EnableArranged ();
			this.ToggleButtons (DataState.BAR_ARRANGED);
			this.panelBG.gameObject.SetActive (true);
			this.fingerTracker.SetActive (true);

			StopCoroutine ("ArrangeRoutine");
			StartCoroutine ("ArrangeRoutine");

			this.OnFlockingDisabled ();
			this.OnTriangleDisabled ();

			Debug.Log ("arranged on");
		}
	}


	void OnArrangedDisabled (){
		foreach (PanelData panel in this.panels) {
			panel.DisableData ();
		}
		this.panelBG.gameObject.SetActive (false);
		this.fingerTracker.SetActive (false);
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


	void OnTriangleEnabled (){
		if (controller.State != DataState.TRIANGLE) {
			controller.EnableTriangle ();
			this.ToggleButtons (DataState.TRIANGLE);
			this.triangleTree.Activate ();

			this.OnFlockingDisabled ();
			this.OnArrangedDisabled ();

			Debug.Log ("triangle on");
		}
	}


	void OnTriangleDisabled (){
		this.triangleTree.DeActivate ();
	}


	void OnChangePosition (float displacement){
		//Debug.Log (percent);
		this.controller.target.transform.localPosition = new Vector3 ((-0.5f + displacement) * 10,
				this.controller.target.transform.localPosition.y, 
				this.controller.target.transform.localPosition.z);
	}
}

