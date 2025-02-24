using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelCode : MonoBehaviour
{
    public int winThreshold;
    public int winProgress = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (winProgress >= winThreshold)
        {
            SceneManager.LoadScene("LabLevel");
        }
    }

    public void WinProgUp()
    {
        winProgress += 1;
    }

}
