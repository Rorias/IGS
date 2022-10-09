using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy
{
    private float followRange = 5;
    private float speedEnemy = 2;
    public Vector2 currentpos;

    private Transform playerT;
    private Transform transform;

    public Enemy(Transform _playerTransform, Transform _transform)
    {
        playerT = _playerTransform;
        transform = _transform;
        currentpos = transform.position;
    }

    public void UpdateEnemy()
    {
        //move to player
        if (Vector2.Distance(transform.position, playerT.position) < followRange)
        {
            transform.position = Vector2.MoveTowards(transform.position, playerT.position, speedEnemy * Time.deltaTime);
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
