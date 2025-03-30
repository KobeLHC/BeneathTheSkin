using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalGate : MonoBehaviour
{
    public GameObject interactPrompt;
    private bool playerIsNear = false;

    //Sprite change for interactable
    private SpriteRenderer spriteRenderer;
    public Sprite basicSprite;
    public Sprite interactSprite;

    //Broken Gate Sprite and variable from EndSceneManager
    public GameObject finalPrompt;
    public EndSceneController end;
    public Sprite brokenGate;

    //Sound effect control
    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    void Start()
    {
        interactPrompt.SetActive(false);
        finalPrompt.SetActive(false);
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {

        if (end.solved)
        {
            spriteRenderer.sprite = brokenGate;
        }

        if (playerIsNear && Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Interacted");
            audioManager.PlaySFX(audioManager.clicking);

            if (!end.solved)
            {
                interactPrompt.SetActive(false);
                finalPrompt.SetActive(true);
            }

            
            spriteRenderer.sprite = basicSprite;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsNear = true;

            if (!end.solved)
            {
                interactPrompt.SetActive(true);
                spriteRenderer.sprite = interactSprite;
            }
        }


    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsNear = false;
            if (!end.solved)
            {
                interactPrompt.SetActive(false);
                finalPrompt.SetActive(false);
                spriteRenderer.sprite = basicSprite;
            }
        }
    }
}
