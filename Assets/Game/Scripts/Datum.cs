using UnityEngine;
using System.Collections;

[RequireComponent (typeof (DataFlocking), typeof (LineRenderer))]
public class Datum : MonoBehaviour
{
	DataController controller = null;
	DataFlocking flocking = null;
	DataArranged arranged = null;

	Transform connectionTarget = null;

	LineRenderer lineRenderer;

	// Use this for initialization
	void Awake ()
	{
		this.flocking = this.GetComponent <DataFlocking> ();
		this.arranged = this.GetComponent <DataArranged> ();

		this.flocking.enabled = false;
		this.arranged.enabled = false;

		this.lineRenderer = this.GetComponent <LineRenderer> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		this.lineRenderer.SetPosition (0, this.transform.position);
		this.lineRenderer.SetPosition (1, this.controller.target.transform.position);

		/*if (this.connectionTarget != null)
			this.lineRenderer.SetPosition (1, this.connectionTarget.transform.position);*/
	}

	public void SetController (DataController controller) {
		this.controller = controller;
		this.flocking.controller = controller;
	}

	public void SetConnectionTarget (Transform target){
		this.connectionTarget = target;
	}

	public void EnableFlocking (){
		this.arranged.enabled = false;
		this.rigidbody.isKinematic = false;

		this.flocking.enabled = true;
		this.flocking.StartCoroutine ("StartRoutine");
	}

	public void EnableArranged (Vector3 target){
		this.flocking.enabled = false;
		this.flocking.StopCoroutine ("StartRoutine");
		this.rigidbody.isKinematic = true;

		this.arranged.enabled = true;
		this.arranged.SetTarget (target);
	}
}

