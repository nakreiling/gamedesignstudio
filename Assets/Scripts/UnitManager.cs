using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UnitManager : MonoBehaviour {

    // Idea for this script is to record both unit data and position for multiple units
    //Goal is that when a battle starts from Map, the Map position is recorded and after Battle ends the health of the surviving unit is kept as it
    //Goes back to if recorded position {Vector 3 and Quaterion}

    public static UnitManager Instance; //instance what does that mean here?

    private static float playerBattleResultHealth, enemyBattleResultHealth;
    public float PlayerH; //this is just to test if info is being recorded from battle or not
    public float EnemyH; 
   

    private void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
            
        }
        else if( Instance != this)
        {
            Destroy(gameObject); //is it okay to have white space here? Weird.
        }
    }

    void Start () {

        GameObject[] player_units = GameObject.FindGameObjectsWithTag("Player");
        GameObject[] enemy_units = GameObject.FindGameObjectsWithTag("Enemy"); //going to use this to help use peserve units between scenes

    }

    // Update is called once per frame
    void Update () {
		
	}

    //okay this is really just a test script to see if we can get the Health "out" of the battle
    public static float PlayerBattleResultHealth
    {
        get
        {
            return playerBattleResultHealth;
        }
        set
        {
            playerBattleResultHealth = value;
        }
    }

    public static float EnemyBattleResultHealth
    {
        get
        {
            return enemyBattleResultHealth;
        }
        set
        {
            enemyBattleResultHealth = value;
        }
    }

}
