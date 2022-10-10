using UnityEngine;

public class Enemy
{
    private Transform playerTransform;
    private Transform transform;

    private Vector2 startPos;

    private float followRange = 5;
    private float enemySpeed = 2;
    private float minPos = 4;
    private float maxPos = 4;

    private bool following = false;

    public Enemy(Transform _playerTransform, Transform _transform)
    {
        playerTransform = _playerTransform;
        transform = _transform;
        startPos = transform.position;
    }

    public bool UpdateEnemy()
    {
        MoveEnemy();

        //if not already following player
        if (!following)
        {
            Patrol();
        }

        return HitPlayer();
    }

    public void ResetEnemy(Transform _gameEnemy)
    {
        _gameEnemy.position = new Vector2(Random.Range(-5, 6), Camera.main.transform.position.y + 10f);
        startPos = _gameEnemy.position;
    }

    private void MoveEnemy()
    {
        //move to player
        if (Vector2.Distance(transform.position, playerTransform.position) < followRange)
        {
            transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, enemySpeed * Time.fixedDeltaTime);
            enemySpeed = 2;
            following = true;
        }
        //move back to spawn
        else if (following)
        {
            transform.position = Vector2.MoveTowards(transform.position, startPos, enemySpeed * Time.fixedDeltaTime);

            if (Vector2.Distance(transform.position, startPos) <= 0)
            {
                enemySpeed = 3;
                following = false;
            }
        }
    }

    private void Patrol()
    {
        //Keep enemy in bounds of playfield
        if (transform.position.x <= startPos.x - minPos)
        {
            transform.position = new Vector3(Mathf.Min(transform.position.x, startPos.x - minPos), transform.position.y, 0);
            enemySpeed = -enemySpeed;
        }

        if (transform.position.x >= startPos.x + maxPos)
        {
            transform.position = new Vector3(Mathf.Max(transform.position.x, startPos.x + maxPos), transform.position.y, 0);
            enemySpeed = -enemySpeed;
        }
        //patrol select area
        transform.position = new Vector2(transform.position.x + (enemySpeed * Time.fixedDeltaTime), transform.position.y);
    }

    private bool HitPlayer()
    {
        if (Vector2.Distance(transform.position, playerTransform.position) <= 0.5f)
        {
            return true;
        }

        return false;
    }
}
