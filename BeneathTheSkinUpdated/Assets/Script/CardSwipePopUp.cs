using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSwipePopUp : MonoBehaviour
{
    public PlayerController other;
    public GameObject cardSwipe;
    public bool playerIsClose;
    public GameObject warningPrompt;
    public ChemicalMixPopUp chemClose;
    public EnterPasswordSystem passClose;

    // Start is called before the first frame update
    void Start()
    {
        cardSwipe.SetActive(false);
        warningPrompt.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerIsClose && !chemClose.playerIsClose && !passClose.playerInZone)
        {
            warningPrompt.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Space) && playerIsClose && other.GetAnimator().GetBool("changeSci") == true)
        {
            warningPrompt.SetActive(false);
            cardSwipe.SetActive(true);
        }

        if(Input.GetKeyDown(KeyCode.Space) && playerIsClose && other.GetAnimator().GetBool("changeSci") == false)
        {
            warningPrompt.SetActive(true);
        }
  
        if (cardSwipe.active)
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
