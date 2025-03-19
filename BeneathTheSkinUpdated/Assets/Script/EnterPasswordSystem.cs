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

    void Start()
    {
        passwordPanel.SetActive(false); // Hide the password UI initially
        submitButton.onClick.AddListener(CheckPassword); // Attach function to button
        finishedPuzzle = false;
    }

    void Update()
    {
        if (playerInZone && !isPasswordUIOpen && Input.GetKeyDown(KeyCode.Space))
        {
            ShowPasswordUI();
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
