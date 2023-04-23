using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapScript : MonoBehaviour
{
    public ButtonScript button;
    public GameObject proyectile;
    public Vector2 direction;
    public float damage;
    public float force;
    public float coolDown;
    private float time;

    void Start(){
        time = coolDown;
    }

    void Update(){
        if(button.on){
            time -= Time.deltaTime;
            if (time <= 0){
                Shoot();
                time = coolDown;
            }
            //Invoke("Activation", coolDown);
        }
    }

    public void Shoot(){
        GameObject bullet = Instantiate(proyectile, transform.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody2D>().velocity = direction * force;
        bullet.GetComponent<ProyectileScript>().dmg = damage;
    }
}
