using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSwipePopUp : MonoBehaviour
{
    public PlayerController other;
    public GameObject bgTint;
    public GameObject cardSwipe;
    public bool playerIsClose;

    // Start is called before the first frame update
    void Start()
    {
        bgTint.SetActive(false);
        cardSwipe.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && playerIsClose && other.GetAnimator().GetBool("changeSci") == true)
        {
            bgTint.SetActive(true);
            cardSwipe.SetActive(true);
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
