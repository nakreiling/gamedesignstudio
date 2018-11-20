using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static bool playerTurn;
    public static int rand;
    // Use this for initialization
     enum moves {ATTACK=1, STRIKE=2, ATKMAG=3, CHARGE=4, DEFEND=5, COUNTER=6, DEFMAG=7, GIVEUP=8 }
    void Start () {


        rand = 1; //=Random.Range(1, 3);
       
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Rand is: " + rand);
       
        //Debug.Log("rand is: "+ rand);
        if (rand == 1)//players turn
        {
            playerTurn = true;
        }
        
        else
        {
            playerTurn = false; //enemy turn
        }



    }
}
