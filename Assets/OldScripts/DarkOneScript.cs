using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkOneScript : MonoBehaviour
{
    private bool canAttack;
    private bool canMove;
    private bool dashing;
    private bool beaming;
    private GameObject playerObj;
    private Vector2 direction;
    private Animator animator;
    private SpriteRenderer sprite;
    public float damage;
    public float speed;
    private float currentSpeed;
    public float minCooldown;
    public float maxCooldown;
    public GameObject proyectile;
    public float force;
    public int bullets;
    public int bulletSize;

    // Start is called before the first frame update
    void Start()
    {
        currentSpeed = speed;
        canAttack = true;
        canMove = false;
        dashing = false;
        beaming = false;
        animator = GetComponent<Animator>();
        playerObj = GameObject.FindGameObjectWithTag("Player");
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canAttack)
        {
            canAttack = false;
            var choices = new[] { "ChargeShoot", "ChargeDash", "ChargeBeam", "Stand" };
            Invoke(choices[Random.Range(0, 4)], 0.1f);
        }
        if (canMove)
        {
            Direction();
            if (direction.x < 0)
                sprite.flipX = true;
            else
                sprite.flipX = false;
            Move();
        }
        if (dashing)
            Move();
        if (beaming)
            Beam();
    }

    void Direction()
    {
        if (playerObj)
        {
            Vector2 playerPos = playerObj.transform.position;
            Vector2 myPos = transform.position;
            direction = (playerPos - myPos).normalized;
        }
        else
            direction = Vector2.zero;
    }

    void Move()
    {
        transform.Translate(direction * currentSpeed * Time.deltaTime);
    }

    void Stand()
    {
        currentSpeed = speed;
        canMove = true;
        dashing = false;
        beaming = false;
        animator.SetFloat("charge", 0);
        animator.SetBool("attack", false);
        animator.SetBool("beam", false);
        Invoke("ResumeAttack", Random.Range(minCooldown, maxCooldown));
    }

    void ChargeShoot()
    {
        animator.SetBool("attack", true);
        Invoke("Shoot", 0.5f);
    }

    void Shoot()
    {
        float angle = 180;
        for (int i = 0; i < bullets; i++)
        {
            GameObject bullet = Instantiate(proyectile, transform.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody2D>().velocity =
                new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad))
                * force;
            bullet.GetComponent<ProyectileScript>().dmg = damage;
            bullet.GetComponent<Transform>().localScale = new Vector3(bulletSize, bulletSize, 1);
            angle -= 360 / bullets;
        }
        Invoke("Stand", 0.5f);
    }

    void ChargeDash()
    {
        animator.SetFloat("charge", -1);
        Invoke("Dash1", 0.5f);
    }

    void Dash1()
    {
        animator.SetFloat("charge", 1);
        Direction();
        float y = direction.y;
        if (direction.x < 0)
        {
            direction = Vector2.left;
            direction.y = y / 2;
            sprite.flipX = true;
        }
        else
        {
            direction = Vector2.right;
            direction.y = y / 2;
            sprite.flipX = false;
        }
        currentSpeed *= 10;
        dashing = true;
        Invoke("Dash2", 1f);
    }

    void Dash2()
    {
        float y = direction.y;
        if (direction.x < 0)
        {
            direction = Vector2.right;
            direction.y = y / 2;
            sprite.flipX = false;
        }
        else
        {
            direction = Vector2.left;
            direction.y = y / 2;
            sprite.flipX = true;
        }
        Invoke("Stand", 1f);
    }

    void ChargeBeam()
    {
        beaming = true;
        animator.SetBool("beam", true);
        Invoke("Stand", 2f);
    }

    void Beam()
    {
        Direction();
        GameObject bullet = Instantiate(proyectile, transform.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody2D>().velocity = direction * force;
        bullet.GetComponent<ProyectileScript>().dmg = damage / 2;
        bullet.GetComponent<Transform>().localScale = new Vector3(
            bulletSize * 2,
            bulletSize * 2,
            1
        );
    }

    void ResumeAttack()
    {
        canAttack = true;
        canMove = false;
    }
}
