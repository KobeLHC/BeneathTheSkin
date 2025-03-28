using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChemicalMixPopUp : MonoBehaviour
{
    // later add player sprite conditon
    public PlayerController other;

    //In use
    public GameObject chemicalScene;
    public bool playerIsClose;
    public GameObject susMeter;
    public GameObject warningPrompt;
    public CardSwipePopUp cardClose;
    public EnterPasswordSystem passClose;

    void Start()
    {
        chemicalScene.SetActive(false);
        warningPrompt.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerIsClose && !cardClose.playerIsClose && !passClose.playerInZone)
        {
            warningPrompt.SetActive(false);
        }

        // only jerry and sci2 sprite can turn on chemical mixing puzzle
        if (Input.GetKeyDown(KeyCode.Space) && playerIsClose && gameObject != null && other.changedSci2Sprite == true || Input.GetKeyDown(KeyCode.Space) && playerIsClose && gameObject != null && other.GetAnimator().GetBool("changeSci") == true)
        {
            warningPrompt.SetActive(false);
            chemicalScene.SetActive(true);
            susMeter.SetActive(false);
            
        }

        if(Input.GetKeyDown(KeyCode.Space) && playerIsClose && gameObject != null && other.changedSci2Sprite == false && other.GetAnimator().GetBool("changeSci") == false)
        {
            warningPrompt.SetActive(true);
        }

        if (chemicalScene.active)
        {
            other.changeRunSpeed(0.0f);
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = false;
        }
    }
}
