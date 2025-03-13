using UnityEngine;
using UnityEngine.UI;

public class ScreenshotDisplay : MonoBehaviour
{
    public GameObject screenshotPanel; // Panel to show the screenshot
    public Image screenshotImage; // UI Image component to display the screenshot
    public Button deleteButton; // Button to delete the screenshot

    void Start()
    {
        screenshotPanel.SetActive(false); // Hide screenshot panel initially
        deleteButton.onClick.AddListener(DeleteScreenshot); // Attach function to delete button
    }

    public void ShowScreenshot()
    {
        screenshotPanel.SetActive(true); // Show the screenshot panel
        Debug.Log("Screenshot Displayed");
    }

    public void DeleteScreenshot()
    {
        screenshotPanel.SetActive(false); // Hide screenshot panel
        Debug.Log("Screenshot Deleted");
    }
}
