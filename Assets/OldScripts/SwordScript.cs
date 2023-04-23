using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordScript : MonoBehaviour
{
    public GameObject sword;
    public GameObject bomb;
    private GameObject spell;
    public float damage;
    public float attackSpeed;
    public float attackSize;
    private float angle;
    private float maxAngle;
    public float bombCoolDown;
    private float time;

    void Start()
    {
        time = bombCoolDown;
    }

    void Update()
    {
        if (GameObject.FindWithTag("Player"))
        {
            OnClick();
            Rotate();
        }
    }

    private void OnClick()
    {
        if (!spell && Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 myPos = transform.position;
            angle = AngleBetweenVector2(mousePos, myPos) + 20;
            maxAngle = angle + 180;
            Quaternion direction = Quaternion.Euler(Vector3.forward * angle);
            spell = Instantiate(sword, myPos, direction);
            spell.GetComponent<MeleeScript>().dmg = damage;
            spell.transform.localScale = new Vector2(attackSize, attackSize);
            spell.transform.parent = transform;
        }

        if (Input.GetKey(KeyCode.E))
        {
            time -= Time.deltaTime;
            if (time <= 0)
            {
                time = bombCoolDown;
                Instantiate(bomb, transform.position, Quaternion.identity);
            }
        }
    }

    private void Rotate()
    {
        if (spell)
        {
            if (angle <= maxAngle)
            {
                angle += attackSpeed;
                spell.transform.eulerAngles = Vector3.forward * angle;
            }
            else
                Destroy(spell);
        }
    }

    private float AngleBetweenVector2(Vector2 vec1, Vector2 vec2)
    {
        Vector2 diference = vec2 - vec1;
        float sign = (vec2.y < vec1.y) ? -1.0f : 1.0f;
        return Vector2.Angle(Vector2.right, diference) * sign;
    }
}
