using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicProyectile : MonoBehaviour
{
    public float speed;
    public GameObject enemy;
    public Vector2 direction;

    public virtual void Update()
    {
        Direction();
        Move();
    }

    public void Direction()
    {
        if (enemy)
        {
            Vector2 playerPos = enemy.transform.position;
            Vector2 myPos = transform.position;
            direction = (playerPos - myPos).normalized;
            speed *= 1.01f;
        }
    }

    public void Move()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }
}
