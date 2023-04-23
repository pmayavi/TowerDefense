using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    public Color unpressed;
    public Color pressed;
    protected SpriteRenderer sprite;
    public bool on;

    void Start()
    {
        on = false;
        sprite = GetComponent<SpriteRenderer>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {     
        sprite.color = pressed;
        on = true;
    }

    void OnTriggerExit2D(Collider2D collision)
    {     
        sprite.color = unpressed;
        on = false;
    }
}
