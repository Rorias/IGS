using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager
{
    private List<GameObject> gameEnemies = new List<GameObject>();
    private List<Enemy> enemies = new List<Enemy>();

    public void CreateEnemy(GameObject _enemy, Transform _playerT)
    {
        GameObject enemy = _enemy;
        gameEnemies.Add(enemy);
        enemies.Add(new Enemy(_playerT, enemy.transform));
    }

    public void UpdateEnemies()
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            enemies[i].UpdateEnemy();

            if (gameEnemies[i].transform.position.y - Camera.main.transform.position.y < -5.5f)
            {
                gameEnemies[i].transform.position = new Vector2(Random.Range(-8, 9), Camera.main.transform.position.y + 10f);
                enemies[i].currentpos = gameEnemies[i].transform.position;
            }
        }
    }
}
