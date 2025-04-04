using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScreenshotDisplay : MonoBehaviour
{
    public GameObject screenshotPanel; // Panel to show the screenshot
    public Image screenshotImage; // UI Image component to display the screenshot
    public Button deleteButton; // Button to delete the screenshot
    public GameObject hintPrompt;
    public float activate;
    public float deactivate;

    void Start()
    {
        screenshotPanel.SetActive(false); // Hide screenshot panel initially
        deleteButton.onClick.AddListener(DeleteScreenshot); // Attach function to delete button
        hintPrompt.SetActive(false);
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
        StartCoroutine(HintingPrompt());
    }

    IEnumerator HintingPrompt()
    {
        yield return new WaitForSeconds(activate);

        hintPrompt.SetActive(true);
        Debug.Log("Target Object Activated");

        yield return new WaitForSeconds(deactivate);

        hintPrompt.SetActive(false);
        Debug.Log("Target Object Deactivated");
    }
}
