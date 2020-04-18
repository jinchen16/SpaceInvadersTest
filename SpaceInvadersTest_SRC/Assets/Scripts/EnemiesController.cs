using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesController : MonoBehaviour
{
    public EnemyRowBehaviour[] enemyRows;

    public static EnemiesController instance;

    public int enemyCounter;
    [SerializeField]
    private int _totalEnemies;

    private void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (GameManager.instance.IsPlaying())
        {
            MoveEnemyRows(Time.fixedDeltaTime);
        }
    }

    private void MoveEnemyRows(float dt)
    {
        for (int i = 0; i < enemyRows.Length; i++)
        {
            enemyRows[i].MoveRow(dt);
        }
    }

    public bool IsAllDead()
    {
        return enemyCounter == _totalEnemies;
    }

    public void RestartEnemies()
    {
        enemyCounter = 0;
        for (int i = 0; i < enemyRows.Length; i++)
        {
            enemyRows[i].RestartRowState();
        }
    }
}
