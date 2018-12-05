using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyActions : MonoBehaviour {

    Stats enemyStats;
    Stats playerStats;

    int turnCheck;
    public bool enemyPhysicalAtkBuff, enemyPhysicalDefBuff;

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
            if (enemy.name == "Skeleton"){
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

            }

        }

       // foreach (Transform Choice in transform)
       // {
            // Debug.Log("DELETE");
         //   Choice.gameObject.SetActive(false);
         //   ButtonHandler.selection = 0;

       // }

    }


    public void SkeletonStrikeButtonAction()
    {
        bool dmgDone=false;
        
        enemyStats = GameObject.FindWithTag("Enemy").GetComponent<Stats>();
        playerStats = GameObject.FindWithTag("Player").GetComponent<Stats>();

        Debug.Log("Skeleton STRIKE");
        if (GameObject.FindWithTag("Player"))
        {
            if (PlayerActions.isCountering ==true)  //if the enemy chooses to counter when the player strikes
            {
               
                enemyStats.ChangeHealth(-99);
                dmgDone = true;
                Debug.Log("GET FUCKED");
               
                
            }
            else {
              //  Debug.Log("MESGSHD" + ButtonHandler.counterFlag);
                    {
                    if (enemyPhysicalAtkBuff == true && dmgDone ==false)
                    {
                        playerStats.ChangeHealth(-160);
                        Debug.Log("Double Strike");
                        SkeletonRevertAttack();
                        enemyPhysicalAtkBuff = false;

                    }
                    else 
                    {
                        if(dmgDone == false)
                        {
                            playerStats.ChangeHealth(-40);
                            Debug.Log("Regular Strike");
                        }
                      
                    }
                }

            }
            ButtonHandler.counterFlag = false;
            PlayerActions.isCountering = false;

        }

        
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
        Debug.Log("Skeleton Charge");
        Attributes attack;
        attack = GameObject.FindWithTag("Enemy").GetComponent<Attributes>();
        Debug.Log("before charge:" + attack);

        attack.changeAttack(2);
        Debug.Log("after charge:" + attack);

        enemyPhysicalAtkBuff = true;
    }
    
    public void SkeletonAtkMagicButtonAction()
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
            }
            else
            {
                Debug.Log("Failed cast");
            }


        }
    }

    //Defense actions
    public void SkeletonDefendButtonAction()
    {
        Debug.Log("Defending -enemy");
        float defenseAmount = 0;
        Attributes defense;

        if (GameObject.FindWithTag("Enemy"))
        {
            defense = GameObject.FindWithTag("Enemy").GetComponent<Attributes>();

            defenseAmount = defense.getDefense();
            if (enemyPhysicalDefBuff == false)
            {
                defenseAmount = defense.changeDefense(.5f); ; //increase the target defense by half (I know)
            }


            enemyPhysicalDefBuff = true;
        }
    }

    public void SkeletonCounterButtonAction()
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
    }
    public void SkeletonGiveUpButtonAction()
    {
        Debug.Log("Surrender forreal-enemy");
        gameObject.SetActive(false);
    }
    public void SkeletonDefMagicButtonAction()
    {
        //Debug.Log("Def magic-enemy");
        
        Attributes health;

        int temp = Random.Range(1, 11);//range one through ten

        if (temp <= 6) //60% chance to work
        {
            if (enemyStats = GameObject.FindWithTag("Enemy").GetComponent<Stats>())
            {
                Debug.Log(" Magic heal enemy");
                enemyStats.ChangeHealth(100);

            }
        }
        else
        {
            Debug.Log("Failed cast");
        }
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
}

