using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TackTower : TowerScript
{
    public int bullets;

    void Shoot()
    {
        shoot = false;
        float angle = 180;
        for (int i = 0; i < bullets; i++)
        {
            GameObject bullet = Instantiate(proyectile, transform.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody2D>().velocity =
                new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad))
                * force;
            bullet.GetComponent<ProyectileScript>().dmg = damage;
            bullet.GetComponent<ProyectileScript>().pierce = pierce;
            bullet.GetComponent<ProyectileScript>().screenTime = screenTime;
            angle -= 360 / bullets;
        }
        Invoke("Continue", cooldown); //Cambiar por Bullet Spawner
    }

    void Continue()
    {
        shoot = true;
    }

    public void Update()
    {
        if (enemy && shoot)
        {
            Shoot();
        }
    }
}
