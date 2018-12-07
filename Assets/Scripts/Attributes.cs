using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attributes : MonoBehaviour {
 
    [SerializeField] private float attack;
    [SerializeField] private float defense;
    [SerializeField] private float strike;


    
    public float getAttack()
    {
       return attack;
    }

    public float getDefense()
    {
        return defense;
    }
    public float getStrike()
    {
        return strike;
    }


  public void changeStrike(float amount)
    {
        strike = strike*amount;
    }
   public void changeAttack(float amount)
    {
        attack = attack * amount;
    }
   public float changeDefense(float amount)
    {
        defense = defense * amount;
        return defense;
    }

    

    public bool ChangeBool(bool x)
    {
        bool timelineActive=false;

        if (timelineActive == true)
        {
            timelineActive = false;
        }
        else
        {
            Debug.Log("You shouldn't be here");
           timelineActive = false;
        }

        return timelineActive;
    }




}
