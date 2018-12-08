using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
   

    Stats enemyStats;
    Stats playerStats;
    public static bool isCountering;
    int turnCheck;
   public bool physicalAtkBuff, physicalDefBuff;
    //Clips to use for this object
    [SerializeField] private TimeLineScript promptSet;
    [SerializeField] private TimeLineScript attackP;
    [SerializeField] private TimeLineScript magicFailP;    


    // public static ArrayList enemyCollection = new ArrayList();
    public delegate void AttackDelegate();
    public delegate void StrikeDelegate();
    public delegate void ChargeDelegate();
    public delegate void AtkMagicDelegate();
    //Defense
    public delegate void DefenseDelegate();
    public delegate void CounterDelegate();
    public delegate void GiveUpDelegate();
    public delegate void DefMagicDelegate();

    public AttackDelegate attackMethod;
    public StrikeDelegate strikeMethod;
    public ChargeDelegate chargeMethod;
    public AtkMagicDelegate atkMagMethod;
    //Defense
    public AttackDelegate defenseMethod;
    public StrikeDelegate counterMethod;
    public ChargeDelegate giveUpMethod;
    public AtkMagicDelegate defMagMethod;

    private void Start()
    {
        

        if (GameObject.FindWithTag("Player"))
        {

            GameObject enemy = GameObject.FindWithTag("Player");
            Debug.Log(enemy.name);
            //find out which enemy it is...set methods according to spefic model
            if (enemy.name == "ToonKnight")
            {
                //Offsense
                attackMethod = ToonAttackButtonAction; //test in Button Handler by calling the variable
                strikeMethod = ToonStrikeButtonAction;
                chargeMethod = ToonChargeButtonAction;
                atkMagMethod = ToonAtkMagicButtonAction;
                //Defense
                defenseMethod = ToonDefendButtonAction;
                counterMethod = ToonCounterButtonAction;
                giveUpMethod = ToonGiveUpButtonAction;
                defMagMethod = ToonDefMagicButtonAction;

            }
            //else if(enemy.name  == "other"){   set different/ same methods depending on class}

        }
    }



    public void ToonAttackButtonAction()
    {
        GameManager.playerPrompt = 5;
        GameManager.timelineActive = true;
        GameManager.promptOver = false;
        promptSet.PlayFromTimeLines(3);//Lets see should change the var in other method...resetPrompt GM
                                       //Call iterator
        StartCoroutine(WaitforPromptOver());

        
           // GameManager.timelineActive = true;
           // attackP.PlayFromTimeLines(0);
        
       

        //TrueToonAttackButtonAction();

       

       // foreach (Transform Choice in transform)
       // {
       //     // Debug.Log("DELETE");
       //     Choice.gameObject.SetActive(false);
       //     selection = 0;

      //  }

    }

    
    
    public void TrueToonAttackButtonAction() //called from timeline
    {
        Debug.Log("YOU are attacking!");
        float attackAmount = 0;
        float defenseAmount = 0;
        float currentHealth = 0;
        Attributes attack;
        Attributes defense;
        Stats health;
        int count = 0;
        // Debug.Log("Attack");
        if (GameObject.FindWithTag("Enemy"))
        {

            // TimeLineScript clip = GetComponent<TimeLineScript>();
            //clip.PlayFromTimeLines(0); //Error is on this line

            GameManager.timelineActive = true;




            //  TimeLineScript.PlayFromTimeLines(0);//timeline atk soldier
            GameObject enemy = GameObject.FindWithTag("Enemy");

            if (attack = GameObject.FindWithTag("Player").GetComponent<Attributes>()) //if the player exists get that object's atk value
            {

                ButtonHandler.attackFlag = true;//Now the AnimScript should trigger the animation for the attack

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


                health = GameObject.FindWithTag("Enemy").GetComponent<Stats>();

                currentHealth = health.currentHealth;

                //set the anim back to idle, first need to turn attackFlag to false

                StartCoroutine(Buffer());
                StartCoroutine(Example());

                // Debug.Log(attackFlag);



                GameManager.timelineActive = false;//should now bring back the UI


            }


            // Debug.Log(GameObject.FindWithTag("Enemy"));
            Stats stats;
            if (stats = GameObject.FindWithTag("Enemy").GetComponent<Stats>())
            {

                stats.ChangeHealth(attackAmount);

                currentHealth = stats.currentHealth;
                // Debug.Log("Target has " + currentHealth + " health total");
                //Do a check to see if equal to zero and then make object a dead object hehehe
                if (physicalAtkBuff == true)
                {
                    ToonRevertAttack(); //decrease the power buff
                    physicalAtkBuff = false;
                }


            }

        }
        GameManager.timelineActive = false;//put in every TRUE method
    }
    public void ToonStrikeButtonAction()
    {
        GameManager.playerPrompt = 6;
        GameManager.timelineActive = true;
        GameManager.promptOver = false;
        promptSet.PlayFromTimeLines(3);//Lets see should change the var in other method...resetPrompt GM
                                       //Call iterator
        StartCoroutine(WaitforPromptOver());
    }

    public void TrueToonStrikeButtonAction()
    {
       
        bool dmgDone=false;
            if (ButtonHandler.enemyMove == "counter")  //if the enemy chooses to counter when the player strikes
            {
                playerStats = GameObject.FindWithTag("Player").GetComponent<Stats>();
            
                playerStats.ChangeHealth(-99);
                dmgDone = true;
            Debug.Log(dmgDone);
            Debug.Log("GET FUCKED");
            GameManager.timelineActive = false;//put in every TRUE method
        }
            else if(ButtonHandler.enemyMove != "counter")
             { 
                 if (enemyStats = GameObject.FindWithTag("Enemy").GetComponent<Stats>())
                  {
                    if (physicalAtkBuff == true && dmgDone ==false)
                    {
                        enemyStats.ChangeHealth(-160);
                        Debug.Log("Double Strike");
                        ToonRevertAttack();
                    physicalAtkBuff = false;
                    GameManager.timelineActive = false;//put in every TRUE method

                }
                    else 
                    {
                    if(dmgDone == false)
                    {
                        
                        enemyStats.ChangeHealth(-40);
                        Debug.Log("Regular Strike");
                        GameManager.timelineActive = false;//put in every TRUE method
                    }
                        
                    }
                  }

            }


        //foreach (Transform Choice in transform)
        // {
        //    Choice.gameObject.SetActive(false);
        //    ButtonHandler.selection = 2;

        // }
        GameManager.timelineActive = false;//put in every TRUE method
    }
    public void ToonRevertAttack() {
        Attributes attack;
        attack = GameObject.FindWithTag("Player").GetComponent<Attributes>();
        Debug.Log("before revert:" + attack);

        attack.changeAttack(.5f);
        Debug.Log("after revert:" + attack);

       
    }
    public void ToonRevertDefense()
    {
        Attributes defense;
        defense = GameObject.FindWithTag("Player").GetComponent<Attributes>();

        Debug.Log("before revert:" + defense);

        defense.changeDefense(2f);
        Debug.Log("after revert:" + defense);
    }

    public void ToonChargeButtonAction()
    {
        GameManager.playerPrompt = 7;
        GameManager.timelineActive = true;
        GameManager.promptOver = false;
        promptSet.PlayFromTimeLines(3);//Lets see should change the var in other method...resetPrompt GM
                                       //Call iterator
        StartCoroutine(WaitforPromptOver());
    }

        public void TrueToonChargeButtonAction() //Increase the player's attack damage by 50% the next attack (req turn count?)
    {
        
        Attributes attack;
        attack = GameObject.FindWithTag("Player").GetComponent<Attributes>();
        Debug.Log("before charge:"+ attack);

        attack.changeAttack(2);
        Debug.Log("after charge:" + attack);

        physicalAtkBuff = true;
        GameManager.timelineActive = false;//put in every TRUE method

    }
    public void ToonAtkMagicButtonAction() //A debuff on the enemy
    {
        GameManager.playerPrompt = 8;
        GameManager.timelineActive = true;
        GameManager.promptOver = false;
        promptSet.PlayFromTimeLines(3);//Lets see should change the var in other method...resetPrompt GM
                                       //Call iterator
        StartCoroutine(WaitforPromptOver());
    }
    

    public void TrueToonAtkMagicAction()
    {
        float defenseAmount = 0;
        Attributes defense;

        if (GameObject.FindWithTag("Enemy"))
        {
            defense = GameObject.FindWithTag("Enemy").GetComponent<Attributes>();

            defenseAmount = defense.getDefense();
            // Debug.Log("Before debuff"+ defenseAmount);




            //  Debug.Log("After debuff"+ defenseAmount);


            // defenseAmount = defense.getDefense();

            int temp = Random.Range(1, 11);//range one through ten

            if (temp <= 6) //60% chance to work
            {
                
                defenseAmount = defense.changeDefense(1.5f); ; //reduce the target defense by half (I know)

            }
            else
            {
                //play smoke

                magicFailP.PlayFromTimeLines(2);// at end calls game manager function to return UI
                Debug.Log("Failed cast");
            }





            //set the anim back to idle, first need to turn attackFlag to false

            // StartCoroutine(Buffer());
            // StartCoroutine(Example());

            // Debug.Log(attackFlag);

        }
        GameManager.timelineActive = false;//put in every TRUE method
    }
    public void ToonDefendButtonAction()
    {
        GameManager.playerPrompt = 1;
        GameManager.timelineActive = true;
        GameManager.promptOver = false;
        promptSet.PlayFromTimeLines(3);//Lets see should change the var in other method...resetPrompt GM
                                       //Call iterator
        StartCoroutine(WaitforPromptOver());
    }
        //Defense actions
        public void TrueToonDefendButtonAction()
    {
        
        float defenseAmount = 0;
        Attributes defense;

        if (GameObject.FindWithTag("Player"))
        {
            defense = GameObject.FindWithTag("Player").GetComponent<Attributes>();

            defenseAmount = defense.getDefense();
            if(physicalDefBuff == false)
            {
                defenseAmount = defense.changeDefense(.5f); ; //increase the target defense by half (I know)
            }
            
                
                physicalDefBuff = true;
        }
        GameManager.timelineActive = false;//put in every TRUE method
    }

    public void ToonCounterButtonAction()
    {
        GameManager.playerPrompt = 2;
        GameManager.timelineActive = true;
        GameManager.promptOver = false;
        promptSet.PlayFromTimeLines(3);//Lets see should change the var in other method...resetPrompt GM
                                       //Call iterator
        StartCoroutine(WaitforPromptOver());
    }

        public void TrueToonCounterButtonAction()
    {
        GameManager.playerPrompt = 2;
        isCountering = true;
        /*
        Debug.Log(ButtonHandler.strikeFlag + " is the name");  
        if (ButtonHandler.strikeFlag == true) //counter success!
        {
            if (enemyStats = GameObject.FindWithTag("Enemy").GetComponent<Stats>())
            {
                Debug.Log("Counter success");
                enemyStats.ChangeHealth(-99);
                EnemyActions.dmgDone = true;

            }

                

        }
        else
        {
            Debug.Log("Counter fail");
            isCountering = false;
        }
       // Debug.Log("Countering");
       */
        GameManager.timelineActive = false;//put in every TRUE method
    }
    public void ToonGiveUpButtonAction()
    {
        GameManager.playerPrompt = 3;
        GameManager.timelineActive = true;
        GameManager.promptOver = false;
        promptSet.PlayFromTimeLines(3);//Lets see should change the var in other method...resetPrompt GM
                                       //Call iterator
        StartCoroutine(WaitforPromptOver());
    }

        public void TrueToonGiveUpButtonAction()
    {
      
        Debug.Log("I Surrender forreal");

        gameObject.SetActive(false);

        GameManager.timelineActive = false;//put in every TRUE method



    }
    public void ToonDefMagicButtonAction()
    {
        GameManager.playerPrompt = 4;
        GameManager.timelineActive = true;
        GameManager.promptOver = false;
        promptSet.PlayFromTimeLines(3);//Lets see should change the var in other method...resetPrompt GM
                                       //Call iterator
        StartCoroutine(WaitforPromptOver());
    }

        public void TrueToonDefMagicButtonAction()
    {
       
        Debug.Log("Def magic");
        Attributes health;

        int temp = Random.Range(1, 11);//range one through ten

        if (temp <= 6) //60% chance to work
        {
            if (playerStats = GameObject.FindWithTag("Player").GetComponent<Stats>())
            {
                Debug.Log(" Magic yay");
                playerStats.ChangeHealth(100);

            }
        }
        else
        {
            Debug.Log("Failed cast");
        }


        GameManager.timelineActive = false;//put in every TRUE method


    }
    IEnumerator WaitforPromptOver()
    {
       // Debug.Log("HEREEEE");
        while(GameManager.promptOver == false)
        {
            yield return null;
        }
         GameManager.timelineActive = true;
        Debug.Log("GPrompt: "+ GameManager.playerPrompt);
        switch (GameManager.playerPrompt)//For those who dont need a timeline exactly this works but during anim things like atk, strike, counter you need TL
        {
            case 1:
                Debug.Log("To do: " + "defend");
                TrueToonDefendButtonAction();
                break;//repeat
            case 2:
                Debug.Log("To do: " + "counter");
                TrueToonCounterButtonAction();
                break;//repeat
            case 3:
                Debug.Log("To do: " + "giveUp");
                TrueToonGiveUpButtonAction();
                break;//repeat
            case 4:
                Debug.Log("To do: " + "defMagic");
                TrueToonDefMagicButtonAction();
                break;//repeat
            case 5: //attack number
               // attackP.PlayFromTimeLines(0); //play the anim of knight atk(put this in method does it matter?) Yes for it needs to happen during sword strike.
                break;//repeat
            case 6:
                Debug.Log("Doing: " + "strike");
                TrueToonStrikeButtonAction();
                break;//repeat
            case 7:
                Debug.Log("To do: " + "charge");
                TrueToonChargeButtonAction();
                break;//repeat
            case 8:
                Debug.Log("To do: " + "atkMag");
                TrueToonAtkMagicAction();
                break;//repeat
            
                
    }
         



    }

    IEnumerator Buffer()
    {

        yield return new WaitForSeconds(1f);

    }

    IEnumerator Example()
    {

        yield return new WaitForSeconds(1f);
        ButtonHandler.attackFlag = false;
        // Debug.Log(attackFlag);
        yield return ButtonHandler.attackFlag;

    }
}

