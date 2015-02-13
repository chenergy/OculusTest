using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public enum DataState {
	NONE,
	FLOCKING,
	BAR_ARRANGED,
	TRIANGLE
}

/// <summary>
/// these define the flock's behavior
/// </summary>
public class DataController : MonoBehaviour
{
	public float minVelocity = 5;
	public float maxVelocity = 20;
	public float randomness = 1;
	public int flockSize = 20;
	public int arrangedLength = 5;
	public Datum prefab;
	public Transform target;
	public Transform arrangedTarget;
	public PanelController panel;

	private DataState state = DataState.NONE;
	public DataState State {
		get { return this.state; }
	}

	internal Vector3 flockCenter;
	internal Vector3 flockVelocity;

	List<Datum> data = new List<Datum>();


	void Start()
	{
		for (int i = 0; i < flockSize; i++)
		{
			Datum newDatum = Instantiate(prefab, transform.position, transform.rotation) as Datum;
			newDatum.transform.parent = transform;
			newDatum.transform.localPosition = new Vector3(
							Random.value * collider.bounds.size.x,
							Random.value * collider.bounds.size.y,
							Random.value * collider.bounds.size.z) - collider.bounds.extents;

			newDatum.SetController (this);

			if (i > 0)
				newDatum.SetConnectionTarget (data [i - 1].transform);

			data.Add(newDatum);
		}

		data [0].SetConnectionTarget (data [data.Count - 1].transform);
			
		if (this.panel != null)
			this.panel.flocking.ButtonTurnsOn ();
		//this.EnableFlocking ();
		//this.EnableArranged ();
	}


	/*void Update()
	{
		if (this.state == DataState.FLOCKING) {
			Vector3 center = Vector3.zero;
			Vector3 velocity = Vector3.zero;

			foreach (Datum datum in data) {
				center += datum.transform.localPosition;
				velocity += datum.rigidbody.velocity;
			}
			flockCenter = center / flockSize;
			flockVelocity = velocity / flockSize;
		} else if (this.state == DataState.BAR_ARRANGED) {

		}
	}*/


	public void EnableFlocking (){
		if (this.state != DataState.FLOCKING) {
			foreach (Datum datum in data) {
				datum.EnableFlocking ();
			}
			this.state = DataState.FLOCKING;
		}
	}


	public void EnableArranged (){
		if (this.state != DataState.BAR_ARRANGED) {
			int[] positions = new int[this.arrangedLength];

			foreach (Datum datum in data) {
				Vector3 target = this.arrangedTarget.position;

				if (this.arrangedTarget != null) {
					int x = Random.Range (0, this.arrangedLength);
					int y = positions [x];

					positions [x]++;

					target.x += x * 5;
					target.y += y * 5;
				}

				datum.EnableArranged (target);
			}

			this.state = DataState.BAR_ARRANGED;
		}
	}

	public void EnableTriangle (){
		if (this.state != DataState.TRIANGLE) {
			this.state = DataState.TRIANGLE;
		}
	}
}