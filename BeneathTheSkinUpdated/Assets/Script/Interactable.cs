using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public GameObject interactPrompt;
    private bool playerIsNear = false;

    //Sprite change for interactable
    private SpriteRenderer spriteRenderer;
    public Sprite basicSprite;
    public Sprite interactSprite;

    //Sound effect control
    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    void Start()
    {
        interactPrompt.SetActive(false);
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if(playerIsNear && Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Interacted");
            audioManager.PlaySFX(audioManager.clicking);
            interactPrompt.SetActive(false);
            spriteRenderer.sprite = basicSprite;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            playerIsNear = true;
            interactPrompt.SetActive(true);
            spriteRenderer.sprite = interactSprite;
        }


    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsNear = false;
            interactPrompt.SetActive(false);
            spriteRenderer.sprite = basicSprite;
        }
    }
}
