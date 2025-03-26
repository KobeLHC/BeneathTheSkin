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

    //End game control
    public bool finishedPuzzle;

    public GameObject susMeter;

    // Start is called before the first frame update
    void Start()
    {
        finishedPuzzle = false;
    }

    // Update is called once per frame
    void Update()
    {
        //For Chemical Mix
        if (winProgress >= winThreshold)
        {
            chemicalScene.SetActive(false);
            other.changeRunSpeed(6.0f);
            finishedPuzzle = true;
            susMeter.SetActive(true);
            Destroy(destroyObjectChem);
        }
    }

    //For Chemical Mix
    public void WinProgUp()
    {
        winProgress += 1;
    }

    public bool getCondition()
    {
        return finishedPuzzle;
    }

}
