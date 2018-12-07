using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoolSwitch : MonoBehaviour {


    public bool ChangeTimeLineActive(bool x)
    {
        if (GameManager.timelineActive == true)
        {
            GameManager.timelineActive = false;
        }
        else
        {
            Debug.Log("You shouldn't be here");
            GameManager.timelineActive = false;
        }

        return GameManager.timelineActive;
    }


    }

