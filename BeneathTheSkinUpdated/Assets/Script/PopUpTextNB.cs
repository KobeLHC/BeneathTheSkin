using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PopUpTextNB : MonoBehaviour
{
    public PlayerController other;

    // Panel and Text game objects
    public GameObject TextPanel;
    public TMP_Text displayText;

    // Holds all the text
    public string[] dialogue;

    // Used to iterate through the above array
    private int index;

    // Button variables
    public GameObject ButtonA;
    public TMP_Text ButtonAText;

    public float wordSpeed = 0.05f;
    public bool playerIsClose = false;
    public bool isTriggered = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerIsClose && !isTriggered)
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

    // Checks if player is within detection range
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = true;
        }
    }
}
