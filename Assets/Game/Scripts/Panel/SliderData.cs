using UnityEngine;
using System.Collections;

public class SliderData : SliderDemo
{
	public delegate void OnUpdatePosition (float percent);
	public event OnUpdatePosition OnChangeSlider;

	public override void UpdatePosition(Vector3 displacement){
		base.UpdatePosition (displacement);

		if (this.OnChangeSlider != null)
			this.OnChangeSlider (displacement.x);
	}
}

