using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChemicalMixPopUp : MonoBehaviour
{
    // later add player sprite conditon
    //public PlayerController other;

    //In use
    public GameObject chemicalScene;
    public bool playerIsClose;

    void Start()
    {
        chemicalScene.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && playerIsClose && gameObject != null/*&& other.GetAnimator().GetBool("___") == true*/)
        {
            chemicalScene.SetActive(true);
            Time.timeScale = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = true;
        }
    }
}
