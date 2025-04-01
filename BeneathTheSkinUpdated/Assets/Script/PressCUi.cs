using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressCUi : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public Sprite pressCUIOn;

    public NPC trigger;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (trigger.pressCUIOn)
        {
            spriteRenderer.sprite = pressCUIOn;
        }
        
    }
}
