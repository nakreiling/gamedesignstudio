using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ActivateDefense : MonoBehaviour
{
    public static bool activateDEF;
    // Use this for initialization
    void Start()
    {
        //gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        activateDEF = ButtonHandler.isPlayerTurn;
        Debug.Log("activateFlag is:" + activateDEF);
        if (activateDEF == false)
        {
            gameObject.SetActive(true);
        }
        else
            gameObject.SetActive(false);
    }
}
