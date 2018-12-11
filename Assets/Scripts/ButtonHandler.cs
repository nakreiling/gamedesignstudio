using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System; //to perform duh maths
using Random = UnityEngine.Random;

public class ButtonHandler : MonoBehaviour //change name to TurnHandler when merged
{

    GameObject[] gos;
    public static bool isPlayerTurn;
    public static bool attackFlag, defendFlag, counterFlag, strikeFlag;
    //public static bool isPlayerTurn;
    bool buttonsMade;
    ///<summary>Placeholder delegate function for our buttonList</summary>
    public delegate void ButtonAction();
    ///<summary>Array of buttons, created from a struct, below.</summary>
    public MyButton[] buttonList;
    ///<summary>Index reference to our currently selected button.</summary>
    public int selectedButton = 0;
    public int prevButton = 0;
    public static int selection;
    Vector3 myVector = new Vector3(-2f, .5f, -3.14f);
    Vector3 enemyVector = new Vector3(2f, .5f, -3.14f);
    RaycastHit hit;
    int layerMask = 1 << 20;
    public static string enemyMove;

    GameObject attack, defend, strike, charge, giveUp, AtkMag, DefMag, counter;
    PlayerActions playerAction;
    EnemyActions action;
    public static int move;



    // private void Start()
    // {
    // playerTurn = true; // can be changed upon what overworld state
    //do
    //{
    //  DisplayButtons();
    //PlayerAction();

    //} while (playerTurn == true);

    //  if (playerTurn == true)
    // {
    //    DisplayButtons();

    //}
    // else playerTurn = false;



    // }





    void Start()
    {
        gos = GameObject.FindObjectsOfType(typeof(GameObject)) as GameObject[]; //will return an array of all GameObjects in the scene
        buttonsMade = false;

        attack = GameObject.Find("Attack");
        defend = GameObject.Find("Defend");
        strike = GameObject.Find("Strike");
        counter = GameObject.Find("Counter");
        charge = GameObject.Find("Charge");
        AtkMag = GameObject.Find("AtkMagic");
        DefMag = GameObject.Find("DefMagic");
        giveUp = GameObject.Find("GiveUp");


        //Turn the buttons off, Game Manager determines which ones are turned on from start
        /*
        attack.SetActive(false);
        defend.SetActive(false);
        strike.SetActive(false);
        counter.SetActive(false);
        charge.SetActive(false);
        AtkMag.SetActive(false);
        DefMag.SetActive(false);
        giveUp.SetActive(false);
        */
        //float battleEndHealth = GameObject.FindWithTag("Player").GetComponent<Stats>().getHealth();
        //UnitManager.PlayerBattleResultHealth = battleEndHealth; //I think this is giving Player 100 health, but doesn't explain skelly having 50 when he should have 75

        //needs to be changed to identify unit based on position, look at TileMap for how to alter
        GameObject.FindWithTag("Enemy").GetComponent<Stats>().setHealth(UnitManager.healthValue[UnitManager.selectedEnemy]); //we need a setter for the Units health that can set it based on what is from the Static (DB) class
        Debug.Log("The Battle Mode has started,known health of Enemy is: " + UnitManager.healthValue[UnitManager.selectedEnemy]);//Derpy?
        GameObject.FindWithTag("Player").GetComponent<Stats>().setHealth(UnitManager.healthValue[UnitManager.selectedUnit]); //okay so if I did this correctly we will pass Health data (and more) from the two scenes
        Debug.Log("The Battle Mode has started,known health of pdog is: " + UnitManager.healthValue[UnitManager.selectedUnit]);//RRREEEEE
    }


    void Update()
    {
        //Debug.Log(GameManager.rand + "is the deciding thing");
     
         

        //cheats
        if (Input.GetKeyDown(KeyCode.T))
        {
            Activate.activateflag = true;
            Debug.Log("IS TROO");
        }
        else if (Input.GetKeyDown(KeyCode.F))
        {
            Activate.activateflag = false;
            Debug.Log("IZZ FALSZ");
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            isPlayerTurn = true;
            Debug.Log("Playturn has changed");
        }


        isPlayerTurn = GameManager.playerTurn; //

        //Debug.Log("isPlayerTurn is: "+ isPlayerTurn);



        if (isPlayerTurn)
        {

            strikeFlag = false;//reset bool
            if (buttonsMade == false)
            {
                attack.SetActive(true);
                defend.SetActive(false);
                strike.SetActive(true);
                counter.SetActive(false);
                charge.SetActive(true);
                AtkMag.SetActive(true);
                DefMag.SetActive(false);
                giveUp.SetActive(false);

                ButtonCreate();

            }

            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                buttonList[prevButton].image.color = Color.white;
                prevButton = 3;
                selectedButton = 3;
                buttonList[selectedButton].image.color = Color.yellow;
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {

                buttonList[prevButton].image.color = Color.white;
                prevButton = 2;
                selectedButton = 2;
                buttonList[selectedButton].image.color = Color.yellow;
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                buttonList[prevButton].image.color = Color.white;
                prevButton = 0;
                selectedButton = 0;
                buttonList[selectedButton].image.color = Color.yellow;
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                buttonList[prevButton].image.color = Color.white;
                prevButton = 1;
                selectedButton = 1;
                buttonList[selectedButton].image.color = Color.yellow;
            }



            if (Input.GetKeyDown(KeyCode.Space)) //players actions only work on his/her turn
            {


                //buttonList[selectedButton].action();
                //buttonList[selectedButton].image.color = Color.yellow;

                foreach (GameObject go in gos)
                {
                    if (go.layer == (9) && go.CompareTag("OffenseChoice"))
                    {
                        go.SetActive(false); //turns off the button objects :o only when space is pressed
                                             // Debug.Log("DESTROY BUTTONS-PLAYER CHOICE MADE");
                    }
                }

                // GameObject[] gos = GameObject.FindObjectsOfType(typeof(GameObject)) as GameObject[]; //will return an array of all GameObjects in the scene

                //foreach (GameObject go in gos)
                //{
                //  if (go.layer == (9) && go.CompareTag("OffsenseChoice"))
                //{
                //  go.SetActive(false); //turns off the button objects :o only when space is pressed
                // Debug.Log("DESTROY BUTTONS-PLAYER CHOICE MADE");
                //}
                //}
                //Debug.Log("PRESSED");

                //To do: Create FSM based on Health of enemy and the status of the Player Unit

                action = GameObject.FindWithTag("Enemy").GetComponent<EnemyActions>();

                //need to create another health3 to represent the NPC will create cases for Defense based on that
                //can use a Switch statement to alter bases on the class of the Player character; "Knight, Mage, Solider" etc.
                //also can still implement a discrete cases based on the fact that the range of health can be considered a discrete case

                float health = GameObject.FindWithTag("Player").GetComponent<Stats>().getHealth(); //GameObject.FindWithTag("Player").GetComponent<Stats>.getHealth() //Better way to do this?
                int health2 = (int)Math.Ceiling(health);//why was this such a pain in the arse?

                float healthE = GameObject.FindWithTag("Enemy").GetComponent<Stats>().getHealth();
                //.GetComponent<Stats>().getHealth();//u""h oh...where is the Enemy's health stored at???
                int health3 = (int)Math.Ceiling(healthE); //this is for the NPC/Enemy's health
                //If the above line "Math" specify that you want to use the UnityEngine method for random 
                if (health3 <= 100 && health3 > 75) //I just realized that I have 4 discrete cases could have used a Switch statement anyway...fail...
                { //This is case I when against the Knight class
                    int dice = Random.Range(1, 102); //so use UnityEngine Random is the correct way?
                    if (dice <= 50)
                    {
                        enemyMove = "defend";
                        //action.attackMethod();
                    }
                    else //change back to else if but shouldn't need it if odds are 50/50
                    {
                        enemyMove = "counter";
                       // strikeFlag = true;
                        //action.strikeMethod();
                    }
                    /*
                    else if (dice >= 28 && dice < 42)
                    {
                        action.chargeMethod();
                        enemyMove = "giveup";
                    }
                    else
                    {
                        action.atkMagMethod();
                        enemyMove = "defMagic";
                    }
                    */
                }
                else if (health3 <= 75 && health3 > 50) //case II for Knight Class
                {
                    int dice = Random.Range(1, 104); //so unlike with a Switch Statement have to creat the "dice" variable each if scope
                    if (dice <= 40)
                    {
                        enemyMove = "defend";
                        //action.attackMethod();
                    }
                    else if (dice >= 41 && dice > 81)
                    {
                        enemyMove = "counter";
                        //strikeFlag = true;
                        //action.strikeMethod();
                    }
                    else   // realzied I didn't need this else if ( dice >=82 && dice < 102)
                    {
                       // action.chargeMethod();//is this line an error? Need to look into chargeMethod, how does it affect/control "giveUp"
                        enemyMove = "giveUp";
                    }
                    /*
                    else
                    {
                        action.atkMagMethod();
                        enemyMove = "defMagic";
                    }
                    */
                }
                else if (health3 <= 50 && health3 > 25) //case III 
                {
                    int dice = Random.Range(1, 104);
                    if (dice <= 40)
                    {
                        enemyMove = "defend";
                        //action.attackMethod();
                    }
                    else if (dice >= 41 && dice < 56)
                    {
                        enemyMove = "counter";
                        //strikeFlag = true;
                       // action.strikeMethod();
                    }
                    else if (dice >= 56 && dice < 62)
                    {
                        //action.chargeMethod();
                        enemyMove = "giveUp";
                    }
                    else
                    {
                       // action.atkMagMethod();
                        enemyMove = "defMagic";
                    }
                }
                else //case 4 Health as less than 25%
                {
                    int dice = Random.Range(1, 104);
                    if (dice <= 15)
                    {
                        enemyMove = "defend";
                      //  action.attackMethod();
                    }
                    else if (dice >= 15 && dice > 31)
                    {
                        enemyMove = "counter";
                       // strikeFlag = true;
                       // action.strikeMethod();
                    }
                    else if (dice >= 31 && dice < 42)
                    {
                       // action.chargeMethod();
                        enemyMove = "giveUp";
                    }
                    else
                    {
                        //action.atkMagMethod();
                        enemyMove = "defMagic";
                    }
                }


                switch (enemyMove)
                {
                    case "defend":
                        move = 1;
                        break;
                    case "counter":
                        move = 2;
                        break;
                    case "giveUp":
                        move = 3;
                        break;

                    case "defMagic":
                        move = 4;
                        break;

                }
                //move = Random.Range(1, 5); //chooses a number between 1 and 4 //neeed 5
                //Set the appropiate methods
                
                // consider replacing with a Switch Statement instead
                
                if (move == 1)
                {
                    enemyMove = "defend";
                    action.defenseMethod(); //call the attack action from the skeleton script 
                    
                }
                else if (move == 2)
                {
                    enemyMove = "counter";
                    action.counterMethod();
                    
                }
                else if (move == 3)
                {
                    enemyMove = "giveUp";
                    action.giveUpMethod();
                    

                }
                else if (move == 4)
                {
                    enemyMove = "defMagic";
                    action.defMagMethod();
                    
                }
                


                buttonList[selectedButton].action();
                buttonList[selectedButton].image.color = Color.yellow;

                GameManager.rand = 2;
                GameManager.addCount(); //increase turn number
                                        // Debug.Log(GameManager.count + " has increased");
                buttonsMade = false;



            }
        }
        else //enemy turn
        {
            //counterFlag = false; //reset upon turn switch so no repeat guarentee
            //GameManager.rand = 2; THIS WAS THE PROBLEM

            foreach (GameObject go in gos)
            {
                if (go.layer == (9) && go.CompareTag("DefenseChoice"))
                {
                    go.SetActive(true); //turns on/off the button objects 

                }

            }

            if (buttonsMade == false)
            {
                ButtonCreate();
            }

            //give player actions
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                buttonList[prevButton].image.color = Color.white;
                prevButton = 3;
                selectedButton = 3;
                buttonList[selectedButton].image.color = Color.yellow;
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                //counterFlag = true;
                buttonList[prevButton].image.color = Color.white;
                prevButton = 2;
                selectedButton = 2;
                buttonList[selectedButton].image.color = Color.yellow;
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                buttonList[prevButton].image.color = Color.white;
                prevButton = 0;
                selectedButton = 0;
                buttonList[selectedButton].image.color = Color.yellow;
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                buttonList[prevButton].image.color = Color.white;
                prevButton = 1;
                selectedButton = 1;
                buttonList[selectedButton].image.color = Color.yellow;
            }

            else if (Input.GetKeyDown(KeyCode.P))
            {
                GameManager.playerTurn = true;
            }

            if (Input.GetKeyDown(KeyCode.Space)) //players actions only work on his/her turn
            {

                //enemy selection OFFENSE


                action = GameObject.FindWithTag("Enemy").GetComponent<EnemyActions>();

                //move =  Random.Range(1, 5); //chooses a number between 1 and 4 
                counterFlag = false;

                buttonList[selectedButton].action();
                buttonList[selectedButton].image.color = Color.yellow; //Do action after the changes are made

                float health = GameObject.FindWithTag("Player").GetComponent<Stats>().getHealth(); //GameObject.FindWithTag("Player").GetComponent<Stats>.getHealth() //Better way to do this?
                int health2 = (int)Math.Ceiling(health);//why was this such a pain in the arse?

                //Include an if statement that checks for the defense of the Player Unit, if it is "high" then use else if for "med" and else for "low"
                //in the if statement encapsulate the Switch for an engagement with a unit that has different attributes
                

                if (health2 >= 100)
                {
                    int dice = Random.Range(1, 102); //so use UnityEngine Random is the correct way?
                    if (dice <= 13)
                    {
                        enemyMove = "Attack";
                        Debug.Log("Skelly Attack Case I"); //need this to figure out how I broke combat :O
                        //action.attackMethod();
                    }
                    else if (dice >= 14 && dice < 28)
                    {
                        enemyMove = "Strike";
                        //strikeFlag = true;
                        //action.strikeMethod();
                    }
                    else if (dice >= 28 && dice < 42)
                    {
                        //action.chargeMethod();
                        enemyMove = "charge";
                    }
                    else
                    {
                       // action.atkMagMethod();
                        enemyMove = "atkMagic";
                    }
                }
                else if (health2 < 100 && health2 >= 75)
                {
                    int dice = Random.Range(1, 104); //so unlike with a Switch Statement have to creat the "dice" variable each if scope
                    if (dice <= 10)
                    {
                        enemyMove = "Attack";
                        Debug.Log("Skelly Attack Case II");
                        //action.attackMethod();
                    }
                    else if (dice >= 11 && dice < 41)
                    {
                        enemyMove = "Strike";
                        //strikeFlag = true;
                       // action.strikeMethod();
                    }
                    else if (dice >= 41 && dice < 83)
                    {
                        //action.chargeMethod();
                        enemyMove = "charge";
                    }
                    else
                    {
                        //action.atkMagMethod();
                        enemyMove = "atkMagic";
                    }
                }
                else if (health2 < 74 && health2 >= 50)
                {
                    int dice = Random.Range(1, 104);
                    if (dice <= 25)
                    {
                        enemyMove = "Attack";
                        Debug.Log("Skelly Attack Case III");
                       // action.attackMethod();
                    }
                    else if (dice >= 26 && dice < 66)
                    {
                        enemyMove = "Strike";
                       // strikeFlag = true;
                       // action.strikeMethod();
                    }
                    else if (dice >= 67 && dice < 87)
                    {
                       // action.chargeMethod();
                        enemyMove = "charge";
                    }
                    else
                    {
                        //action.atkMagMethod();
                        enemyMove = "atkMagic";
                    }
                }
                else
                {
                    int dice = Random.Range(1, 104);
                    if (dice <= 40)
                    {
                        enemyMove = "Attack";
                        Debug.Log("Skelly Attack Case IV"); 
                       // action.attackMethod();
                    }
                    else if (dice >= 41 && dice < 81)
                    {
                        enemyMove = "Strike";
                      //  strikeFlag = true;
                       // action.strikeMethod();
                    }
                    else if (dice >= 82 && dice < 92)
                    {
                        //action.chargeMethod();
                        enemyMove = "charge";
                    }
                    else
                    {
                        //action.atkMagMethod();
                        enemyMove = "atkMagic";
                    }
                }


                switch (enemyMove)
                {
                    case "attack":
                        move = 1;
                        break;
                    case "strike":
                        move = 2;
                        break;
                    case "charge":
                        move = 3;
                        break;

                    case "atkMagic":
                        move = 4;
                        break;

                }

                //Set the appropiate methods

                if (move == 1)
                {
                    action.attackMethod(); //call the attack action from the skeleton script 
                    enemyMove = "attack";
                }
                else if (move == 2)
                {
                    enemyMove = "strike";
                    strikeFlag = true;
                    action.strikeMethod();

                }
                else if (move == 3)
                {
                    action.chargeMethod();
                    enemyMove = "charge";
                }
                else if (move == 4)
                {
                    action.atkMagMethod();
                    enemyMove = "atkMagic";
                }
                
                //Debug.Log(strikeFlag);



                // GameObject[] gos = GameObject.FindObjectsOfType(typeof(GameObject)) as GameObject[]; //will return an array of all GameObjects in the scene
                foreach (GameObject go in gos)
                {
                    if (go.layer == (9) && go.CompareTag("DefenseChoice"))
                    {
                        go.SetActive(false); //turns off the button objects :o only when space is pressed
                                             // Debug.Log("DESTROY BUTTONS-PLAYER CHOICE MADE");
                    }
                }
                //Debug.Log("PRESSED");
                GameManager.rand = 1;
                GameManager.addCount(); //increase turn number
                                        //  Debug.Log(GameManager.count + " has increased");
                buttonsMade = false;

            }

            //To do here
            // Debug.Log("Skelly time");
            if (Input.GetKeyDown(KeyCode.P))
            {
                GameManager.rand = 1;
            }

        }

    }


    //Method which creates buttons
    void ButtonCreate()
    {


        if (isPlayerTurn == true)
        {
            foreach (GameObject go in gos)
            {
                if (go.layer == (9) && go.CompareTag("OffenseChoice"))
                {
                    go.SetActive(true); //turns on/off the button objects 
                                        // Debug.Log("CREATE BUTTONS");
                }

            }
        }
        else
            foreach (GameObject go in gos)
            {
                if (go.layer == (9) && go.CompareTag("DefenseChoice"))
                {
                    go.SetActive(true); //turns on/off the button objects 
                                        // Debug.Log("CREATE BUTTONS");
                }
            }


        // Instantiate buttonList to hold the amount of buttons we are using.
        buttonList = new MyButton[4];
        // Set up the first button, finding the game object based off its name. We also 
        // must set the expected onClick method, and should trigger the selected colour.
        if (GameManager.rand == 1)
        {
            buttonList[0].image = GameObject.Find("Attack").GetComponent<Image>();
            buttonList[0].image.color = Color.white;
            buttonList[0].action = AttackButtonAction;

        }
        else if (GameManager.rand == 2)
        {
            buttonList[0].image = GameObject.Find("Defend").GetComponent<Image>();
            buttonList[0].image.color = Color.white;
            buttonList[0].action = DefendButtonAction;
        }

        // Do the same for the second button. We are also ensuring the image colour is
        // set to our normalColor, to ensure uniformity.


        if (GameManager.rand == 1) //player is attacking
        {
            // Activate.activateflag = true;

            buttonList[1].image = GameObject.Find("Charge").GetComponent<Image>();
            buttonList[1].image.color = Color.white;
            buttonList[1].action = ChargeButtonAction;
        }
        else if (GameManager.rand == 2)
        {
            // Activate.activateflag = false;

            buttonList[1].image = GameObject.Find("GiveUp").GetComponent<Image>();
            buttonList[1].image.color = Color.white;
            buttonList[1].action = GiveUpButtonAction;
        }

        // Do the same for the second button. We are also ensuring the image color is
        // set to our normalColor, to ensure uniformity.
        if (GameManager.rand == 1)
        {
            buttonList[2].image = GameObject.Find("Strike").GetComponent<Image>();
            buttonList[2].image.color = Color.white;
            buttonList[2].action = StrikeButtonAction;

        }
        else if (GameManager.rand == 2)
        {
            buttonList[2].image = GameObject.Find("Counter").GetComponent<Image>();
            buttonList[2].image.color = Color.white;
            buttonList[2].action = CounterButtonAction;
        }


        // Do the same for the second button. We are also ensuring the image color is
        // set to our normalColor, to ensure uniformity.
        if (GameManager.rand == 1)
        {
            buttonList[3].image = GameObject.Find("AtkMagic").GetComponent<Image>();
            buttonList[3].image.color = Color.white;
            buttonList[3].action = AtkMagicButtonAction;
        }
        else if (GameManager.rand == 2)
        {

            buttonList[3].image = GameObject.Find("DefMagic").GetComponent<Image>();
            buttonList[3].image.color = Color.white;
            buttonList[3].action = DefMagicButtonAction;
        }


        buttonsMade = true;

    }




    //OFFENSE ACTIONS

    ///<summary>This is the method that will call when selecting "Attack".</summary>
    void AttackButtonAction()
    {

        // attackFlag = true;//animator
        playerAction = GameObject.FindWithTag("Player").GetComponent<PlayerActions>();

        playerAction.attackMethod();
        // Debug.Log(attackFlag + "VARIABNELKFN");

      
    }


    void ChargeButtonAction()
    {
        playerAction = GameObject.FindWithTag("Player").GetComponent<PlayerActions>();

        playerAction.chargeMethod();
        // Debug.Log("CHARGGGGH");
    }

    void AtkMagicButtonAction()
    {
        playerAction = GameObject.FindWithTag("Player").GetComponent<PlayerActions>();

        playerAction.atkMagMethod();
    }

    ///<summary>This is the method that will call when selecting "Strike".</summary>
    void StrikeButtonAction()
    {
        PlayerActions action;
        // attackFlag = true;//animator
        action = GameObject.FindWithTag("Player").GetComponent<PlayerActions>();

        action.strikeMethod();

    }

    //DEFENSE METHODS

    ///<summary>This is the method that will call when selecting "Defend".</summary>
    void DefendButtonAction()
    {
        PlayerActions action;
        // attackFlag = true;//animator
        action = GameObject.FindWithTag("Player").GetComponent<PlayerActions>();

        action.defenseMethod();
    }
    void GiveUpButtonAction()
    {
        PlayerActions action;
        // attackFlag = true;//animator
        action = GameObject.FindWithTag("Player").GetComponent<PlayerActions>();

        action.giveUpMethod();

    }
    void DefMagicButtonAction()
    {
        Debug.Log("Def magic release!");
        PlayerActions action;
       
        action = GameObject.FindWithTag("Player").GetComponent<PlayerActions>();

        action.defMagMethod();

    }
    ///<summary>This is the method that will call when selecting "Counter".</summary>
    void CounterButtonAction()
    {
        playerAction = GameObject.FindWithTag("Player").GetComponent<PlayerActions>();

        playerAction.counterMethod();
    }
    //Enemy Actions



    /*private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Debug.DrawLine(origin, origin + dir * currentHitDistance);
        Gizmos.DrawWireSphere(origin + dir * currentHitDistance, sphereRadius);
    }*/
    IEnumerator Example()
    {

        yield return new WaitForSeconds(1f);
        attackFlag = false;
        // Debug.Log(attackFlag);
        yield return attackFlag;

    }
    //Wait buffer for health change
    IEnumerator Buffer()
    {

        yield return new WaitForSeconds(1f);

    }

    ///<summary>A struct to represent individual buttons. This makes it easier to wrap
    /// the required variables into a single container. Don't forget 
    /// [System.Serializable], if you wish to see your final array in the inspector.
    [System.Serializable]
    public struct MyButton
    {
        /// <summary>The image contained in the button.</summary>
        public Image image;
        /// <summary>The delegate method to invoke on action.</summary>
        public ButtonAction action;
    }
}