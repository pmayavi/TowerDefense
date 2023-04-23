using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    public GameObject enemy;
    public float cooldown;

    void Start()
    {
        Invoke("Spawn", cooldown);
    }

    void Spawn()
    {
        Instantiate(enemy, transform.position, Quaternion.identity);
        Invoke("Spawn", cooldown);
    }
}
