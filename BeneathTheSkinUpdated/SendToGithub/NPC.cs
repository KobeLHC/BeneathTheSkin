using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NPC : MonoBehaviour
{
    // Panel and Text game objects
    public GameObject TextPanel;
    public TMP_Text displayText;

    // Holds all the text for an NPC
    public string[] dialogue;
    public string[] options;

    // Used to iterate through the above arrays
    private int index;
    private int nextTextLine = 1;

    // Button variables
    public GameObject ButtonA;
    public TMP_Text ButtonAText;
    public GameObject ButtonB;
    public TMP_Text ButtonBText;

    public float wordSpeed;
    public bool playerIsClose;
    public bool isBranching;
    // The variable below exists only to change the isBranching boolean. This is because the Coroutine/IEnumerator doesn't like bool for some reason.
    public int changeIsBranching = 0;

    // Update is called once per frame
    void Update()
    {
        // Checks whether isBranching should be true or false
        if (changeIsBranching == 1)
        {
            isBranching = true;
        }
        else
        {
            isBranching = false;
        }

        // Checls if the player interacted with NPC (checks for player input and if close enough)
        if(Input.GetKeyDown(KeyCode.Space) && playerIsClose)
        {
            ButtonA.SetActive(false);
            ButtonB.SetActive(false);

            // Starts typing out the NPC dialogue
            if (TextPanel.activeInHierarchy)
            {
                //zeroText();
            }
            else
            {
                TextPanel.SetActive(true);
                StartCoroutine(Typing());
            }
        }

        // Checks if message is complete
        if(displayText.text == dialogue[index].Substring(1))
        {
            // Checks if current message is branching
            if (!isBranching)
            {
                // If not branching, only displays one button with the corresponding button text.
                ButtonAText.text = options[0];
                ButtonA.SetActive(true);
            }
            else
            {
                // If branching, displays multiple buttons with the corresponding button texts.
                ButtonAText.text = options[index + 1];
                ButtonA.SetActive(true);
                ButtonBText.text = options[index + 2];
                ButtonB.SetActive(true);
            }
        }
    }

    // Method to close text panel
    public void zeroText()
    {
        displayText.text = "";
        index = 0;
        TextPanel.SetActive(false);
    }

    // Below is how each letter is typed one at a time. Is also how the code checks whether the text should be branching or not.
    IEnumerator Typing()
    {
        foreach(char letter in dialogue[index].ToCharArray())
        {
            if (letter == '%')
            {
                isBranching = true;
                changeIsBranching = 1;
            }
            else if (letter == '#')
            {
                changeIsBranching = 0;
            }
            else
            {
                displayText.text += letter;
                yield return new WaitForSeconds(wordSpeed);
            }
        }
    }

    // Name is slightly confusing, the following 2 methods are used to go to the next line of dialogue.
    // This one is for non-branching dialogue.
    public void NextLine()
    {
        ButtonA.SetActive(false);
        ButtonB.SetActive(false);

        if (index < dialogue.Length - 1 && !isBranching)
        {
            index += nextTextLine;
            displayText.text = "";
            StartCoroutine(Typing());
            nextTextLine = 1;
        }
        else if (index < dialogue.Length - 1 && isBranching)
        {
            index += nextTextLine;
            displayText.text = "";
            StartCoroutine(Typing());
            nextTextLine = 2;
        }
        else
        {
            zeroText();
        }
        isBranching = false;
    }

    // This one is for branching dialogue.
    public void NextLineB()
    {
        ButtonA.SetActive(false);
        ButtonB.SetActive(false);

        if (index < dialogue.Length - 1)
        {
            index += 2;
            displayText.text = "";
            StartCoroutine(Typing());
        }
        else
        {
            zeroText();
        }
        isBranching = false;
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

