using UnityEngine;
using UnityEngine.UI;

public class ButtonHandler : MonoBehaviour
{
    ///<summary>Placeholder delegate function for our buttonList</summary>
    public delegate void ButtonAction();
    ///<summary>Array of buttons, created from a struct, below.</summary>
    public MyButton[] buttonList;
    ///<summary>Index reference to our currently selected button.</summary>
    public int selectedButton = 0;
    public int prevButton = 0;

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
        }
    }


    ///<summary>This is the method that will call when selecting "Attack".</summary>
    void AttackButtonAction()
    {
        Debug.Log("Attack");
    }

    ///<summary>This is the method that will call when selecting "Defend".</summary>
    void DefendButtonAction()
    {
        Debug.Log("Defend");
    }
    ///<summary>This is the method that will call when selecting "Strike".</summary>
    void StrikeButtonAction()
    {
        Debug.Log("Strike");
    }
    ///<summary>This is the method that will call when selecting "Counter".</summary>
    void CounterButtonAction()
    {
        Debug.Log("Counter");
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