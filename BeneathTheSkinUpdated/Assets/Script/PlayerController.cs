using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D body;

    float horizontal;
    float vertical;
    float moveLimiter = 0.7f;

    public float runSpeed = 8.0f;
    public int susLevel = 0;

    //Change Sprite
    private SpriteRenderer spriteRenderer;
    
    //Scientist sprite
    public Sprite newSprite;

    //Security sprite
    public Sprite newSprite2;

    //Player basic sprite
    public Sprite basicSprite;

    //which shape to change
    private bool sciOn;
    private bool secOn;

    //Animation
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        sciOn = false;
        secOn = false;
    }

    void Update()
    {
        // Gives a value between -1 and 1
        horizontal = Input.GetAxisRaw("Horizontal"); // -1 is left | 1 is right
        vertical = Input.GetAxisRaw("Vertical"); // -1 is down | 1 is up

        //shape shift
        if(Input.GetKeyDown(KeyCode.C) == true)
        {
            if (sciOn == true)
            {
                ChangePlayerSci();
            }

            else if(secOn == true)
            {
                ChangePlayerSec();
            }

            else
            {
                Debug.Log("No collision yet");
            }
        }
    }

    void FixedUpdate()
    {
        if (horizontal != 0 && vertical != 0) // Check for diagonal movement
        {
            // limit movement speed diagonally, so you move at 70% speed
            horizontal *= moveLimiter;
            vertical *= moveLimiter;
        }

        body.velocity = new Vector2(horizontal * runSpeed, vertical * runSpeed);
    }

    public void moreSus(int value)
    {
        susLevel += value;
    }

    public void lessSus(int value)
    {
        susLevel -= value;
    }

    public void ChangePlayerSci()
    {
        if (anim.GetBool("changeSci") == false)
        {
            spriteRenderer.sprite = newSprite;
            anim.SetBool("changeSci", true);
            anim.SetBool("changeSec", false);
            Debug.Log("changeSci true");
        }

        else
        {
            spriteRenderer.sprite = basicSprite;
            anim.SetBool("changeSci", false);
            Debug.Log("changeSci false");
        }
    }

    public void ChangePlayerSec()
    {
        if (anim.GetBool("changeSec") == false)
        {
            spriteRenderer.sprite = newSprite2;
            anim.SetBool("changeSec", true);
             anim.SetBool("changeSci", false);
            Debug.Log("changeSec true");
        }

        else
        {
            spriteRenderer.sprite = basicSprite;
            anim.SetBool("changeSec", false);
            Debug.Log("changeS false");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //control which sprite to change into
        if(other.gameObject.tag == "Sci")
        {
            sciOn = true;
            secOn = false;
        }
        else if (other.gameObject.tag == "Sec")
        {
            secOn = true;
            sciOn = false;
        }
    }

    public Animator GetAnimator()
    {
        return anim;
    }
}
