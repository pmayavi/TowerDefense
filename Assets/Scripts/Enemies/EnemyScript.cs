using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public int count;
    public float speed;
    public float maxLife;

    private GameObject[] paths;
    private GameObject path;
    private Vector2 direction;
    private float originalSpeed;
    private float lastSpeed;
    private float life;
    private int max;
    private float R, G;

    void Start()
    {
        G = 0;
        originalSpeed = speed;
        count = 0;
        life = maxLife;
        R = life / maxLife;
        paths = GameObject.FindGameObjectsWithTag("Path");
        path = paths[count];
        max = paths.Length;
    }

    public void Direction()
    {
        if (path)
        {
            Vector2 pos = path.transform.position;
            Vector2 myPos = transform.position;
            direction = (pos - myPos).normalized;
            if ((pos - myPos).x < 0.05 && (pos - myPos).x > -0.05 && (pos - myPos).y < 0.05 && (pos - myPos).y > -0.05)
            {
                if (count == max)
                    Destroy(gameObject);
                else
                {
                    path = paths[count++];
                    Direction();
                }
                return;
            }
        }
        else Destroy(gameObject);

    }

    public void Move()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    void Update()
    {
        Direction();
        Move();
    }

    public void Hurt(float dmg)
    {
        life -= dmg;
        R = life / maxLife;
        GetComponent<SpriteRenderer>().color = new Color(R, G * R, 0);
        if (life <= 0)
            Destroy(gameObject);
    }

    public void Slow(float perc, float time, float color)
    {
        speed *= perc;
        G = color * R;
        GetComponent<SpriteRenderer>().color = new Color(R, G, 0);
        Move();
        if (time > 0)
            Invoke("Deslow", time);
    }

    public void Deslow()
    {
        if (speed > 0)
            speed = originalSpeed;
        G = 0;
        GetComponent<SpriteRenderer>().color = new Color(life / maxLife, 0, 0);
        Move();
    }

    public void Unfreeze()
    {
        speed = originalSpeed;
        Move();
    }
}
