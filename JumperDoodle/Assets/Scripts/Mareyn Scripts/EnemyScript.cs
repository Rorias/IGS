using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    private Transform playerPos;
    private Vector2 currentpos;
    public float distance;
    public float speedEnemy;

    void Start()
    {
        playerPos = player.GetComponent<Transform>();
        currentpos = GetComponent<Transform>().position;
    }

    // Update is called once per frame
    void Update()
    {
        //move to player
        if (Vector2.Distance(transform.position, playerPos.position) < distance)
        {
            transform.position = Vector2.MoveTowards(transform.position, playerPos.position, speedEnemy * Time.deltaTime);
        }
        else
        //patrolling
        {
            if (Vector2.Distance(transform.position, currentpos) <= 0)
            {

            }
            //go back to orginal pos
            else
            {
                transform.position = Vector2.MoveTowards(transform.position, currentpos, speedEnemy * Time.deltaTime);
            }
        }
    }
}
