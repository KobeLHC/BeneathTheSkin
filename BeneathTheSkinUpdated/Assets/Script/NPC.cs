using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Array2DEditor;

public class NPC : MonoBehaviour
{
    // Reference to player class
    public PlayerController other;

    // Panel and Text game objects
    public GameObject TextPanel;
    public TMP_Text displayText;

    // Holds all the text for an NPC
    public Array2DString textLines;

    // Holds all the sus Values for an NPC's dialogue options
    public Array2DInt susValues;

    public Array2DInt buttonIndexing;

    // Used to iterate through the above arrays
    private int index;

    public int sus;

    // Button variables
    public GameObject ButtonA;
    public TMP_Text ButtonAText;
    public GameObject ButtonB;
    public TMP_Text ButtonBText;
    public GameObject ButtonC;
    public TMP_Text ButtonCText;

    public float wordSpeed = 0.05f;
    public bool playerIsClose;
    public int textOptions;
    public int textLinesNum;
    public bool textIsDone;
    public int randNum = 0;
    public int randMax = 8;

    // Update is called once per frame
    void Update()
    {


        // Checks if the player interacted with NPC (checks for player input and if close enough)
        if (Input.GetKeyDown(KeyCode.Space) && playerIsClose)
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
                    ButtonAText.text = textLines.GetCell(1, index).Substring(textLines.GetCell(1, index).IndexOf('|') + 1);
                    ButtonA.SetActive(true);
                }
                else if (textOptions == 2)
                {
                    ButtonAText.text = textLines.GetCell(1, index).Substring(textLines.GetCell(1, index).IndexOf('|') + 1);
                    ButtonA.SetActive(true);
                    ButtonBText.text = textLines.GetCell(2, index).Substring(textLines.GetCell(2, index).IndexOf('|') + 1);
                    ButtonB.SetActive(true);
                }
                else if (textOptions == 3)
                {
                    ButtonAText.text = textLines.GetCell(1, index).Substring(textLines.GetCell(1, index).IndexOf('|') + 1);
                    ButtonA.SetActive(true);
                    ButtonBText.text = textLines.GetCell(2, index).Substring(textLines.GetCell(2, index).IndexOf('|') + 1);
                    ButtonB.SetActive(true);
                    ButtonCText.text = textLines.GetCell(3, index).Substring(textLines.GetCell(3, index).IndexOf('|') + 1);
                    ButtonC.SetActive(true);
                }

            }
        }
    }

    // Method to close text panel
    public void zeroText()
    {
        Debug.Log("" + index + " " + buttonIndexing.GetCell(0, 0) + " " + buttonIndexing.GetCell(1, 0) + " " + buttonIndexing.GetCell(2, 0));
        Debug.Log(textLinesNum);
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

        if (index < textLinesNum)
        {

            displayText.text = "";
            StartCoroutine(Typing());
        }
        else
        {
            Debug.Log(index < textLinesNum);
            Debug.Log(textLinesNum);
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


        if (index < textLinesNum)
        {

            displayText.text = "";
            StartCoroutine(Typing());
        }
        else
        {
            Debug.Log(index < textLinesNum);
            Debug.Log(textLinesNum);
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

        if (index < textLinesNum)
        {
            displayText.text = "";
            StartCoroutine(Typing());
        }
        else
        {
            Debug.Log(index < textLinesNum);
            Debug.Log(textLinesNum);
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

