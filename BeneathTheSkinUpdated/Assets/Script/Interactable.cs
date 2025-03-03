using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public GameObject interactPrompt;
    private bool playerIsNear = false;
    // Start is called before the first frame update
    void Start()
    {
        interactPrompt.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(playerIsNear && Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Interacted");
            interactPrompt.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            playerIsNear = true;
            interactPrompt.SetActive(true);
        }


    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsNear = false;
            interactPrompt.SetActive(false);
        }
    }
}
