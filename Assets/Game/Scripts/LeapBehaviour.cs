using UnityEngine;
using System.Collections;
using Leap;

public class LeapBehaviour : MonoBehaviour {
	Controller controller;
	
	void Start ()
	{
		controller = new Controller();
		controller.EnableGesture (Gesture.GestureType.TYPECIRCLE);
	}
	
	void Update ()
	{
		Frame frame = controller.Frame();
		// do something with the tracking data in the frame...

		if (!frame.Hands.IsEmpty) {
			//Debug.Log ("hands present");

			foreach (Hand hand in frame.Hands){
				if (hand.IsLeft){
					//Debug.Log ("left");
				} else if (hand.IsRight){
					Debug.Log ("right");
					foreach (Gesture g in frame.Gestures ()) {
						if (g.Type == Gesture.GestureType.TYPECIRCLE){
							Debug.Log ("performed circle gesture");
						}
					}
				}
			}
		}

		/*
		if (frame.Hand (0)) {
			Debug.Log ("left hand present");
		}

		if (frame.Hand (1) != null) {
			Debug.Log ("right hand present");
		}*/
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