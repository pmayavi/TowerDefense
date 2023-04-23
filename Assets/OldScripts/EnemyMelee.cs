using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMelee : MonoBehaviour
{
    public float damage;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerMovement>() != null)
        {
            GameObject.Find("GameManager").GetComponent<PlayerStats>().Hurt(damage);
        }
    }

    public void SetDamage(float dmg)
    {
        damage = dmg;
    }
}
