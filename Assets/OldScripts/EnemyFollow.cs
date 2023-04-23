using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    protected GameObject playerObj = null;
    protected Animator animator;
    protected Vector2 direction;
    protected float damage;
    protected float speed;

    public virtual void Start()
    {
        if (GetComponent<Animator>())
            animator = GetComponent<Animator>();
        if (playerObj == null)
            playerObj = GameObject.FindGameObjectWithTag("Player");
    }

    public virtual void Update()
    {
        Direction();
        Move();
    }

    public virtual void Direction()
    {
        if (playerObj)
        {
            Vector2 playerPos = playerObj.transform.position;
            Vector2 myPos = transform.position;
            direction = (playerPos - myPos).normalized;
        }else direction = Vector2.zero;
    }

    public virtual void Move()
    {
        transform.Translate(direction * speed * Time.deltaTime);
        Animations(direction);
    }

    private void Animations(Vector2 direction)
    {
        animator.SetFloat("X", direction.x);
        animator.SetFloat("Y", direction.y);
    }

    public void SetDamage(float dmg){
        damage = dmg;
    }

    public void SetSpeed(float spd){
        speed = spd;
    }
}