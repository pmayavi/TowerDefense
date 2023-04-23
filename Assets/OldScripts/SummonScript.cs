using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonScript : MonoBehaviour
{
    public GameObject boss;
    public int candles;
    private bool summon = false;

    void Start() { }

    void Update() { }

    public void Light()
    {
        candles -= 1;
        if (candles == 0)
            summon = true;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (summon && collision.GetComponent<PlayerMovement>() != null)
        {
            Instantiate(boss, transform.position, Quaternion.identity);
            summon = false;
        }
    }
}
