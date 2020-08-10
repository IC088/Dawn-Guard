using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyGraphics : MonoBehaviour
{

    public AIPath aipath;

    // Update is called once per frame
    void Update()
    {
        swapPath(aipath);
        
    }

    void swapPath(AIPath aiPath)
    {
        /*
         * If the enemy path is going through to the right then x *= -1
         */
        if (aiPath.desiredVelocity.x >= 0.01f)
        {
            transform.localScale = new Vector3(5f, 5f, 5f);
        }

        else if (aiPath.desiredVelocity.x <= -0.01f)
        {
            transform.localScale = new Vector3(-5f, 5f, 5f);
        }
    }
}
