using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Array2DEditor;

public class PopUpTextB : MonoBehaviour
{
    // Reference to player class
    public PlayerController other;



    // Panel and Text game objects
    public GameObject TextPanel;
    public GameObject GameObjGroup;
    public TMP_Text displayText;

    // Holds all the text for an NPC
    public Array2DString textLines;

    // Holds all the sus Values for an NPC's dialogue options
    public Array2DInt susValues;

    public Array2DInt buttonIndexing;

    // Used to iterate through the above arrays
    public int index;

    public int sus;

    // Button variables
    public GameObject ButtonA;
    public Button AButton;
    public TMP_Text ButtonAText;

    public GameObject ButtonB;
    public Button BButton;
    public TMP_Text ButtonBText;

    public GameObject ButtonC;
    public Button CButton;
    public TMP_Text ButtonCText;

    public float wordSpeed = 0.05f;
    public bool playerIsClose;
    public bool isTriggered = false;
    public int textOptions;
    public int textLinesNum = 0;
    public bool textIsDone;
    //public int randNum = 0;
    //public int randMax = 8;

    void Start()
    {
        AButton.onClick.AddListener(NextLineA); // Attach function to button
        BButton.onClick.AddListener(NextLineB); // Attach function to button
        CButton.onClick.AddListener(NextLineC); // Attach function to button
    }

    // Update is called once per frame
    void Update()
    {


        // Checks if the player interacted with NPC (checks for player input and if close enough)
        if (playerIsClose && !isTriggered)
        {
            other.changeRunSpeed(0.0f);
            ButtonA.SetActive(false);
            ButtonB.SetActive(false);
            ButtonC.SetActive(false);

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

        if (index < textLinesNum)
        {
            // Checks if message is complete
            if (displayText.text.Length == textLines.GetCell(0, index).Substring(1).Length)
            {

                if (textOptions == 1)
                {
                    ButtonAText.text = textLines.GetCell(1, index);
                    ButtonA.SetActive(true);
                }
                else if (textOptions == 2)
                {
                    ButtonAText.text = textLines.GetCell(1, index);
                    ButtonA.SetActive(true);
                    ButtonBText.text = textLines.GetCell(2, index);
                    ButtonB.SetActive(true);
                }
                else if (textOptions == 3)
                {
                    ButtonAText.text = textLines.GetCell(1, index);
                    ButtonA.SetActive(true);
                    ButtonBText.text = textLines.GetCell(2, index);
                    ButtonB.SetActive(true);
                    ButtonCText.text = textLines.GetCell(3, index);
                    ButtonC.SetActive(true);
                }

            }
        }
    }

    // Method to close text panel
    public void zeroText()
    {
        displayText.text = "";
        index = 0;
        TextPanel.SetActive(false);
        //displayText.enabled = false;
        other.changeRunSpeed(6.0f);
    }

    // Below is how each letter is typed one at a time. Is also how the code checks whether the text should be branching or not.
    IEnumerator Typing()
    {
        for (int i = 0; i < textLines.GetCell(0, index).Length; i++)
        {
            if (textLines.GetCell(0, index)[i] == '@')
            {
                textOptions = 1;
            }
            else if (textLines.GetCell(0, index)[i] == '#')
            {
                textOptions = 2;
            }
            else if (textLines.GetCell(0, index)[i] == '^')
            {
                textOptions = 3;
            }
            else
            {
                displayText.text += textLines.GetCell(0, index)[i];
                yield return new WaitForSeconds(wordSpeed);

                //fail letter replacement system, might revisit if we need to add more to game
                /*
                if (textLines.GetCell(0, index)[i].Equals(" ") || textLines.GetCell(0, index)[i - 1].Equals(" "))
                {
                    displayText.text += textLines.GetCell(0, index)[i];
                    yield return new WaitForSeconds(wordSpeed);
                }
                else
                {
                    randNum = UnityEngine.Random.Range(0, randMax);

                    if (randNum == 1)
                    {
                        randMax = 8;
                        displayText.text += '*';
                        yield return new WaitForSeconds(wordSpeed);
                    }
                    else
                    {
                        randMax--;
                        displayText.text += textLines.GetCell(0, index)[i];
                        yield return new WaitForSeconds(wordSpeed);
                    }
                }
                */
            }
        }
    }

    // Name is slightly confusing, the following methods are used to go to the next line of dialogue.
    public void NextLineA()
    {
        ButtonA.SetActive(false);
        ButtonB.SetActive(false);
        ButtonC.SetActive(false);
        sus = susValues.GetCell(0, index);
        other.moreSus(sus);
        index = buttonIndexing.GetCell(0, index);
        Debug.Log(index);

        if (index < textLinesNum)
        {

            displayText.text = "";
            StartCoroutine(Typing());
        }
        else
        {
            zeroText();
        }
    }
    public void NextLineB()
    {
        ButtonA.SetActive(false);
        ButtonB.SetActive(false);
        ButtonC.SetActive(false);
        sus = susValues.GetCell(1, index);
        other.moreSus(sus);
        index = buttonIndexing.GetCell(1, index);
        Debug.Log(index);

        if (index < textLinesNum)
        {

            displayText.text = "";
            StartCoroutine(Typing());
        }
        else
        {
            zeroText();
        }
    }
    public void NextLineC()
    {
        ButtonA.SetActive(false);
        ButtonB.SetActive(false);
        ButtonC.SetActive(false);
        sus = susValues.GetCell(2, index);
        other.moreSus(sus);
        index = buttonIndexing.GetCell(2, index);
        Debug.Log(index);

        if (index < textLinesNum)
        {

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
}
