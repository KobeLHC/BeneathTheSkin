using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeterController : MonoBehaviour
{
    public GameObject player;
    public PlayerController playerObject;

    //sprite control
    private SpriteRenderer spriteRenderer;
    public Sprite startSus;
    public Sprite lowSus;
    public Sprite midSus;
    public Sprite highSus;
    public Sprite ggSus;



    void Start()
    {
        playerObject = player.GetComponent<PlayerController>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        susCheck();
    }

    public void susCheck()
    {
        if (playerObject.susLevel == 0)
        {
            spriteRenderer.sprite = startSus;
        }

        else if (playerObject.susLevel > 0 && playerObject.susLevel < 10)
        {
            spriteRenderer.sprite = lowSus;
        }

        else if (playerObject.susLevel >= 10 && playerObject.susLevel < 25)
        {
            spriteRenderer.sprite = midSus;
        }

        else if (playerObject.susLevel >= 25 && playerObject.susLevel <= 40)
        {
            spriteRenderer.sprite = highSus;
        }
        else
        {
            spriteRenderer.sprite = ggSus;
        }
    }
}
