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


    void changeStrike(float amount)
    {
        strike = strike*amount;
    }
    void changeAttack(float amount)
    {
        attack = attack * amount;
    }
    void changeDefense(float amount)
    {
        defense = defense * amount;
    }
   


}
