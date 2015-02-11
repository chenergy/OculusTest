using UnityEngine;
using System.Collections;
using Leap;

public class GlobeControl : MonoBehaviour
{
	public delegate void InteractAction ();
	public event InteractAction OnInteract;

	public Globe globe;
	public GameObject alertPrefab;
	public Transform target;
	public Transform alertEndpoint;
	public HandController handController;
	public PanelData[] panelData;


	private Controller controller;
	private Quaternion startRotation = Quaternion.identity;
	private Vector3 startPosition = Vector3.zero;
	private bool started = false;
	private bool completed = false;
	private GlobeAlert targetAlert;


	void Start ()
	{
		controller = new Controller();
		startRotation = globe.transform.rotation;
		startPosition = globe.transform.position;

		Vector3 alertPosition = Random.insideUnitSphere;

		this.targetAlert = (GameObject.Instantiate (this.alertPrefab, Vector3.zero, Quaternion.identity) as GameObject).GetComponent<GlobeAlert> ();
		this.targetAlert.transform.parent = globe.transform;
		this.targetAlert.transform.localPosition = Vector3.zero;
		this.targetAlert.transform.localRotation = Quaternion.LookRotation (alertPosition);

		this.targetAlert.alertEndpoint = this.alertEndpoint;
		//newAlert.transform.Rotate (alertPosition - globe.transform.position, 0.0f);
	}

	void Update ()
	{
		Frame frame = controller.Frame();

		if (!frame.Hands.IsEmpty) {
			if (this.handController.GetAllGraphicsHands ().Length > 0) {
				HandModel handModel = this.handController.GetAllGraphicsHands () [0];

				// Globe rotation
				if ((handModel.GetPalmPosition () - globe.transform.position).sqrMagnitude < 0.1f) {
					globe.collider.isTrigger = false;

					if (!this.started) {
						this.started = true;
						if (OnInteract != null)
							OnInteract ();
					}

					globe.lineRenderer.enabled = true;
					globe.lineRenderer.SetPosition (0, globe.transform.position);
					globe.lineRenderer.SetPosition (1, handModel.GetPalmPosition ());
				} else {
					this.started = false;
					globe.collider.isTrigger = true;
					globe.lineRenderer.enabled = false;
				}

				// Alert parenting
				if ((this.targetAlert.childAlert.transform.position - handModel.fingers [0].GetTipPosition ()).sqrMagnitude < 0.01f) {
					foreach (Hand hand in frame.Hands) {
						if (hand.PinchStrength > 0.5f) {
							this.completed = true;
							this.targetAlert.transform.parent = null;
							this.targetAlert.childAlert.transform.position = handModel.fingers [0].GetTipPosition ();
						} else if (this.completed && hand.PinchStrength < 0.5f) {
							foreach (PanelData panel in this.panelData) {
								panel.EnableData ();
							}
							this.targetAlert.GoToEndpoint ();
						}
					}
				}

				// Reset target and globe 
				target.localRotation = Quaternion.Lerp (
					target.localRotation, 
					globe.transform.localRotation, 
					Time.deltaTime * 5);
				globe.rigidbody.velocity = Vector3.zero;
				globe.transform.position = this.startPosition;
			}
		}
	}

	#if UNITY_STANDALONE_WIN
	[DllImport("mono", SetLastError=true)]
	static extern void mono_thread_exit();
	#endif

	void OnApplicationQuit() {
		controller.Dispose();
		#if UNITY_STANDALONE_WIN && !UNITY_EDITOR && UNITY_3_5
		mono_thread_exit ();
		#endif
	}
}

