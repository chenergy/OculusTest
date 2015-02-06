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
	}

	public void TurnOff()
	{
		this.TurnsOffGraphics();
	}
}

