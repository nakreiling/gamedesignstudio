using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static bool playerTurn, timelineActive;
    public static int rand, count;
    // Use this for initialization
    Vector3 attckPos;
    Vector3 defendPos;
    Vector3 strikePos;
    Vector3 counterPos;
    Vector3 chargePos;
    Vector3 AtkMagPos;
    Vector3 DefMagPos;
    Vector3 giveUpPos;

    Vector3 tempAtk, tempDefend, tempCounter, tempStrike, tempAtkMag, tempDefMag, tempCharge, tempGiveUp;
    GameObject rockPlayer,rockEnemy, attack, defend, strike, charge, giveUp, AtkMag, DefMag, counter;
    //Collection of all objects
    GameObject[] gos;

    enum moves {ATTACK=1, STRIKE=2, ATKMAG=3, CHARGE=4, DEFEND=5, COUNTER=6, DEFMAG=7, GIVEUP=8 }
    void Start () {

        timelineActive = false;

        //Get list of objects that can be set active later

        gos = GameObject.FindObjectsOfType(typeof(GameObject)) as GameObject[]; //will return an array of all GameObjects in the scene

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

         attckPos= attack.transform.position;
         defendPos = defend.transform.position;
         strikePos = strike.transform.position;
         counterPos = counter.transform.position;
         chargePos = charge.transform.position;
         AtkMagPos = AtkMag.transform.position;
         DefMagPos = DefMag.transform.position;
         giveUpPos = giveUp.transform.position;

        tempAtk = attckPos;
        tempDefend = defendPos;
        tempCounter = counterPos;
        tempStrike = strikePos;
        tempAtkMag = AtkMagPos;
        tempDefMag = DefMagPos;
        tempCharge = chargePos;
        tempGiveUp = giveUpPos;


        //Turn them off

        // attack.SetActive(false); defend.SetActive(false); strike.SetActive(false);
        //counter.SetActive(false); charge.SetActive(false); AtkMag.SetActive(false);
        //DefMag.SetActive(false); attack.SetActive(false);giveUp.SetActive(false);




        count = 0;
        rand = 1; //=Random.Range(1, 3);

        
       
        
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
        }
        
        //turn off ui during cutscenes
        if(timelineActive == true)
        {
            foreach (GameObject go in gos)
            {
                if (go.layer == (9) && go.CompareTag("OffenseChoice"))
                {
                    go.SetActive(false); //turns off the button objects :o only when space is pressed
                                         // Debug.Log("DESTROY BUTTONS-PLAYER CHOICE MADE");
                }
            }

            foreach (GameObject go in gos)
            {
                if (go.layer == (9) && go.CompareTag("DefenseChoice"))
                {
                    go.SetActive(false); //turns off the button objects :o only when space is pressed
                                         // Debug.Log("DESTROY BUTTONS-PLAYER CHOICE MADE");
                }
            }


            Debug.Log("it is true");
            attckPos = new Vector3(0, 220, 0);
            defendPos = new Vector3(0, 220, 0);
            counterPos = new Vector3(0, 220, 0);
            strikePos = new Vector3(0, 220, 0);
            AtkMagPos = new Vector3(0, 220, 0);
            DefMagPos = new Vector3(0, 220, 0);
            chargePos = new Vector3(0, 220, 0);
            giveUpPos = new Vector3(0, 220, 0);
           // Debug.Log("MOVE");
        }
        else if(timelineActive == false)
        {


            Debug.Log("it is now false");


            attack.transform.position = tempAtk;
            defend.transform.position = tempDefend;
            counter.transform.position = tempCounter;
            strike.transform.position = tempStrike;
            AtkMag.transform.position = tempAtkMag;
            DefMag.transform.position = tempDefMag;
            charge.transform.position = tempCharge;
            giveUp.transform.position = tempGiveUp;
           // Debug.Log("Come BACK");

        }



    }

    public static int addCount()
    {
       return count++;
    }

    
}
