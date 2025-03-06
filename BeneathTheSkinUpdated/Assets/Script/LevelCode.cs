using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelCode : MonoBehaviour
{
    public int winThreshold;
    public int winProgress = 0;
    public GameObject chemicalScene;
    public GameObject destroyObjectChem;
    public PlayerController other;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //For Chemical Mix
        if (winProgress >= winThreshold)
        {
            chemicalScene.SetActive(false);
            other.changeRunSpeed(6.0f);
            //Time.timeScale = 1;
            Destroy(destroyObjectChem);
        }
    }

    //For Chemical Mix
    public void WinProgUp()
    {
        winProgress += 1;
    }

}
