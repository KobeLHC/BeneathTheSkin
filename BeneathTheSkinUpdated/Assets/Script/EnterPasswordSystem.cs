using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnterPasswordSystem : MonoBehaviour
{
    public GameObject passwordPanel; // The password UI panel
    public TMP_InputField passwordInput; // The input field for entering the password
    public Button submitButton; // The submit button
    public GameObject player; // The Player object
    //public MonoBehaviour playerMovementScript; // The Player movement script

    public string correctPassword = "1234"; // Set your correct password
    public bool playerInZone = false; // Track if the player is in the zone
    public bool isPasswordUIOpen = false; // Track if the UI is open

    void Start()
    {
        passwordPanel.SetActive(false); // Hide the password UI initially
        submitButton.onClick.AddListener(CheckPassword); // Attach function to button
    }

    void Update()
    {
        // If player is in the zone and presses Space, open the password UI
        if (playerInZone && !isPasswordUIOpen && Input.GetKeyDown(KeyCode.Space))
        {
            ShowPasswordUI();
        }
    }

    // Checks if player is close enough to interact with NPC
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInZone = true;
        }
    }

    //Checks if player has left the detection range for interaction.
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInZone = false;
        }
    }

    void ShowPasswordUI()
    {
        passwordPanel.SetActive(true); // Show the password UI
        isPasswordUIOpen = true; // Mark UI as open
        //DisablePlayerMovement(); // Stop player movement
        Debug.Log("Open");
    }

    void CheckPassword()
    {
        if (passwordInput.text == correctPassword)
        {
            passwordPanel.SetActive(false); // Hide the UI
            isPasswordUIOpen = false; // Mark UI as closed
            //EnablePlayerMovement(); // Allow player movement again
        }
        else
        {
            Debug.Log("Wrong password! Try again.");
        }
    }

    //void DisablePlayerMovement()
    //{
    //    playerMovementScript.enabled = false;
    //}

    //void EnablePlayerMovement()
    //{
    //    playerMovementScript.enabled = true;
    //}
}
