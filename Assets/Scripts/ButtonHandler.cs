using UnityEngine;
using UnityEngine.UI;
using System.Collections;

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



    }


    void Update()
    {
        //Debug.Log(GameManager.rand + "is the deciding thing");
     
         

        //cheats
        if (Input.GetKeyDown(KeyCode.T))
        {
           // GameManager.turnOffMagic();
           // Activate.activateflag = true;
            Debug.Log("IS gone");
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
                if(GameManager.timelineActive == false)
                {
                    foreach (GameObject go in gos)
                    {
                        if (go.layer == (9) && go.CompareTag("OffenseChoice"))
                        {
                            go.SetActive(false); //turns off the button objects :o only when space is pressed
                                                 // Debug.Log("DESTROY BUTTONS-PLAYER CHOICE MADE");
                        }
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
                action = GameObject.FindWithTag("Enemy").GetComponent<EnemyActions>();

                move = Random.Range(1, 5); //chooses a number between 1 and 4 //neeed 5
                //Set the appropiate methods
                if (move == 1)
                {
                    enemyMove = "defend";
                    Debug.Log("it is set");
                    GameManager.enemyPrompt = 9;
                    action.defenseMethod(); //call the attack action from the skeleton script 
                    
                }
                else if (move == 2)
                {
                    enemyMove = "counter";
                    GameManager.enemyPrompt = 10;
                    action.counterMethod();
                    
                }
                else if (move == 3)
                {
                    enemyMove = "giveUp";
                    GameManager.enemyPrompt = 11;
                    action.giveUpMethod();
                    

                }
                else if (move == 4)
                {
                    enemyMove = "defMagic";
                    GameManager.enemyPrompt = 12;
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
            if(GameManager.timelineActive == false)
            {
                foreach (GameObject go in gos)
                {
                    if (go.layer == (9) && go.CompareTag("DefenseChoice"))
                    {
                        go.SetActive(true); //turns on/off the button objects 

                    }

                }
            }
            

            if (buttonsMade == false) //watch it
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

                move =  Random.Range(1, 5); //chooses a number between 1 and 4 
                counterFlag = false;

                buttonList[selectedButton].action();
                buttonList[selectedButton].image.color = Color.yellow; //Do action after the changes are made

                //Set the appropiate methods
                if (move == 1)
                {
                    GameManager.enemyPrompt = 13;
                    action.attackMethod(); //call the attack action from the skeleton script 
                    enemyMove = "attack";
                }
                else if (move == 2)
                {
                    GameManager.enemyPrompt = 14;
                    enemyMove = "strike";
                    strikeFlag = true;
                    action.strikeMethod();

                }
                else if (move == 3)
                {
                    GameManager.enemyPrompt = 15;
                    action.chargeMethod();
                    enemyMove = "charge";
                }
                else if (move == 4)
                {
                    GameManager.enemyPrompt = 16;
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