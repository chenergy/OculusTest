using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public class GameManager : MonoBehaviour
{
	public static GameManager instance = null;

	public DataController boidController;

	void Awake (){
		instance = this;
	}
}

