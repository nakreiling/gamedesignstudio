using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimScript : MonoBehaviour {

     Animator m_animator;
    public static bool isAttackingPressed;
    // Use this for initialization
    void Start () {
        m_animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {

        isAttackingPressed = ButtonHandler.attackFlag; //corresponds with the player selecting to attack in the game
        if (isAttackingPressed == true)
        {
            m_animator.SetBool("isAttacking", true); //trigger the attack animation
        }
        else {
            m_animator.SetBool("isAttacking", false); //turn off the attack animation
        }
       
		
	}
}
