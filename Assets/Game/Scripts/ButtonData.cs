using UnityEngine;
using System.Collections;

public class ButtonData : ButtonDemoToggle
{
	public delegate void ButtonOn ();
	public event ButtonOn OnButtonEnabled;

	public override void ButtonTurnsOn()
	{
		this.TurnOn ();
		if (this.OnButtonEnabled != null)
			this.OnButtonEnabled ();
	}

	public override void ButtonTurnsOff (){
	}


	public void TurnOn ()
	{
		this.TurnsOnGraphics ();
		this.SetMinDistance(onDistance);
		this.toggle_state_ = true;
	}

	public void TurnOff()
	{
		this.TurnsOffGraphics();
		this.SetMinDistance(offDistance);
		this.toggle_state_ = false;
	}
}

