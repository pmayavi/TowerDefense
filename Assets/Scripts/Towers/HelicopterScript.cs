using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelicopterScript : TowerScript
{
    public float speed;
    public float rotationSpeed;

    void Update()
    {
        if (enemy)
        {
            Direction2();
            Move();
            if (shoot)
                Shoot();
        }
    }

    public void Shoot()
    {
        direction *= -1;
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

    void Move()
    {
        float inputMagnitude = Mathf.Clamp01(direction.magnitude);
        direction.Normalize();

        transform.Translate(direction * speed * inputMagnitude * Time.deltaTime, Space.World);

        if (direction != Vector2.zero)
        {
            Quaternion rotation = Quaternion.LookRotation(Vector3.forward, direction);
            rotation *= Quaternion.Euler(0, 0, 90);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
        }
    }

    public void Direction2()
    {
        if (enemy)
        {
            Vector2 enemyPos = enemy.transform.position;
            Vector2 myPos = transform.position;
            direction = (enemyPos - myPos);
        }
        else direction = Vector2.zero;
    }
}
