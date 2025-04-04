using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D body;

    float horizontal;
    float vertical;
    float moveLimiter = 0.7f;
    public Vector2 faceLeft;

    public float runSpeed = 8.0f;
    public int susLevel = 0;

    //Change Sprite
    private SpriteRenderer spriteRenderer;
    
    //Scientist sprite
    public Sprite newSprite;

    //Security sprite
    public Sprite newSprite2;

    //Scientist 2 sprite
    public Sprite newSprite3;

    //Player basic sprite
    public Sprite basicSprite;

    //which shape to change
    public bool sciOn;
    public bool secOn;
    public bool sci2On;

    //Animation
    public Animator anim;

    //Direction Control
    public bool moveUp;
    public bool moveDown;
    public bool moveLeft;
    public bool moveRight;
    //public bool idle;

    // Player Animation controller
    public bool changedSecSprite;
    public bool changedSci2Sprite;
    public bool resetedSprite;

    //Sound effect control
    AudioManager audioManager;
    private bool isWalking;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        sciOn = false;
        secOn = false;
        sci2On = false;
        isWalking = false;
        changedSecSprite = false;
        changedSci2Sprite = false;
        resetedSprite = true;
    }

    void Update()
    {
        // Getting direction as bool and limiting player movement control by WASD and only one direction bool is true at a time
        moveUp = false;
        moveDown = false;
        moveLeft = false;
        moveRight = false;

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            moveUp = true;
            moveDown = false;
            moveLeft = false;
            moveRight = false;
            //Debug.Log("Up");
        }

        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            moveUp = false;
            moveDown = false;
            moveLeft = true;
            moveRight = false;
            //Debug.Log("Left");
        }

        else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            moveUp = false;
            moveDown = true;
            moveLeft = false;
            moveRight = false;
            //Debug.Log("Down");
        }

        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            moveUp = false;
            moveDown = false;
            moveLeft = false;
            moveRight = true;
            //Debug.Log("Right");
        }

        //Changing to Sec animation and which animation to play
        if (changedSecSprite)
        {
            //different animation for security movement
            anim.SetBool("secIdle", !(moveUp || moveDown || moveLeft || moveRight));
            anim.SetBool("secUp", moveUp);
            anim.SetBool("secLeft", moveLeft);
            anim.SetBool("secRight", moveRight);
            anim.SetBool("secDown", moveDown);
        }

        //Changing to Sci2 animation and which animation to play
        if (changedSci2Sprite)
        {
            //different animation for scientist2 movement
            anim.SetBool("sci2Idle", !(moveUp || moveDown || moveLeft || moveRight));
            anim.SetBool("sci2Up", moveUp);
            anim.SetBool("sci2Left", moveLeft);
            anim.SetBool("sci2Right", moveRight);
            anim.SetBool("sci2Down", moveDown);
        }

        //Changing back to buddy animation
        if (resetedSprite)
        {
            //No need to set Idle condition
            anim.SetBool("moveUp", moveUp);
            anim.SetBool("moveDown", moveDown);
            anim.SetBool("moveRight", moveRight);
            anim.SetBool("moveLeft", moveLeft);
        }

        // Gives a value between -1 and 1
        horizontal = Input.GetAxisRaw("Horizontal"); // -1 is left | 1 is right
        vertical = Input.GetAxisRaw("Vertical"); // -1 is down | 1 is up

        //Walking SFX control
        if (horizontal != 0 || vertical != 0)
        {

            if (!isWalking)
            {
                audioManager.LoopSFX(audioManager.walking);
                isWalking = true;
            }
        }

        else
        {
            if (isWalking)
            {
                audioManager.EndSFX(audioManager.walking);
                isWalking = false;
            }
        }

        //shape shift
        if (Input.GetKeyDown(KeyCode.C) == true)
        {
            if (sciOn == true)
            {
                ChangePlayerSci();
            }

            else if(secOn == true)
            {
                ChangePlayerSec();
            }

            else if(sci2On == true)
            {
                ChangePlayerSci2();
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

    public void changeRunSpeed(float speed)
    {
        runSpeed = speed;
    }

    public void moreSus(int value)
    {
        susLevel += value;
    }

    public void lessSus(int value)
    {
        susLevel -= value;
    }

    //player change into scientist
    public void ChangePlayerSci()
    {
        if (anim.GetBool("changeSci") == false)
        {
            spriteRenderer.sprite = newSprite;
            anim.SetBool("changeSci", true);
            anim.SetBool("changeSec", false);
            anim.SetBool("changeSci2", false);
            Debug.Log("changeSci true");
        }

        else
        {
            spriteRenderer.sprite = basicSprite;
            anim.SetBool("changeSci", false);
            Debug.Log("changeSci false");
        }
    }

    //player change into security
    public void ChangePlayerSec()
    {
        //player changed into sec
        if (changedSecSprite == false)
        {
            spriteRenderer.sprite = newSprite2;
            changedSecSprite = true;
            changedSci2Sprite = false;
            resetedSprite = false;
            ResetAllAnimations();
            //Debug.Log("changedSecSprite true");
        }

        //Reset player sprite to buddy
        else
        {
            changedSecSprite = false;
            ResetAllAnimations();
            spriteRenderer.sprite = basicSprite;
            //Debug.Log("changedSecSprite false");
        }
    }

    //player change into scientist 2
    public void ChangePlayerSci2()
    {
        //changed into sci2
        if (changedSci2Sprite == false)
        {
            spriteRenderer.sprite = newSprite3;
            changedSci2Sprite = true;
            changedSecSprite = false;
            resetedSprite = false;
            ResetAllAnimations();
            //Debug.Log("changedSci2Sprite true");
        }

        //Reset player sprite to buddy
        else
        {
            changedSci2Sprite = false;
            ResetAllAnimations();
            spriteRenderer.sprite = basicSprite;
            //Debug.Log("changedSci2Sprite false");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //control which sprite to change into
        if(other.gameObject.tag == "Sci")
        {
            sciOn = true;
            secOn = false;
            sci2On = false;
        }
        else if (other.gameObject.tag == "Sec")
        {
            secOn = true;
            sciOn = false;
            sci2On = false;
        }
        else if(other.gameObject.tag == "Sci2")
        {
            sci2On = true;
            sciOn = false;
            secOn = false;
        }
    }

    private void ResetAllAnimations()
    {
        //Security anim bool
        anim.SetBool("secIdle", false);
        anim.SetBool("secUp", false);
        anim.SetBool("secLeft", false);
        anim.SetBool("secRight", false);
        anim.SetBool("secDown", false);

        //Sci2 anim bool
        anim.SetBool("sci2Idle", false);
        anim.SetBool("sci2Up", false);
        anim.SetBool("sci2Left", false);
        anim.SetBool("sci2Right", false);
        anim.SetBool("sci2Down", false);

        //Jerry anim bool
        anim.SetBool("changeSci", false);

        //Setting back Buddy animation
        resetedSprite = true;
    }

    public Animator GetAnimator()
    {
        return anim;
    }
}
