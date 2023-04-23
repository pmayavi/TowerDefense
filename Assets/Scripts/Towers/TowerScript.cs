using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerScript : MonoBehaviour
{
    public int pierce;
    public float force;
    public float damage;
    public float cooldown;
    public float screenTime;
    public GameObject proyectile;

    protected bool shoot;
    protected GameObject enemy;
    protected Vector2 direction;
    protected List<GameObject> inRange;

    void Start()
    {
        shoot = true;
        inRange = new List<GameObject>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<EnemyScript>())
        {
            inRange.Add(collision.gameObject);
            if (!enemy)
                enemy = collision.gameObject;
            else if (enemy.GetComponent<EnemyScript>().count < collision.GetComponent<EnemyScript>().count) //Puede que tenga que poner .gameobect despues de collision
            {
                enemy = collision.gameObject;
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        inRange.Remove(collision.gameObject);
        if (collision.gameObject == enemy)
        {
            enemy = null;
            foreach (GameObject g in inRange)
            {
                if (!g)
                    continue;
                else if (!enemy)
                    enemy = g;
                else if (enemy.GetComponent<EnemyScript>().count < g.GetComponent<EnemyScript>().count) // Puede que este targueteando mal
                    enemy = g;
            }
        }
    }

    public void Direction()
    {
        if (enemy)
        {
            Vector2 enemyPos = enemy.transform.position;
            Vector2 myPos = transform.position;
            direction = (enemyPos - myPos).normalized;
        }
        else direction = Vector2.zero;
    }
}
