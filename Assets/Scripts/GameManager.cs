using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public static bool playerTurn, timelineActive, promptOver;
    public static int rand, count, playerPrompt, enemyPrompt;
    private float temp;
    Stats enemyStats;
    Stats playerStats;
    //PlayerPrompt...1-defend 2-counter 3-giveUp 4-defMagic 5-attack 6-strike 7 -charge 8-atkMag
    //Enemy Prompt...9-defend 10-counter giveUp-11, defMagic-12 attack-13 strike-14 charge-15 atkMag-16
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
    GameObject rockPlayer, rockEnemy, attack, defend, strike, charge, giveUp, AtkMag, DefMag, counter, atkPromptPlayer, atkMagPromptPlayer, chargePromptPlayer, strikePromptPlayer, defPromptPlayer, counterPromptPlayer, giveUpPromptPlayer, defMagPromptPlayer, atkPromptEnemy, strikePromptEnemy, chargePromptEnemy, atkMagPromptEnemy, defPromptEnemy, giveUpPromptEnemy, counterPromptEnemy, defMagPromptEnemy;
    //Above is all object I turn off at start included sprites I need.
    //Collection of all objects
    GameObject[] gos;

    enum moves { ATTACK = 1, STRIKE = 2, ATKMAG = 3, CHARGE = 4, DEFEND = 5, COUNTER = 6, DEFMAG = 7, GIVEUP = 8 }


    void Start() {

        GameObject[] player_units = GameObject.FindGameObjectsWithTag("Player");
        GameObject[] enemy_units = GameObject.FindGameObjectsWithTag("Enemy");
        timelineActive = false;

        //Get list of objects that can be set active later

        gos = GameObject.FindObjectsOfType(typeof(GameObject)) as GameObject[]; //will return an array of all GameObjects in the scene

        atkPromptPlayer = GameObject.Find("atkPromptPlayer");
        atkPromptPlayer.SetActive(false);//test here

        atkMagPromptPlayer = GameObject.Find("atkMagPromptPlayer");
        atkMagPromptPlayer.SetActive(false);

        strikePromptPlayer = GameObject.Find("strikePromptPlayer");
        strikePromptPlayer.SetActive(false);

        chargePromptPlayer = GameObject.Find("chargePromptPlayer");
        chargePromptPlayer.SetActive(false);
        //Defense Player
        defPromptPlayer = GameObject.Find("defPromptPlayer");
        defPromptPlayer.SetActive(false);//test here

        counterPromptPlayer = GameObject.Find("counterPromptPlayer");
        counterPromptPlayer.SetActive(false);//test here

        giveUpPromptPlayer = GameObject.Find("giveUpPromptPlayer");
        giveUpPromptPlayer.SetActive(false);

        defMagPromptPlayer = GameObject.Find("defMagPromptPlayer");
        defMagPromptPlayer.SetActive(false);

        //enemy sprite turn off Enemy
        atkPromptEnemy = GameObject.Find("atkPromptEnemy");
        atkPromptEnemy.SetActive(false);//test here

        atkMagPromptEnemy = GameObject.Find("atkMagPromptEnemy");
        atkMagPromptEnemy.SetActive(false);

        strikePromptEnemy = GameObject.Find("strikePromptEnemy");
        strikePromptEnemy.SetActive(false);

        chargePromptEnemy = GameObject.Find("chargePromptEnemy");
        chargePromptEnemy.SetActive(false);
        //Defense sprite enemy
        defPromptEnemy = GameObject.Find("defPromptEnemy");
        defPromptEnemy.SetActive(false);//test here

        counterPromptEnemy = GameObject.Find("counterPromptEnemy");
        counterPromptEnemy.SetActive(false);//test here

        giveUpPromptEnemy = GameObject.Find("giveUpPromptEnemy");
        giveUpPromptEnemy.SetActive(false);

        defMagPromptEnemy = GameObject.Find("defMagPromptEnemy");
        defMagPromptEnemy.SetActive(false);


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
        DefMag = GameObject.Find("DefMagic");
        giveUp = GameObject.Find("GiveUp");

        attckPos = attack.transform.position;
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

        enemyStats = GameObject.FindWithTag("Enemy").GetComponent<Stats>();
        playerStats = GameObject.FindWithTag("Player").GetComponent<Stats>();

        count = 0;
        rand = 1; //=Random.Range(1, 3);




    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.X))
        {
            turnOffMagic();
            // Activate.activateflag = true;
            Debug.Log("Magic IS gone");
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            playerStats = GameObject.FindWithTag("Player").GetComponent<Stats>();
            playerStats.ChangeHealth(-100);

            foreach (GameObject go in gos)
                {
                  if (go.layer == (10) && go.CompareTag("Player"))
                {
                  go.SetActive(false); //turns off the button objects :o only when space is pressed
                 Debug.Log("DESTROY -Player");
                }
                }

        }
        else if (Input.GetKeyDown(KeyCode.V))
        {
            enemyStats.ChangeHealth(-100);
            enemyStats = GameObject.FindWithTag("Enemy").GetComponent<Stats>(); 
            foreach (GameObject go in gos)
            {
                if (go.layer == (11) && go.CompareTag("Enemy"))
                {
                    go.SetActive(false); //turns off the button objects :o only when space is pressed
                    Debug.Log("DESTROY -Enemy");
                }
            }
        }
        else if (Input.GetKeyDown(KeyCode.Q)){

            Application.Quit();
        }
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


        if (GameObject.FindGameObjectsWithTag("Player").Length < 1 || GameObject.FindWithTag("Player").GetComponent<Stats>().getHealth() < 1)
        {
           // GameObject.FindWithTag("Player").SetActive(false);
            rockPlayer.SetActive(true); //2nd verse same as the first, need a time buffer and then unload the scene, load overworld
                                        //To do: Timed Buffer
                                        //Debug.Log("I am the Player and its time to leave!"); //Player died
            float battleEndHealth = GameObject.FindWithTag("Enemy").GetComponent<Stats>().getHealth(); //recording the Health of the surviving, need to put it into the Unit Manager...
            UnitManager.battleWinner = UnitManager.enemyS;
            UnitManager.healthValue[UnitManager.battleWinner] = (int)battleEndHealth;
            Debug.Log("NPC survived with: " + battleEndHealth);
            Debug.Log("Battle winner index: " + UnitManager.battleWinner);
            SceneManager.LoadScene("OverWorld"); //, LoadSceneMode.Single); //might need to switch to Asynchronous mode
        }
        else if (GameObject.FindGameObjectsWithTag("Enemy").Length < 1 || GameObject.FindWithTag("Enemy").GetComponent<Stats>().getHealth() < 1) //was <= 0
        {  //To do: Delete the Unit from the overall Unit list
           // GameObject.FindWithTag("Enemy").SetActive(false); //So... if this works....nullreference error? should have a time buffer and unload the scene
            rockEnemy.SetActive(true); //I think we have to explicitly remove the NPC as well?
            //Debug.Log("I'm out of Here! Love Skelly."); //NPC died
            float battleEndHealth = GameObject.FindWithTag("Player").GetComponent<Stats>().getHealth();
            UnitManager.battleWinner = UnitManager.unitS;
            UnitManager.healthValue[UnitManager.battleWinner] = (int)battleEndHealth;
            Debug.Log("Player survived with: " + battleEndHealth); //Derp a lerp
            Debug.Log("Battle winner index: " + UnitManager.battleWinner);
            SceneManager.LoadScene("OverWorld"); //, LoadSceneMode.Single);
                                                 
                                                 /*if (GameObject.FindGameObjectsWithTag("Player").Length < 1)
                                                 {
                                                     rockPlayer.SetActive(true);

                                                 }
                                                 else if (GameObject.FindGameObjectsWithTag("Enemy").Length < 1)
                                                 {
                                                     rockEnemy.SetActive(true);
                                                 }*/

        }
            //Debug.Log(timelineActive);
            //turn off ui during cutscenes
            if (timelineActive == true)
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


            // Debug.Log("it is true");
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
        else if (timelineActive == false)
        {


            //  Debug.Log("it is now false");

            /*
            attack.transform.position = tempAtk;
            defend.transform.position = tempDefend;
            counter.transform.position = tempCounter;
            strike.transform.position = tempStrike;
            AtkMag.transform.position = tempAtkMag;
            DefMag.transform.position = tempDefMag;
            charge.transform.position = tempCharge;
            giveUp.transform.position = tempGiveUp;
           // Debug.Log("Come BACK");
           */

        }

   

    }
  
    
    public static int addCount()
    {
       return count++;
    }

    public void turnOffMagic()
    {
        foreach (GameObject go in gos)
        {
            if (go.layer == (12) && go.CompareTag("Magic"))
            {
                go.SetActive(false); //turns off  magic
            }
        }
        timelineActive = false; //brings back the UI
    }
    
    public void setPlayerSprites()
    {
       // Debug.Log(playerPrompt+ "player");
        switch (playerPrompt)
        {
            case 1:
                defPromptPlayer.SetActive(true);
                break;//repeat
            case 2:
                counterPromptPlayer.SetActive(true);
                break;//repeat
            case 3:
                giveUpPromptPlayer.SetActive(true);
                break;//repeat
            case 4:
                defMagPromptPlayer.SetActive(true);
                break;//repeat
            case 5:
                atkPromptPlayer.SetActive(true);
                break;//repeat
            case 6:
                strikePromptPlayer.SetActive(true);
                break;//repeat
            case 7:
                chargePromptPlayer.SetActive(true);
                break;//repeat
            case 8:
                atkMagPromptPlayer.SetActive(true);
                break;//repeat
        }
        

    }
    public void pokoioi()
    {
        SceneManager.LoadScene("Overworld");
    }

    public void setEnemySprites()
    {
       // Debug.Log(enemyPrompt+ "enemy");
        switch (enemyPrompt)
        {
            case 9:
                defPromptEnemy.SetActive(true);
                break;//repeat
            case 10:
                counterPromptEnemy.SetActive(true);
                break;
            case 11:
                giveUpPromptEnemy.SetActive(true);
                break;
            case 12:
                defMagPromptEnemy.SetActive(true);
                break;
            case 13:
                atkPromptEnemy.SetActive(true);
                break;
            case 14:
                strikePromptEnemy.SetActive(true);
                break;
            case 15:
                chargePromptEnemy.SetActive(true);
                break;
            case 16:
                atkMagPromptEnemy.SetActive(true);
                break;
        }

    }

    public void resetPrompts()
    {
        Debug.Log("RESET");
        foreach (GameObject go in gos)
        {
            if (go.layer == (5) && go.CompareTag("Prompt"))
            {
                go.SetActive(false); //turns off prompts
                                    
            }

        }

        timelineActive = false;
        promptOver = true;
        //THESE TWO LINES MIGHT BE NEEDED AT A LATER TIME
       // playerPrompt = 0;
       // enemyPrompt = 0;
    }
}
