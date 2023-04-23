using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapTower : MonoBehaviour
{
    public float dmg;
    public int spikes;
    public int maxSpikes;
    public float cooldown;
    public float rangeToSpawn;
    public GameObject baseSpike;
    public GameObject[] spikeObjects;

    void Start()
    {
        spikes = 0;
        spikeObjects = new GameObject[maxSpikes];
        Invoke("ReleaseSpike", cooldown);
    }

    void ReleaseSpike()
    {
        if (spikes < maxSpikes)
        {
            Vector2 where = new Vector2();
            where.x = transform.localPosition.x + Random.Range(-rangeToSpawn, rangeToSpawn);
            where.y = transform.localPosition.y + Random.Range(-rangeToSpawn, rangeToSpawn);
            spikeObjects[spikes++] = Instantiate(baseSpike, where, Quaternion.identity);
        }
        Invoke("ReleaseSpike", cooldown);
    }

    void RemoveSpike()
    {
        GameObject remove = spikeObjects[--spikes];
        spikeObjects[spikes] = null;
        Destroy(remove);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<EnemyScript>())
        {
            if (spikes > 0)
            {
                collision.GetComponent<EnemyScript>().Hurt(dmg);
                RemoveSpike();
            }
        }
    }
}
