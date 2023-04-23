using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowingTower : MonoBehaviour
{
    public float percentage;
    public float slowTime;
    public float color;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<EnemyScript>())
        {
            collision.GetComponent<EnemyScript>().Slow(percentage, slowTime, color);
        }
    }
}
