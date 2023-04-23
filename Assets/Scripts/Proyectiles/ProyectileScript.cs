using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProyectileScript : MonoBehaviour
{
    public float dmg;
    public string self;
    public float screenTime;
    public int pierce;

    void Start()
    {
        Invoke("Death", screenTime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != self)
        {
            if (collision.GetComponent<EnemyScript>())
            {
                collision.GetComponent<EnemyScript>().Hurt(dmg);
                if (--pierce <= 0)
                    Destroy(gameObject);
            }
        }
    }

    void Death()
    {
        Destroy(gameObject);
    }
}
