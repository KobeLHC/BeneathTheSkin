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
    public GameObject player;

    //Getting GameObject Component and condition
    public CardSwipe cardObject;
    public EnterPasswordSystem passwordObject;
    public LevelCode chemicalObject;
    public PlayerController playerObject;

    //In script bool/condition to control what scene to load
    public bool cardDone;
    public bool passwordDone;
    public bool chemicalDone;
    public int playerLose;

    void Start()
    {
        //Getting component at start
        cardObject = cardPuzzle.GetComponent<CardSwipe>();
        passwordObject = passwordPuzzle.GetComponent<EnterPasswordSystem>();
        chemicalObject = chemicalPuzzle.GetComponent<LevelCode>();
        playerObject = player.GetComponent<PlayerController>();
    }

    void Update()
    {
        //Continously checking for gameobject condition
        cardDone = cardObject.getCondition();
        passwordDone = passwordObject.getCondition();
        chemicalDone = chemicalObject.getCondition();
        playerLose = playerObject.susLevel;

        if(playerLose >= 100)
        {
            SceneManager.LoadScene("Lose");
        }
        
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
