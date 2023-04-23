using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : EnemyFollow
{
    public GameObject proyectile;
    public float force;

    public override void Move()
    {
        if (playerObj && animator.GetBool("shoot") == true)
        {
            animator.SetBool("shoot", false);
            GameObject bullet = Instantiate(proyectile, transform.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody2D>().velocity = direction * force;
            bullet.GetComponent<ProyectileScript>().dmg = damage;
        }
    }
}
