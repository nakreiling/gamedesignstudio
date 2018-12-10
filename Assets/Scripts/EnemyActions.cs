using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyActions : MonoBehaviour
{

    Stats enemyStats;
    Stats playerStats;

    [SerializeField] private TimeLineScript promptSet;
    [SerializeField] private TimeLineScript attackE;
    [SerializeField] private TimeLineScript defendE;
    [SerializeField] private TimeLineScript atkMagE;
    [SerializeField] private TimeLineScript chargeE;
    [SerializeField] private TimeLineScript magicFailE;
    [SerializeField] private TimeLineScript DefE;
    [SerializeField] private TimeLineScript HealE;
    [SerializeField] private TimeLineScript DieE;
    [SerializeField] private TimeLineScript atkNoTrigger;
    [SerializeField] private TimeLineScript atkNoTriggerE;

    int turnCheck;
    public bool enemyPhysicalAtkBuff=false;
    public static bool enemyPhysicalDefBuff=false;

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

    //Var
    public static bool dmgDone;

    private void Start()
    {



        if (GameObject.FindWithTag("Enemy"))
        {

            GameObject enemy = GameObject.FindWithTag("Enemy");
            Debug.Log(enemy.name);
            //find out which enemy it is...set methods according to spefic model
            if (enemy.name == "Skeleton")
            {
                attackMethod = SkeletonAttackButtonAction; //test in Button Handler by calling the variable
                strikeMethod = SkeletonStrikeButtonAction;
                chargeMethod = SkeletonChargeButtonAction;
                atkMagMethod = SkeletonAtkMagicButtonAction;

                //Defense
                defenseMethod = SkeletonDefendButtonAction;
                counterMethod = SkeletonCounterButtonAction;
                giveUpMethod = SkeletonGiveUpButtonAction;
                defMagMethod = SkeletonDefMagicButtonAction;

            }


        }
    }


    public void SkeletonAttackButtonAction()
    {
        GameManager.enemyPrompt = 13;
        GameManager.timelineActive = true;
        GameManager.promptOver = false;
        promptSet.PlayFromTimeLines(3);//Lets see should change the var in other method...resetPrompt GM
                                       //Call iterator
        StartCoroutine(WaitforPromptOver());
    }

    public void TrueSkeletonAttackButtonAction()
    {
        float attackAmount = 0;
        float defenseAmount = 0;
        float currentHealth = 0;
        Attributes attack;
        Attributes defense;
        Stats health;
        int count = 0;
        Debug.Log("Attack-Skeleton");
        if (GameObject.FindWithTag("Player"))
        {
            attackE.PlayFromTimeLines(1);


            GameObject player = GameObject.FindWithTag("Player");

            if (attack = GameObject.FindWithTag("Enemy").GetComponent<Attributes>()) //if the player exists get that object's atk value
            {

                ButtonHandler.attackFlag = true;//Now the AnimScript should trigger the animation for the attack

                attackAmount = attack.getAttack();
                // Debug.Log("The attack would normally do " + attackAmount + " damage!");
                //Check defense of enemy

                defense = GameObject.FindWithTag("Player").GetComponent<Attributes>();

                defenseAmount = defense.getDefense();
                // Debug.Log("The defense of the enemy is " + defenseAmount);
                //modify the atk amount
                attackAmount = (attackAmount * defenseAmount);

                //finalize amount
                attackAmount = -attackAmount;
                //Debug.Log("It did " + attackAmount + "Damage");


                health = GameObject.FindWithTag("Player").GetComponent<Stats>();

                currentHealth = health.currentHealth;

                //set the anim back to idle, first need to turn attackFlag to false

                ButtonHandler.attackFlag = false;

                StartCoroutine(Buffer());

                // Debug.Log("Tghe thing is sisdfin "+ButtonHandler.attackFlag);


            }


            // Debug.Log(GameObject.FindWithTag("Enemy"));
            Stats stats;
            if (stats = GameObject.FindWithTag("Player").GetComponent<Stats>())
            {

                stats.ChangeHealth(attackAmount);
                
                currentHealth = stats.currentHealth;
                // Debug.Log("Target has " + currentHealth + " health total");
                //Do a check to see if equal to zero and then make object a dead object hehehe
                if (enemyPhysicalAtkBuff == true)
                {
                    SkeletonRevertAttack(); //decrease the power buff
                    enemyPhysicalAtkBuff = false;
                }

            }

        }

        // foreach (Transform Choice in transform)
        // {
        // Debug.Log("DELETE");
        //   Choice.gameObject.SetActive(false);
        //   ButtonHandler.selection = 0;

        // }
        //GameManager.timelineActive = false;//put in every TRUE method
    }

    public void SkeletonStrikeButtonAction()
    {
        GameManager.enemyPrompt = 14;
        GameManager.timelineActive = true;
        GameManager.promptOver = false;
        promptSet.PlayFromTimeLines(3);//Lets see should change the var in other method...resetPrompt GM
                                       //Call iterator
        StartCoroutine(WaitforPromptOver());
    }

        public void TrueSkeletonStrikeButtonAction()//called by event in Timeline
    {
        bool dmgDone = false;
        if (GameObject.FindWithTag("Player"))
        {
            enemyStats = GameObject.FindWithTag("Enemy").GetComponent<Stats>();
        playerStats = GameObject.FindWithTag("Player").GetComponent<Stats>();

        Debug.Log("Skeleton STRIKE");
      
            if (PlayerActions.isCountering == true)  //if the enemy chooses to counter when the player strikes
            {
                atkNoTrigger.PlayFromTimeLines(15);
                enemyStats.ChangeHealth(-99);
                dmgDone = true;
                Debug.Log("GET FUCKED");


            }
            else
            {
                //  Debug.Log("MESGSHD" + ButtonHandler.counterFlag);
                {
                    if (enemyPhysicalAtkBuff == true && dmgDone == false)
                    {
                        atkNoTriggerE.PlayFromTimeLines(16);
                        playerStats.ChangeHealth(-160);
                        Debug.Log("Double Strike");
                        SkeletonRevertAttack();
                        enemyPhysicalAtkBuff = false;
                       // GameManager.timelineActive = false;//put in every TRUE method

                    }
                    else
                    {
                        if (dmgDone == false)
                        {
                            atkNoTriggerE.PlayFromTimeLines(16);
                            playerStats.ChangeHealth(-40);
                            Debug.Log("Regular Strike");
                           // GameManager.timelineActive = false;//put in every TRUE method just being safe
                        }

                    }
                }

            }
            ButtonHandler.counterFlag = false;
            PlayerActions.isCountering = false;

        }
      //  GameManager.timelineActive = false;//put in every TRUE method

        //ButtonHandler.strikeFlag = true;
    }
    public void SkeletonRevertAttack()
    {
        Attributes attack;
        attack = GameObject.FindWithTag("Enemy").GetComponent<Attributes>();
        Debug.Log("before revert:" + attack);

        attack.changeAttack(.5f);
        Debug.Log("after revert:" + attack);


    }
    public void SkeletonRevertDefense()
    {
        
        Attributes defense;
        defense = GameObject.FindWithTag("Enemy").GetComponent<Attributes>();

        Debug.Log("before revert:" + defense);

        defense.changeDefense(2f);
        Debug.Log("after revert:" + defense);
    }

    public void SkeletonChargeButtonAction()
    {
        GameManager.enemyPrompt = 15;
        GameManager.timelineActive = true;
        GameManager.promptOver = false;
        promptSet.PlayFromTimeLines(3);//Lets see should change the var in other method...resetPrompt GM
                                       //Call iterator
        StartCoroutine(WaitforPromptOver());
    }
       

    public void TrueSkeletonChargeButtonAction()
    {
        Debug.Log("Skeleton Charge");
        Attributes attack;
        attack = GameObject.FindWithTag("Enemy").GetComponent<Attributes>();
        if(enemyPhysicalAtkBuff == false)
        {
            Debug.Log("before charge:" + attack);
            chargeE.PlayFromTimeLines(7);
            attack.changeAttack(2);
            Debug.Log("after charge:" + attack);
            enemyPhysicalAtkBuff = true;
        }
        else
        {


        }
     

        
      //  GameManager.timelineActive = false;//put in every TRUE method
    }

    public void SkeletonAtkMagicButtonAction()
    {
        GameManager.enemyPrompt = 16;
        GameManager.timelineActive = true;
        GameManager.promptOver = false;
        promptSet.PlayFromTimeLines(3);//Lets see should change the var in other method...resetPrompt GM
                                       //Call iterator
        StartCoroutine(WaitforPromptOver());
    }

        public void TrueSkeletonAtkMagicButtonAction()
    {
        Debug.Log("Skeleton Atk Mag.");
        float defenseAmount = 0;
        Attributes defense;

        if (GameObject.FindWithTag("Player"))
        {
            defense = GameObject.FindWithTag("Player").GetComponent<Attributes>();

            defenseAmount = defense.getDefense();
            // Debug.Log("Before debuff"+ defenseAmount);




            //  Debug.Log("After debuff"+ defenseAmount);


            // defenseAmount = defense.getDefense();

            int temp = Random.Range(1, 11);//range one through ten

            if (temp <= 6) //60% chance to work
            {
                defenseAmount = defense.changeDefense(1.5f); ; //reduce the target defense by half (I know)
                atkMagE.PlayFromTimeLines(5);//Play successful cast
                                             //  GameManager.timelineActive = false;//put in every TRUE method
            }
            else
            {
                Debug.Log("Failed cast");
                magicFailE.PlayFromTimeLines(9);
               // GameManager.timelineActive = false;//put in every TRUE method
            }


        }
       // GameManager.timelineActive = false;//put in every TRUE method
    }

    //Defense actions
    public void SkeletonDefendButtonAction()
    {
        GameManager.enemyPrompt = 9;
        GameManager.timelineActive = true;
        GameManager.promptOver = false;
        promptSet.PlayFromTimeLines(3);//Lets see should change the var in other method...resetPrompt GM

        StartCoroutine(WaitforPromptOver());//test here


    }
    public void TrueSkeletonDefendButtonAction()
    {

        Debug.Log("Defending -enemy");
        float defenseAmount = 0;
        Attributes defense;

        //if (GameObject.FindWithTag("Enemy"))
        //{
            defense = GameObject.FindWithTag("Enemy").GetComponent<Attributes>();

            defenseAmount = defense.getDefense();
            if (enemyPhysicalDefBuff == false)
            {
                Debug.Log("Defense buff enemy");
                DefE.PlayFromTimeLines(10);
                defenseAmount = defense.changeDefense(.5f); ; //increase the target defense by half (I know)
                enemyPhysicalDefBuff = true;
           // }


            
        }
        //GameManager.timelineActive = false;//put in every TRUE method
    }

    public void SkeletonCounterButtonAction()
    {
        GameManager.enemyPrompt = 10;
        GameManager.timelineActive = true;
        GameManager.promptOver = false;
        promptSet.PlayFromTimeLines(3);//Lets see should change the var in other method...resetPrompt GM
                                       //Call iterator
        StartCoroutine(WaitforPromptOver());
    }

        public void TrueSkeletonCounterButtonAction()
    {

        Debug.Log("Countering-enemy"); //ALL that was needed was the choice in ButtonHandler, fix strike
        /*
        Debug.Log(ButtonHandler.selection + " is the number");  
        if (ButtonHandler.selection == 2) //counter success! the player must choose stirke action which changes the variable
            //earlier I was making skeleton choice change the variable
        {
            
            if (playerStats = GameObject.FindWithTag("Player").GetComponent<Stats>())
            {
                Debug.Log("Counter success");
                playerStats.ChangeHealth(-99);
                ButtonHandler.counterFlag = false;
            }
            
        }
        else
        {
            Debug.Log("Counter fail");
        }
        */
        //GameManager.timelineActive = false;//put in every TRUE method
    }
    public void SkeletonGiveUpButtonAction()
    {
        GameManager.enemyPrompt = 11;
        GameManager.timelineActive = true;
        GameManager.promptOver = false;
        promptSet.PlayFromTimeLines(3);//Lets see should change the var in other method...resetPrompt GM
                                       //Call iterator
        StartCoroutine(WaitforPromptOver());
    }
    public void TrueSkeletonGiveUpButtonAction()
    {
        Debug.Log("Surrender forreal-enemy");
        DieE.PlayFromTimeLines(14);//Lets see should change the var in other method...resetPrompt GM
        gameObject.SetActive(false);
      //  GameManager.timelineActive = false;//put in every TRUE method
    }
    public void SkeletonDefMagicButtonAction()
    {
        GameManager.enemyPrompt = 12;
        GameManager.timelineActive = true;
        GameManager.promptOver = false;
        promptSet.PlayFromTimeLines(3);//Lets see should change the var in other method...resetPrompt GM
                                       //Call iterator
        StartCoroutine(WaitforPromptOver());
    }

    public void TrueSkeletonDefMagicButtonAction()
    {
        //Debug.Log("Def magic-enemy");

        Attributes health;

        int temp = Random.Range(1, 11);//range one through ten

        if (temp <= 6) //60% chance to work
        {
            if (enemyStats = GameObject.FindWithTag("Enemy").GetComponent<Stats>())
            {
                Debug.Log(" Magic heal enemy");
                HealE.PlayFromTimeLines(12);
                enemyStats.ChangeHealth(40);

            }
        }
        else
        {
            Debug.Log("Failed cast");
            magicFailE.PlayFromTimeLines(9);
        }
       // GameManager.timelineActive = false;//put in every TRUE method
    }
    IEnumerator Buffer()
    {

        yield return new WaitForSeconds(1f);

    }

    IEnumerator Example()
    {
        ButtonHandler.attackFlag = false;
        yield return new WaitForSeconds(1f);

        //Debug.Log(attackFlag);
        yield return ButtonHandler.attackFlag;

    }

    IEnumerator WaitforPromptOver()
    {
        // Debug.Log("HEREEEE");
        while (GameManager.promptOver == false)
        {
            yield return null;
        }
        GameManager.timelineActive = true;
        Debug.Log("GPrompt: " + GameManager.enemyPrompt);
        switch (GameManager.enemyPrompt) //For those who dont need a timeline exactly this works but during anim things like atk, strike, counter you need TL
        {
            //Just call true actions here?
            case 9:
                if (GameObject.FindWithTag("Player"))
                {
                Debug.Log("To do: " + "defend");
                    enemyPhysicalDefBuff = true;
                    TrueSkeletonDefendButtonAction();
               
                }
                   
               // GameManager.timelineActive = false;
                break;//repeat
            case 10:
                Debug.Log("Doing: " + "counter");
                TrueSkeletonCounterButtonAction();
               // GameManager.timelineActive = false;
                break;//repeat
            case 11:
                Debug.Log("Doing: " + "giveUp");
                TrueSkeletonGiveUpButtonAction();
              //  GameManager.timelineActive = false;
                break;//repeat
            case 12:
                if (GameObject.FindWithTag("Player"))
                {

                    Debug.Log("Doing: " + "defMagic");
                    TrueSkeletonDefMagicButtonAction();
                }
                    
               // GameManager.timelineActive = false;
                break;//repeat
            case 13: //attack number
                if (GameObject.FindWithTag("Player"))
                {
                    Debug.Log("Doing: " + "defMagic");
                    TrueSkeletonDefMagicButtonAction();
                    Debug.Log("Doing: " + "attack");
                    TrueSkeletonAttackButtonAction();//then inside you call the TimeLine associated with action
                                                     //defendE.PlayFromTimeLines(0); //play the anim of knight atk
                    GameManager.timelineActive = false;
                    ButtonHandler.buttonsMade = false;
                }
                break;//repeat
            case 14:
                if (GameObject.FindWithTag("Player"))
                {
                    Debug.Log("Doing: " + "strike");
                    TrueSkeletonStrikeButtonAction();
                    GameManager.timelineActive = false;

                    ButtonHandler.buttonsMade = false;
                }
                
                break;//repeat
            case 15:
                if (GameObject.FindWithTag("Player"))
                {
                    Debug.Log("Doing: " + "charge");
                    TrueSkeletonChargeButtonAction();
                    GameManager.timelineActive = false;
                    ButtonHandler.buttonsMade = false;
                }

                    

                break;//repeat
            case 16:
                if (GameObject.FindWithTag("Player"))
                {
                    Debug.Log("Doing: " + "atkMag");
                    TrueSkeletonAtkMagicButtonAction();
                    GameManager.timelineActive = false;
                    ButtonHandler.isPlayerTurn = true;
                    ButtonHandler.buttonsMade = false;
                }

                   

                break;//repeat
                
        }

    }

}

