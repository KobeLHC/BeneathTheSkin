using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneTimeInteractable : MonoBehaviour
{
    public GameObject oneTimeInteractPrompt;
    private bool playerIsNear = false;
    private bool oneTimeUse = true;
    public GameObject destroyedObject;

    //Sound Effect
    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    void Start()
    {
        oneTimeInteractPrompt.SetActive(false);
    }

    void Update()
    {
        if (playerIsNear && Input.GetKeyDown(KeyCode.Space) && destroyedObject != null)
        {
            Debug.Log("Interacted");
            audioManager.PlaySFX(audioManager.clicking);
            oneTimeInteractPrompt.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if(destroyedObject == null)
            {
                oneTimeInteractPrompt.SetActive(false);
            }

            else
            {
                playerIsNear = true;
                oneTimeInteractPrompt.SetActive(true);
            }
        }


    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (destroyedObject == null)
            {
                oneTimeInteractPrompt.SetActive(false);
            }
            else
            {
                playerIsNear = false;
                oneTimeInteractPrompt.SetActive(false);
            }
        }
    }
}
