using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellScript : MonoBehaviour
{
    public GameObject proyectile;
    public float damage;
    public float force;
    public float size;

    void Update()
    {
        if (GameObject.FindWithTag("Player"))
        {
            if (Input.GetMouseButtonDown(1))
            {
                GameObject spell = Instantiate(proyectile, transform.position, Quaternion.identity);
                Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 myPos = transform.position;
                Vector2 direction = (mousePos - myPos).normalized;
                spell.GetComponent<Rigidbody2D>().velocity = direction * force;
                spell.GetComponent<ProyectileScript>().dmg = damage;
                spell.transform.localScale = new Vector2(size, size);
            }
        }
    }
}