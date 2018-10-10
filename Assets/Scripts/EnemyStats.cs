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

    public void ChangeHealth(int amount)
    {

        Vector3 fwd = transform.TransformDirection(Vector3.forward);
        Debug.DrawRay(transform.position, fwd * 50, Color.yellow);
        //Vector3 startPos = new Vector3(-1.46f, 3f, -3.14f);
        // RaycastHit hit;
        // int layerMask = 1 << 8;
        // layerMask = ~layerMask;
        // Debug.DrawRay(startPos, Vector3.forward * 20, Color.green, 50000);

        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        healthFill.value = currentHealth / maxHealth;
    }
    private void PositionHealthBar()
    {
        Vector3 currentPos = transform.position;
        healthBar.position = new Vector3(currentPos.x, currentPos.y + healthBarOffset, currentPos.z);

        //healthBar.LookAt(Camera.main.transform);
    }
}
