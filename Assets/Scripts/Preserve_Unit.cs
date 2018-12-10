using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Preserve_Unit : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}





    void Awake()
    {
      //   GameObject[] objs = GameObject.FindGameObjectsWithTag("music");
      /*
        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }
       */
        DontDestroyOnLoad(this.gameObject); //More of a Test script and perhaps later a failsafe..
        /*Idea is to use this test script first to see if I can perseve units between scences or not, don't expect it to be pretty
         */
    }
}
