using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Activate : MonoBehaviour {
    public static bool activateflag;
    // Use this for initialization
    void Start () {
        //gameObject.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {

        activateflag = ButtonHandler.isPlayerTurn;
       // activateflag = false;
        Debug.Log("activateFlag is:"+ activateflag);
		if(activateflag == true)
        {
            gameObject.SetActive(true);
        }
        else
            gameObject.SetActive(false);
    }
}
