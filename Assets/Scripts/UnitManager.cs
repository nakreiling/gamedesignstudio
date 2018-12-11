using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UnitManager : MonoBehaviour {

    // Idea for this script is to record both unit data and position for multiple units
    //Goal is that when a battle starts from Map, the Map position is recorded and after Battle ends the health of the surviving unit is kept as it
    //Goes back to if recorded position {Vector 3 and Quaterion}

    public static UnitManager Instance; //instance what does that mean here?
    public static List<int> unitXList;
    public static List<int> unitYList;
    public static List<int> healthValue;
    public static int selectedUnit;
    public static int selectedEnemy;
    public static int battleWinner;
    public static bool tileMapUsed = false;
    private static float playerBattleResultHealth, enemyBattleResultHealth;
    private static Vector3 playerMapPosition, enemyMapPosition;
    private static Quaternion playerRotation, enemyRotation;  //idea is to use these two variables in conjunction to record where on the map the two combatants are before switching 
    //public float PlayerH; //this is just to test if info is being recorded from battle or not
    //public float EnemyH; 
   

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
            //I think this is a typo, we neeed to specifically only have the one GameObject
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

    public static Vector3 PlayerMapPosition
    {
        get
        {
            return playerMapPosition;
        }
        set
        {
            playerMapPosition = value;
        }
    }

    public static Vector3 EnemyMapPosition
    {
        get
        {
            return enemyMapPosition;
        }
        set
        {
            enemyMapPosition = value;
        }
    }

    public static Quaternion PlayerRotation
    {
        get
        {
            return playerRotation;
        }
        set
        {
            playerRotation = value;
        }
    }

    public static Quaternion EnemyRotation
    {
        get
        {
            return enemyRotation;
        }
        set
        {
            enemyRotation = value;
        }
    }


    public void Reset() //From the game title screen, this will reset the static class, effectively resetting the game
    {
     c static UnitManager Instance; //instance what does that mean here?
    public static List<int> unitXList;
    public static List<int> unitYList;
    public static List<int> healthValue;
    public static int selectedUnit;
    public static int selectedEnemy;
    public static int battleWinner;
    public static bool tileMapUsed = false;
}


    /*
    //Player Static Data recorded for testing purposes here, single target only right now
    public float PlayerH = PlayerBattleResultHealth; //this is just to test if info is being recorded from battle or not
    public Vector3 PlayerP = PlayerMapPosition; //This doesn't work like how I thought
    public Quaternion PlayerR = PlayerRotation;
    //Enemy Data single target only currently
    public float EnemyH = EnemyBattleResultHealth;
    public Vector3 EnemyP = EnemyMapPosition;
    public Quaternion EnemyR = EnemyRotation;
    */

}

