using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stats : MonoBehaviour {

    public Transform healthBar;
    public Slider healthFill;
    public float currentHealth;
    public float maxHealth;
    public float healthBarOffset = 2;
	
	
	// Update is called once per frame
	void Update () {
        PositionHealthBar();
     
    }
    public float getHealth()
    {
        return currentHealth;
    }

    public void ChangeHealth(float amount)
    {
      
        Attributes  defense = GameObject.FindWithTag("Player").GetComponent<Attributes>();
        PlayerActions var = GameObject.FindWithTag("Player").GetComponent<PlayerActions>();

        Vector3 fwd = transform.TransformDirection(Vector3.forward);
        
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        healthFill.value = currentHealth; //Displays health remaining
        //Buff handling
      
        if (var.physicalDefBuff ==true)
        {
            defense.changeDefense(2f);
            var.physicalDefBuff = false;
            
          
        }
     

    }
    private void PositionHealthBar()
    {
        Vector3 currentPos = transform.position;
        healthBar.position = new Vector3(currentPos.x, currentPos.y + healthBarOffset, currentPos.z);
        
        //trying to rotate the health bar 180 degress but the above line is overwriting it!
        //healthBar.transform.eulerAngles = new Vector3(healthBar.transform.eulerAngles.x, healthBar.transform.eulerAngles.y + 180, healthBar.transform.eulerAngles.z);



        //healthBar.LookAt(GameObject.FindWithTag("MainCamera").transform);
    }
}
