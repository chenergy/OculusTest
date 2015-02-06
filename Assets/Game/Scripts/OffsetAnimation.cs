using UnityEngine;
using System.Collections;

public class OffsetAnimation : MonoBehaviour 
{
	public GameObject iShield;
	public float iSpeed;

	private Material mMaterial;
	private float mTime;

	// Use this for initialization
	void Start () 
	{
		mMaterial = iShield.renderer.material;

		mTime = 0.0f;
	}

	// Update is called once per frame
	void Update () 
	{
		mTime += Time.deltaTime * iSpeed;

		mMaterial.SetTextureOffset ("_MainTex", new Vector2 (Mathf.Repeat (mTime, 1.0f), 0));
	}
}

