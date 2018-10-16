using UnityEngine;
using UnityEngine.UI;

public class ButtonHandler : MonoBehaviour
{
   // public GameObject currentHitObject;
   // private float currentHitDistance;
    
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

   // public float sphereRadius = 10f;
   // public float maxDistance=  5f;
   // public Vector3 origin;
   //     private Vector3 dir;
    
    RaycastHit hit;
    int layerMask = 1 << 20;
    

    void Start()
    {

       

        // Instantiate buttonList to hold the amount of buttons we are using.
        buttonList = new MyButton[4];
        // Set up the first button, finding the game object based off its name. We also 
        // must set the expected onClick method, and should trigger the selected colour.
        buttonList[0].image = GameObject.Find("Attack").GetComponent<Image>();
        buttonList[0].image.color = Color.yellow;
        buttonList[0].action = AttackButtonAction;
        // Do the same for the second button. We are also ensuring the image colour is
        // set to our normalColor, to ensure uniformity.
        buttonList[1].image = GameObject.Find("Defend").GetComponent<Image>();
        buttonList[1].image.color = Color.white;
        buttonList[1].action = DefendButtonAction;
        // Do the same for the second button. We are also ensuring the image colour is
        // set to our normalColor, to ensure uniformity.
        buttonList[2].image = GameObject.Find("Strike").GetComponent<Image>();
        buttonList[2].image.color = Color.white;
        buttonList[2].action = StrikeButtonAction;
        // Do the same for the second button. We are also ensuring the image colour is
        // set to our normalColor, to ensure uniformity.
        buttonList[3].image = GameObject.Find("Counter").GetComponent<Image>();
        buttonList[3].image.color = Color.white;
        buttonList[3].action = CounterButtonAction;
    }


    void Update()
    {
        
        // Raycast thing = (myVector, enemyVector);
        //  Debug.DrawLine(myVector, enemyVector, Color.green);
       /* origin = transform.position;
        dir = transform.forward;
        if(Physics.SphereCast(origin, sphereRadius, dir, out hit, maxDistance, layerMask, QueryTriggerInteraction.UseGlobal))
        {
            currentHitObject = hit.transform.gameObject;
            currentHitDistance = hit.distance;

        }
        else
        {
            currentHitDistance = maxDistance;
            currentHitObject = null;
        }
        */

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
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            buttonList[selectedButton].action();
            buttonList[selectedButton].image.color = Color.yellow;
            Debug.Log("PRESSED");
        }
    }


    ///<summary>This is the method that will call when selecting "Attack".</summary>
    void AttackButtonAction()
    {
        int count = 0;
        Debug.Log("Attack");
        if (GameObject.FindWithTag("Enemy"))
        {

            
           // Debug.Log(GameObject.FindWithTag("Enemy"));
            EnemyStats stats;
            if(stats = GameObject.FindWithTag("Enemy").GetComponent<EnemyStats>())
            {
                stats.ChangeHealth(-20);
            }

        } 

            foreach (Transform Choice in transform)
            {
                Choice.gameObject.SetActive(false);
                selection = 0;

            }
        
    }

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
    ///<summary>This is the method that will call when selecting "Strike".</summary>
    void StrikeButtonAction()
    {
        int count = 0;
        Debug.Log("Strike");
       
        if (GameObject.FindWithTag("Enemy"))
        {
            count++;
            Debug.Log(count);
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