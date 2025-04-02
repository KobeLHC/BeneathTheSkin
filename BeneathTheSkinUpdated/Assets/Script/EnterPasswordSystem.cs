using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnterPasswordSystem : MonoBehaviour
{
    public GameObject passwordPanel; // The password UI panel
    public TMP_InputField passwordInput; // The input field
    public Button submitButton; // The submit button
    public Button exitButton; // exit button
    public GameObject player; // The Player object
    public ScreenshotDisplay screenshotDisplay; // Reference to ScreenshotDisplay script

    public int correctPasswordInt;
    public string correctPassword; // Set your correct password
    public bool playerInZone = false; // Track if the player is in the zone
    public bool isPasswordUIOpen = false; // Track if the UI is open

    //End game control
    public bool finishedPuzzle;

    //promp control
    public GameObject warningPrompt;
    public PlayerController playerObject;
    public CardSwipePopUp cardClose;
    public ChemicalMixPopUp chemClose;
    public GameObject wrongPasswordPrompt;

    //Door control
    public GameObject door;

    void Start()
    {
        correctPasswordInt = Random.Range(1000, 9999);
        correctPassword = correctPasswordInt.ToString();
        passwordPanel.SetActive(false); // Hide the password UI initially
        submitButton.onClick.AddListener(CheckPassword); // Attach function to button
        exitButton.onClick.AddListener(exitPanel);
        finishedPuzzle = false;
        warningPrompt.SetActive(false);
        playerObject = player.GetComponent<PlayerController>();
        wrongPasswordPrompt.SetActive(false);
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
            playerObject.changeRunSpeed(0.0f);
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
            playerObject.changeRunSpeed(6.0f);
            wrongPasswordPrompt.SetActive(false);
            Destroy(door);
            screenshotDisplay.ShowScreenshot(); // Show screenshot after correct password
        }
        else
        {
            wrongPasswordPrompt.SetActive(true);
            Debug.Log("Wrong password! Try again.");
        }
    }

    public void exitPanel()
    {
        passwordPanel.SetActive(false);
        isPasswordUIOpen = false;
        Debug.Log("Exit Panel!");
        wrongPasswordPrompt.SetActive(false);
        playerObject.changeRunSpeed(6.0f);
    }

    public bool getCondition()
    {
        return finishedPuzzle;
    }
}
