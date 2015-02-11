using UnityEngine;
using System.Collections;

public class MeshDistortion : MonoBehaviour
{
	public MeshFilter target;

	private Mesh mesh;

	// Use this for initialization
	void Start ()
	{
		mesh = target.mesh;
	}
	
	// Update is called once per frame
	void Update ()
	{
		Vector3[] vertices = mesh.vertices;

		for (int i = 0; i < vertices.Length; i++) {
			//Vector3 v = this.target.mesh.vertices [i];
			vertices [i].y = TrigLookup.Sin (i * Time.time * 0.01f);
		}

		mesh.vertices = vertices;
	}
}

