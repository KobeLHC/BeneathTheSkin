using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public GameObject interactPrompt;
    private bool playerIsNear = false;

    //Sound effect control
    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

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
            audioManager.PlaySFX(audioManager.clicking);
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
