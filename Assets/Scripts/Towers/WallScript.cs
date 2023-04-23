using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallScript : MonoBehaviour
{
    public int maxEnemies;
    public float unfreezeTime;
    public float cooldown;

    private GameObject[] enemies;
    private SpriteRenderer sprite;
    private bool available;
    private int current;

    void Start()
    {
        available = true;
        enemies = new GameObject[maxEnemies];
        sprite = GetComponent<SpriteRenderer>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (available && collision.GetComponent<EnemyScript>())
        {
            enemies[current] = collision.gameObject;
            current++;
            collision.GetComponent<EnemyScript>().Slow(0, 0, 0);
            if (current > maxEnemies - 1)
            {
                available = false;
                sprite.color = new Color(0, 0, 0, 0.2f);
                Maxed();
            }
        }
    }

    void Maxed()
    {
        current--;
        if (enemies[current])
            enemies[current].GetComponent<EnemyScript>().Unfreeze();
        if (current == 0)
            Invoke("Enable", cooldown);
        else
            Invoke("Maxed", unfreezeTime);
    }

    void Enable()
    {
        available = true;
        sprite.color = new Color(0, 0, 0, 1f);
    }
}
