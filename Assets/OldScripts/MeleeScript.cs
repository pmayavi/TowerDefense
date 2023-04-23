using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeScript : MonoBehaviour
{
    public float dmg;
    public string self;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != self)
        {
            if (collision.GetComponent<PlayerMovement>() != null)
            {
                GameObject.Find("GameManager").GetComponent<PlayerStats>().Hurt(dmg);
            }
        }
    }
}
