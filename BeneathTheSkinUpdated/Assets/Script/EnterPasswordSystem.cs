using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnterPasswordSystem : MonoBehaviour
{
    public GameObject passwordPanel; // The password UI panel
    public TMP_InputField passwordInput; // The input field
    public Button submitButton; // The submit button
    public GameObject player; // The Player object
    public ScreenshotDisplay screenshotDisplay; // Reference to ScreenshotDisplay script

    public string correctPassword = "1234"; // Set your correct password
    public bool playerInZone = false; // Track if the player is in the zone
    public bool isPasswordUIOpen = false; // Track if the UI is open

    //End game control
    public bool finishedPuzzle;

    //promp control
    public GameObject warningPrompt;
    public PlayerController playerObject;
    public CardSwipePopUp cardClose;
    public ChemicalMixPopUp chemClose;

    //Door control
    public GameObject door;

    void Start()
    {
        passwordPanel.SetActive(false); // Hide the password UI initially
        submitButton.onClick.AddListener(CheckPassword); // Attach function to button
        finishedPuzzle = false;
        warningPrompt.SetActive(false);
        playerObject = player.GetComponent<PlayerController>();
    }

    void Update()
    {
        if (!playerInZone && !cardClose.playerIsClose && !chemClose.playerIsClose)
        {
            warningPrompt.SetActive(false);
        }

        if (playerInZone && !isPasswordUIOpen && Input.GetKeyDown(KeyCode.Space) && playerObject.changedSecSprite == true)
        {
            warningPrompt.SetActive(false);
            ShowPasswordUI();
        }

        if(playerInZone && !isPasswordUIOpen && Input.GetKeyDown(KeyCode.Space) && playerObject.changedSecSprite == false)
        {
            warningPrompt.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInZone = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInZone = false;

            if (finishedPuzzle)
            {
                Destroy(gameObject);
            }
        }
    }

    void ShowPasswordUI()
    {
        passwordPanel.SetActive(true);
        isPasswordUIOpen = true;
        Debug.Log("Password UI Opened");
    }

    void CheckPassword()
    {
        if (passwordInput.text == correctPassword)
        {
            finishedPuzzle = true;
            passwordPanel.SetActive(false);
            isPasswordUIOpen = false;
            Debug.Log("Correct password entered!");
            Destroy(door);
            screenshotDisplay.ShowScreenshot(); // Show screenshot after correct password
        }
        else
        {
            Debug.Log("Wrong password! Try again.");
        }
    }

    public bool getCondition()
    {
        return finishedPuzzle;
    }
}
