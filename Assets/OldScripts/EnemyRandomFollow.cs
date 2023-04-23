using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRandomFollow : EnemyFollow
{
    public int changeDirection;

    public override void Direction()
    {
        int ran = Random.Range(0, changeDirection);
        if (playerObj && ran == 1)
        {
            Vector2 playerPos = playerObj.transform.position;
            Vector2 myPos = transform.position;
            direction = (playerPos - myPos).normalized;
        }
        else if (ran == 0)
        {
            var choices = new[] { Vector2.up, Vector2.left, Vector2.right, Vector2.down };
            direction += choices[Random.Range(0, 4)] * 2;
        }
    }
}
