using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy
{
    private float followRange = 5;
    private float speedEnemy = 2;
    public Vector2 startPos;

    private Transform playerT;
    private Transform transform;

    private float minPos = -4;
    private float maxPos = 4;


    public Enemy(Transform _playerTransform, Transform _transform)
    {
        playerT = _playerTransform;
        transform = _transform;
        startPos = transform.position;
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
            if (Vector2.Distance(transform.position, startPos) <= 0)
            {
                if (transform.position.x <= startPos.x - minPos)
                {
                    transform.position = new Vector3(Mathf.Max(transform.position.x, startPos.x - minPos), transform.position.y, 0);
                    speedEnemy = -speedEnemy;
                }

                if (transform.position.x >= startPos.x + maxPos)
                {
                    transform.position = new Vector3(Mathf.Min(transform.position.x, startPos.x + maxPos), transform.position.y, 0);
                    speedEnemy = -speedEnemy;
                }
                transform.position = new Vector2(transform.position.x + (speedEnemy * Time.fixedDeltaTime), transform.position.y);
            }
            //go back to orginal pos
            else
            {
                transform.position = Vector2.MoveTowards(transform.position, startPos, speedEnemy * Time.deltaTime);
            }
        }
    }
}
