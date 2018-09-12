using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battle_Input : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (GameObject.Find("CubeRight") != null)
        {

            transform.localPosition = new Vector3(0f, .5f, -3.14f);
        }

    }
    
}

