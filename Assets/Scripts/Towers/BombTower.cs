using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombTower : TowerScript
{
    public float explotionSize;
    public float explotionDuration;
    public float shortCooldown;

    public void Shoot()
    {
        shoot = false;
        if (enemy && direction != Vector2.zero)
        {
            GameObject bullet = Instantiate(proyectile, transform.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody2D>().velocity = direction * force;
            bullet.GetComponent<BombScript>().explotionDuration = explotionDuration;
            bullet.GetComponent<BombScript>().explotionSize = explotionSize;
            bullet.GetComponent<BombScript>().screenTime = screenTime;
            bullet.GetComponent<BombScript>().dmg = damage;
        }
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
            Invoke("Shoot", shortCooldown);
            Invoke("Continue", cooldown);
        }
    }
}
