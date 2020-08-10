using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_on_player : MonoBehaviour
{
    public GameObject player;
    public float offset;
    private Vector3 playerPosition;
    public float offsetSmoothing;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Camera follow with some smoothing
        playerPosition = new Vector3(player.transform.position.x, transform.position.y, transform.position.z);
        if (player.transform.localScale.x > 0f )
        {
            playerPosition = new Vector3(playerPosition.x + offset, playerPosition.y, playerPosition.z);
        }
        else if (player.transform.localScale.x < 0f)
        {
            playerPosition = new Vector3(playerPosition.x - offset, playerPosition.y, playerPosition.z);
        }
        else if (player.transform.localScale.y > 0f)
        {
            playerPosition = new Vector3(playerPosition.x , playerPosition.y + offset, playerPosition.z);
        }
        else if (player.transform.localScale.y < 0f)
        {
            playerPosition = new Vector3(playerPosition.x , playerPosition.y - offset, playerPosition.z);
        }
        transform.position = Vector3.Lerp(transform.position, playerPosition, offsetSmoothing * Time.deltaTime);
    }
}
