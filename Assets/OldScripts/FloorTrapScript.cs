using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorTrapScript : MonoBehaviour
{
    public PlayerStats player;
    public Color stand;
    public Color broken;
    protected SpriteRenderer sprite;
    public float resistTime;
    public float breakTime;
    private Color original;
    private List<Collider2D> colliders;
    

    void Start()
    {
        player = GameObject.Find("GameManager").GetComponent<PlayerStats>();
        sprite = GetComponent<SpriteRenderer>();
        original = sprite.color;
        colliders = new List<Collider2D>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    { 
        colliders.Add(collision);
        StartCoroutine(Break());
    }

    void OnTriggerExit2D(Collider2D collision)
    { 
        colliders.Remove(collision);
    }

    public IEnumerator Break(){
        sprite.color = stand;
        yield return new WaitForSeconds(resistTime);
        sprite.color = broken;
        GetComponent<BoxCollider2D>().isTrigger = false;
        for (int i = colliders.Count - 1; i >= 0; i--)
        {
            GameObject temp = colliders[i].gameObject;
            colliders.RemoveAt(i);
            Destroy(temp);
        }
        yield return new WaitForSeconds(breakTime);
        sprite.color = original;
        GetComponent<BoxCollider2D>().isTrigger = true;
    }

}
