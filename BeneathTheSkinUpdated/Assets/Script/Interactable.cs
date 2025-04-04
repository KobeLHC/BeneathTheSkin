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

    //Special Object
    public GameObject board;

    //hint prompt
    public GameObject hintPrompt;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    void Start()
    {
        interactPrompt.SetActive(false);
        spriteRenderer = GetComponent<SpriteRenderer>();

        if(board == null)
        {
            // Do nothing
        }

        else
        {
            board.SetActive(false);
        }
        
    }

    void Update()
    {
        if(playerIsNear && Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Interacted");
            audioManager.PlaySFX(audioManager.clicking);
            interactPrompt.SetActive(false);
            spriteRenderer.sprite = basicSprite;

            if(gameObject.tag == "Bulletin")
            {
                board.SetActive(true);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            playerIsNear = true;
            interactPrompt.SetActive(true);
            hintPrompt.SetActive(false);
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

            if(gameObject.tag == "Bulletin")
            {
                board.SetActive(false);
            }
        }
    }
}
