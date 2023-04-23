using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicTower : TowerScript
{
    public void Shoot()
    {
        shoot = false;
        if (enemy && direction != Vector2.zero)
        {
            GameObject bullet = Instantiate(proyectile, transform.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody2D>().velocity = direction * force;
            bullet.GetComponent<ProyectileScript>().screenTime = screenTime;
            bullet.GetComponent<ProyectileScript>().pierce = pierce;
            bullet.GetComponent<ProyectileScript>().dmg = damage;
        }
        Invoke("Continue", cooldown);
    }

    void Continue()
    {
        shoot = true;
    }

    public void Update()
    {
        if (enemy && shoot)
        {
            Direction();
            Shoot();
        }
    }
}
