using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStats : MonoBehaviour {

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

        Vector3 fwd = transform.TransformDirection(Vector3.forward);
        
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        healthFill.value = currentHealth; //Displays health remaining
    }
    private void PositionHealthBar()
    {
        Vector3 currentPos = transform.position;
        healthBar.position = new Vector3(currentPos.x, currentPos.y + healthBarOffset, currentPos.z);

        healthBar.LookAt(GameObject.FindWithTag("MainCamera").transform);
    }
}
