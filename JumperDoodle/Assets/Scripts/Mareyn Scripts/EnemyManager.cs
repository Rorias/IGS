using System.Collections.Generic;
using UnityEngine;

public class EnemyManager
{
    private List<GameObject> gameEnemies = new List<GameObject>();
    private List<Enemy> enemies = new List<Enemy>();

    public void CreateEnemy(GameObject _enemy, Transform _playerTransform)
    {
        GameObject enemy = _enemy;
        gameEnemies.Add(enemy);
        enemies.Add(new Enemy(_playerTransform, enemy.transform));
    }

    public void UpdateEnemies()
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            enemies[i].UpdateEnemy();

            if (gameEnemies[i].transform.position.y - Camera.main.transform.position.y < -5.5f)
            {
                enemies[i].ResetEnemy(gameEnemies[i].transform);
            }
        }
    }
}
