using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FlavourText : MonoBehaviour
{
    public PlayerController other;

    // Panel and Text game objects
    public GameObject TextPanel;
    public GameObject GameObjGroup;
    public TMP_Text displayText;

    // Holds all the text
    public string[] dialogue;

    // Used to iterate through the above array
    private int index;

    // Button variables
    public GameObject ButtonA;
    //public Button AButton;
    public TMP_Text ButtonAText;

    public float wordSpeed = 0.01f;
    public bool playerIsClose = false;
    public bool isTriggered = false;

    // Start is called before the first frame update
    void Start()
    {
        //AButton.onClick.AddListener(NextLine); // Attach function to button
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && playerIsClose)
        {
            ButtonA.SetActive(false);
            isTriggered = true;
            other.changeRunSpeed(0.0f);

            // Starts typing out the NPC dialogue
            if (TextPanel.activeInHierarchy)
            {
                //zeroText();
            }
            else
            {
                TextPanel.SetActive(true);
                GameObjGroup.SetActive(true);
                displayText.enabled = true;
                StartCoroutine(Typing());
            }
        }

        // Checks if message is complete
        if (displayText.text == dialogue[index])
        {
            ButtonAText.text = "Continue";
            ButtonA.SetActive(true);
        }
    }

    // Method to close text panel
    public void zeroText()
    {
        displayText.text = "";
        index = 0;
        other.changeRunSpeed(6.0f);
        TextPanel.SetActive(false);
        GameObjGroup.SetActive(false);
        //displayText.enabled = false;
    }

    // Below is how each letter is typed one at a time. Is also how the code checks whether the text should be branching or not.
    IEnumerator Typing()
    {
        foreach (char letter in dialogue[index].ToCharArray())
        {
            displayText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
    }

    // Name is slightly confusing, the following 2 methods are used to go to the next line of dialogue.
    // This one is for non-branching dialogue.
    public void NextLine()
    {
        ButtonA.SetActive(false);

        if (index < dialogue.Length - 1)
        {
            index++;
            displayText.text = "";
            StartCoroutine(Typing());
        }
        else
        {
            zeroText();
        }
    }

    // Checks if player is close enough to interact with NPC
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = true;
        }
    }

    //Checks if player has left the detection range for interaction.
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = false;
            zeroText();
        }
    }
}
