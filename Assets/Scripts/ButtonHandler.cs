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
    public int selection;
    Vector3 myVector = new Vector3(-2f, .5f, -3.14f);
    Vector3 enemyVector = new Vector3(2f, .5f, -3.14f);
    RaycastHit hit;
    int layerMask = 1 << 20;

    



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


        //PlayerAction();
     



    }


    void Update()
    {
        //cheats
        if (Input.GetKeyDown(KeyCode.T)){
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
            if (buttonsMade == false)
            {
                ButtonCreate();
            }




            //   GameObject charge= GameObject.Find("Charge");

            //  buttonList[1].image = GameObject.Find("Charge").GetComponent<Image>();


            // .FindWithTag("Player").GetComponent<Attributes>()



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

            else if (Input.GetKeyDown(KeyCode.P))
            {
                GameManager.playerTurn = true;
            }

            if (Input.GetKeyDown(KeyCode.Space)) //players actions only work on his/her turn
            {


                buttonList[selectedButton].action();
                buttonList[selectedButton].image.color = Color.yellow;

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
                GameManager.rand = 2;
                buttonsMade = false;



            }
        }
        else //enemy turn
        {
            GameManager.rand = 2;
            // GameObject[] gos = GameObject.FindObjectsOfType(typeof(GameObject)) as GameObject[]; //will return an array of all GameObjects in the scene
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
                

                buttonList[selectedButton].action();
                buttonList[selectedButton].image.color = Color.yellow;

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
    void ButtonCreate() {
        

        if(isPlayerTurn == true)
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
        if(GameManager.rand == 1)
        {
            buttonList[0].image = GameObject.Find("Attack").GetComponent<Image>();
            buttonList[0].image.color = Color.white;
            buttonList[0].action = AttackButtonAction;

        }
        else
        {
            buttonList[0].image = GameObject.Find("Defend").GetComponent<Image>();
            buttonList[0].image.color = Color.white;
            buttonList[0].action = DefendButtonAction;
        }
        
        // Do the same for the second button. We are also ensuring the image colour is
        // set to our normalColor, to ensure uniformity.


        if (GameManager.rand ==1) //player is attacking
        {
           // Activate.activateflag = true;

            buttonList[1].image = GameObject.Find("Charge").GetComponent<Image>();
            buttonList[1].image.color = Color.white;
            buttonList[1].action = ChargeButtonAction;
        }
        else
        {
           // Activate.activateflag = false;

            buttonList[1].image = GameObject.Find("GiveUp").GetComponent<Image>();
            buttonList[1].image.color = Color.white;
            buttonList[1].action = GiveUpButtonAction;
        }
        
        // Do the same for the second button. We are also ensuring the image color is
        // set to our normalColor, to ensure uniformity.
        if(GameManager.rand == 1)
        {
            buttonList[2].image = GameObject.Find("Strike").GetComponent<Image>();
            buttonList[2].image.color = Color.white;
            buttonList[2].action = StrikeButtonAction;

        }
        else
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
        else {

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
        float attackAmount = 0;
        float defenseAmount = 0;
        float currentHealth = 0;
        Attributes attack;
        Attributes defense;
        EnemyStats health;
        int count = 0;
        Debug.Log("Attack");
        if (GameObject.FindWithTag("Enemy"))
        {
            GameObject enemy = GameObject.FindWithTag("Enemy");

            if (attack = GameObject.FindWithTag("Player").GetComponent<Attributes>()) //if the player exists get that object's atk value
            {

                attackFlag = true;//Now the AnimScript should trigger the animation for the attack

                attackAmount = attack.getAttack();
                // Debug.Log("The attack would normally do " + attackAmount + " damage!");
                //Check defense of enemy

                defense = GameObject.FindWithTag("Enemy").GetComponent<Attributes>();

                defenseAmount = defense.getDefense();
                // Debug.Log("The defense of the enemy is " + defenseAmount);
                //modify the atk amount
                attackAmount = (attackAmount * defenseAmount);

                //finalize amount
                attackAmount = -attackAmount;
                //Debug.Log("It did " + attackAmount + "Damage");


                health = GameObject.FindWithTag("Enemy").GetComponent<EnemyStats>();

                currentHealth = health.currentHealth;

                //set the anim back to idle, first need to turn attackFlag to false


                StartCoroutine(Example());

               // Debug.Log(attackFlag);






            }


            // Debug.Log(GameObject.FindWithTag("Enemy"));
            EnemyStats stats;
            if (stats = GameObject.FindWithTag("Enemy").GetComponent<EnemyStats>())
            {

                stats.ChangeHealth(attackAmount);
                
                currentHealth = stats.currentHealth;
               // Debug.Log("Target has " + currentHealth + " health total");
                //Do a check to see if equal to zero and then make object a dead object hehehe

            }

        }

        foreach (Transform Choice in transform)
        {
            // Debug.Log("DELETE");
            Choice.gameObject.SetActive(false);
            selection = 0;

        }

    }


    void ChargeButtonAction()
    {

        Debug.Log("CHARGGGGH");
    }

    void AtkMagicButtonAction()
    {
        Debug.Log("Atk magic release!");
    }

    ///<summary>This is the method that will call when selecting "Strike".</summary>
    void StrikeButtonAction()
    {
        int count = 0;
         Debug.Log("Strike");

        if (GameObject.FindWithTag("Enemy"))
        {
            count++;
            //Debug.Log(count);
            //Debug.Log(GameObject.FindWithTag("Enemy"));
            EnemyStats stats;
            if (stats = GameObject.FindWithTag("Enemy").GetComponent<EnemyStats>())
            {
                stats.ChangeHealth(-40);

            }


        }
        foreach (Transform Choice in transform)
        {
            Choice.gameObject.SetActive(false);
            selection = 2;

        }
    }

    //DEFENSE METHODS

    ///<summary>This is the method that will call when selecting "Defend".</summary>
    void DefendButtonAction()
    {
         Debug.Log("Defend");
        foreach (Transform Choice in transform)
        {
            Choice.gameObject.SetActive(false);
            selection = 1;

        }
    }
    void GiveUpButtonAction()
    {
        Debug.Log("I SURRENDER");

    }
    void DefMagicButtonAction()
    {
        Debug.Log("Def magic release!");
    }
    ///<summary>This is the method that will call when selecting "Counter".</summary>
    void CounterButtonAction()
    {
        Debug.Log("Counter");
        foreach (Transform Choice in transform)
        {
            Choice.gameObject.SetActive(false);
            selection = 3;

        }
    }


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