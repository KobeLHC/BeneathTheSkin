using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScene : MonoBehaviour
{
    private void OnEnable()
    {
        SceneManager.LoadScene("TitleScreen", LoadSceneMode.Single);
    }
}
