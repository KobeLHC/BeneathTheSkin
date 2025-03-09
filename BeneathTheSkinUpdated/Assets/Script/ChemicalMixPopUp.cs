using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChemicalMixPopUp : MonoBehaviour
{
    // later add player sprite conditon
    public PlayerController other;

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
        /// Comment out part is for when we want to add condition like buddy change into someone to active the chemcial mix
        if (Input.GetKeyDown(KeyCode.Space) && playerIsClose && gameObject != null/*&& other.GetAnimator().GetBool("___") == true*/)
        {
            chemicalScene.SetActive(true);
        }

        if (chemicalScene.active)
        {
            other.changeRunSpeed(0.0f);
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = false;
        }
    }
}
