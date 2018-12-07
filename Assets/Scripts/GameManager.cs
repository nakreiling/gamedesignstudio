using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //added to be able to switch between Battle and Overworld scences

public class GameManager : MonoBehaviour {
    public static bool playerTurn;
    public static int rand, count;
    // Use this for initialization

    GameObject rockPlayer,rockEnemy, attack, defend, strike, charge, giveUp, AtkMag, DefMag, counter;
    enum moves {ATTACK=1, STRIKE=2, ATKMAG=3, CHARGE=4, DEFEND=5, COUNTER=6, DEFMAG=7, GIVEUP=8 }
    void Start () {

        //Get list of objects that can be set active later

        rockPlayer = GameObject.Find("RockPlayer");
        rockEnemy = GameObject.Find("RockEnemy");

        rockPlayer.SetActive(false);
        rockEnemy.SetActive(false);
        
        attack = GameObject.Find("Attack");
        defend = GameObject.Find("Defend");
        strike = GameObject.Find("Strike");
        counter = GameObject.Find("Counter");
        charge = GameObject.Find("Charge");
        AtkMag = GameObject.Find("AtkMagic");
        DefMag= GameObject.Find("DefMagic");
        giveUp = GameObject.Find("GiveUp");
        

        //Turn them off
       
       // attack.SetActive(false); defend.SetActive(false); strike.SetActive(false);
        //counter.SetActive(false); charge.SetActive(false); AtkMag.SetActive(false);
        //DefMag.SetActive(false); attack.SetActive(false);giveUp.SetActive(false);
        



        count = 0;
        rand = 2; //=Random.Range(1, 3);

        
       
        
    }

    // Update is called once per frame
    void Update()
    {
       //  Debug.Log("Rand is: " + rand);
        //Debug.Log(playerTurn + "equals Player Turn ");
        //Debug.Log("rand is: "+ rand);
        if (rand == 1)//players turn
        {
            defend.SetActive(false); 
            counter.SetActive(false); 
            DefMag.SetActive(false);
            giveUp.SetActive(false);

            //attack.SetActive(true); strike.SetActive(true); AtkMag.SetActive(true); charge.SetActive(true);
            playerTurn = true;

        }
        
        else
        {
            //defend.SetActive(true); counter.SetActive(true); DefMag.SetActive(true); giveUp.SetActive(true);
            playerTurn = false; //enemy turn
        }

        


        if(GameObject.FindGameObjectsWithTag("Player").Length < 1)
        {
            rockPlayer.SetActive(true);
        }
        else if(GameObject.FindGameObjectsWithTag("Enemy").Length < 1)
        {
            rockEnemy.SetActive(true);
        }//adding some statements to account for when a units health is 0, Concern: Units health only decreases in battle right?
        else if(GameObject.FindWithTag("Player").GetComponent<Stats>().getHealth() <= 0)
        {
            GameObject.FindWithTag("Player").SetActive(false);
            rockPlayer.SetActive(true); //2nd verse same as the first, need a time buffer and then unload the scene, load overworld
            //To do: Timed Buffer
            Debug.Log("I am the Player and its time to leave!");
            SceneManager.LoadScene("OverWorld"); //, LoadSceneMode.Single); //might need to switch to Asynchronous mode
        }
        else if (GameObject.FindWithTag("Enemy").GetComponent<Stats>().getHealth() <= 0)
        {  //To do: Delete the Unit from the overall Unit list
            GameObject.FindWithTag("Enemy").SetActive(false); //So... if this works....nullreference error? should have a time buffer and unload the scene
            rockEnemy.SetActive(true); //I think we have to explicitly remove the NPC as well?
            Debug.Log("I'm out of Here! Love Skelly");
            SceneManager.LoadScene("OverWorld"); //, LoadSceneMode.Single);
        }
        //else if (GameObject.FindWithTag("Player").GetComponent<Stats>().getHealth() <= 0)
        //{
            
        //}

    }

    public static int addCount()
    {
       return count++;
    }
}
