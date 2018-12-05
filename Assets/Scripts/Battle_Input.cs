using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battle_Input : MonoBehaviour {
    Animator anime;
    public static Animator anim;
    public int thingy;
    GameObject player;

    // Use this for initialization
    void Start () {
		anime = gameObject.GetComponent<Animator>();
        //thingy = gameObject.GetComponent<ButtonHandler>().selection;
     
    }
	
	// Update is called once per frame
	void Update () {

       // thingy = GetComponent<ButtonHandler>().selection; //Cannot access variable in ButtonHandler See prof.
        if (thingy == 0) //attack
        {

            anime.SetInteger("attack", 0);
        }
        else
            print("TRY AGAIN");

    }
    
}

