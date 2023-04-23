using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    private Vector2 direction;
    private Vector2 attackDir;
    private Animator animator;
    private SpriteRenderer sprite;
    public GameObject bomb;
    public GameObject dmgbox;
    private GameObject attack;
    public float damage;
    private float angle;
    public float bombCoolDown;
    private float time;
    private bool freeze;

    void Start()
    {
        time = bombCoolDown;
        freeze = false;
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (GameObject.FindWithTag("Player"))
        {
            OnClick();
            OnInput();
            if (attack)
                Animations(attackDir);
            else if (!freeze)
                Move();
        }
    }

    private void OnInput()
    {
        direction = Vector2.zero;
        int j = 0;

        if (Input.GetKey(KeyCode.W))
        {
            direction += Vector2.up;
            j++;
        }
        if (Input.GetKey(KeyCode.A))
        {
            if (j == 0)
            {
                direction += Vector2.left;
                j++;
            }
            else
            {
                j++;
                direction *= (1 - 1 / j);
                direction += (Vector2.left / j);
            }
        }
        if (Input.GetKey(KeyCode.S))
        {
            if (j == 0)
            {
                direction += Vector2.down;
                j++;
            }
            else
            {
                j++;
                direction *= (1 - 1 / j);
                direction += (Vector2.down / j);
            }
        }
        if (Input.GetKey(KeyCode.D))
        {
            if (j == 0)
            {
                direction += Vector2.right;
            }
            else
            {
                j++;
                direction *= (1 - 1 / j);
                direction += (Vector2.right / j);
            }
        }
        if (Input.GetKey(KeyCode.E))
        {
            if (time <= 0)
            {
                freeze = true;
                animator.SetBool("Crouch", true);
                time = bombCoolDown;
                Invoke("Bomb", 0.4f);
            }
        }
    }

    private void OnClick()
    {
        if (!attack && Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 myPos = transform.position;
            angle = AngleBetweenVector2(mousePos, myPos);
            Quaternion quat = Quaternion.Euler(Vector3.forward * angle);
            if (angle <= -135 || angle > 135)
            {
                attackDir += Vector2.right;
                quat = Quaternion.Euler(Vector3.forward * 90);
            }
            else if (angle <= -45)
            {
                attackDir += Vector2.up;
                quat = Quaternion.Euler(Vector3.forward * 180);
            }
            else if (angle <= 45)
            {
                attackDir += Vector2.left;
                quat = Quaternion.Euler(Vector3.forward * -90);
            }
            else if (angle <= 135)
            {
                attackDir += Vector2.down;
                quat = Quaternion.Euler(Vector3.forward * 0);
            }
            direction = attackDir;
            attack = Instantiate(dmgbox, myPos, quat);
            attack.GetComponent<MeleeScript>().dmg = damage;
            //attack.transform.localScale = new Vector2(attackSize, attackSize);
            attack.transform.parent = transform;
            Invoke("AttackBox", 0.01f);
        }
    }

    private void Move()
    {
        float delta = Time.deltaTime;
        time -= delta;
        Animations(direction);
        transform.Translate(direction * speed * delta);
    }

    private void Animations(Vector2 direction)
    {
        if (direction.x == 0)
            animator.SetBool("X", false);
        else
            animator.SetBool("X", true);
        if (direction.x > 0)
            sprite.flipX = true;
        else if (direction.x < 0)
            sprite.flipX = false;
        animator.SetFloat("Y", direction.y);
    }

    private void AttackBox()
    {
        animator.SetBool("Attack", true);
        Invoke("Destruction", 0.25f);
    }

    private void Destruction()
    {
        Destroy(attack);
        animator.SetBool("Attack", false);
        attackDir = Vector2.zero;
    }

    private void Bomb()
    {
        freeze = false;
        animator.SetBool("Crouch", false);
        Instantiate(bomb, transform.position + Vector3.down, Quaternion.identity);
    }

    private float AngleBetweenVector2(Vector2 vec1, Vector2 vec2)
    {
        Vector2 diference = vec2 - vec1;
        float sign = (vec2.y < vec1.y) ? -1.0f : 1.0f;
        return Vector2.Angle(Vector2.right, diference) * sign;
    }
}
