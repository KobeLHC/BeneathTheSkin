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

    //Output to final gate script
    public bool solved;

    //Pause Menu
    public GameObject pauseMenu;
    public bool pauseOn;

    void Start()
    {
        //Getting component at start
        cardObject = cardPuzzle.GetComponent<CardSwipe>();
        passwordObject = passwordPuzzle.GetComponent<EnterPasswordSystem>();
        chemicalObject = chemicalPuzzle.GetComponent<LevelCode>();
        playerObject = player.GetComponent<PlayerController>();
        solved = false;
        pauseMenu.SetActive(false);
        pauseOn = false;
    }

    void Update()
    {
        //Continously checking for gameobject condition
        cardDone = cardObject.getCondition();
        passwordDone = passwordObject.getCondition();
        chemicalDone = chemicalObject.getCondition();
        playerLose = playerObject.susLevel;

        //press esc to activate pause menu
        if (Input.GetKeyDown(KeyCode.Escape) && !pauseOn)
        {
            pauseMenu.SetActive(true);
            pauseOn = true;
            Time.timeScale = 0f;
        }

       else if (Input.GetKeyDown(KeyCode.Escape) && pauseOn)
        {
            pauseMenu.SetActive(false);
            pauseOn = false;
            Time.timeScale = 1f;
        }

        //if all puzzle solved then send true to final gate script
        if (cardDone && passwordDone && chemicalDone)
        {
            solved = true;
        }       

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
