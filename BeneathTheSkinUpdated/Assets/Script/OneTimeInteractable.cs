using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneTimeInteractable : MonoBehaviour
{
    public GameObject oneTimeInteractPrompt;
    private bool playerIsNear = false;
    //private bool oneTimeUse = true;
    public GameObject destroyedObject;
    public bool tpDone;

    //Sound Effect
    AudioManager audioManager;

    //Sprite change for interactable
    private SpriteRenderer spriteRenderer;
    public Sprite basicSprite;
    public Sprite interactSprite;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    void Start()
    {
        oneTimeInteractPrompt.SetActive(false);
        spriteRenderer = GetComponent<SpriteRenderer>();
        tpDone = false;
    }

    void Update()
    {
        if (playerIsNear && Input.GetKeyDown(KeyCode.Space) && destroyedObject != null)
        {
            if(destroyedObject.tag == "tp")
            {
                tpDone = true;
                Destroy(destroyedObject);
            }

            Debug.Log("Interacted");
            spriteRenderer.sprite = basicSprite; 
            audioManager.PlaySFX(audioManager.clicking);
            oneTimeInteractPrompt.SetActive(false);

        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (destroyedObject == null)
            {
                oneTimeInteractPrompt.SetActive(false);
            }

            else
            {
                playerIsNear = true;
                oneTimeInteractPrompt.SetActive(true);
                spriteRenderer.sprite = interactSprite;

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
                spriteRenderer.sprite = basicSprite;

            }
        }
    }
}
