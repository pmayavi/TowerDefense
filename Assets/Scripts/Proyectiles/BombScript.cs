using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombScript : MonoBehaviour
{
    public float dmg;
    public string self;
    public float screenTime;
    public float explotionSize;
    public float explotionDuration;

    private bool exploted;

    void Start()
    {
        exploted = false;
        Invoke("Death", screenTime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != self && collision.GetComponent<EnemyScript>())
        {
            collision.GetComponent<EnemyScript>().Hurt(dmg);
            if (!exploted)
            {
                exploted = true;
                gameObject.transform.localScale = new Vector3(explotionSize, explotionSize, 0);
                GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                Invoke("Death", explotionDuration);
            }
        }
    }

    void Death()
    {
        Destroy(gameObject);
    }
}