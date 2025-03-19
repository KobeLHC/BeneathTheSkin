using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndSceneController : MonoBehaviour
{
    //Gameobject of puzzle
    public GameObject cardPuzzle;
    public GameObject passwordPuzzle;
    public GameObject chemicalPuzzle;

    //Getting GameObject Component and condition
    public CardSwipe cardObject;
    public EnterPasswordSystem passwordObject;
    public LevelCode chemicalObject;

    //In script bool to control what scene to load
    public bool cardDone;
    public bool passwordDone;
    public bool chemicalDone;

    void Start()
    {
        //Getting component at start
        cardObject = cardPuzzle.GetComponent<CardSwipe>();
        passwordObject = passwordPuzzle.GetComponent<EnterPasswordSystem>();
        chemicalObject = chemicalPuzzle.GetComponent<LevelCode>();
    }

    void Update()
    {
        //Continously checking for gameobject condition
        cardDone = cardObject.getCondition();
        passwordDone = passwordObject.getCondition();
        chemicalDone = chemicalObject.getCondition();
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(cardDone && passwordDone && chemicalDone)
        {
            SceneManager.LoadScene("Win");
        }

        else
        {
            //SceneManager.LoadScene("Lose");
        }
        
    }
}
