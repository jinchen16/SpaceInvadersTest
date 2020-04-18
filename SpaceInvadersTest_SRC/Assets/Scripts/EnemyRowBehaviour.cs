using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRowBehaviour : MonoBehaviour
{
    public float Speed;

    public EnemyBehaviour[] enemies;

    public BoxCollider2D[] colliders; // 0: Right collider, 1: Left collider.
    private Vector2 _startColPos1, _startColPos2;

    private Vector3 _rowPosition;
    private Vector3 _startRowPos;
    
    private void Start()
    {
        _startRowPos = transform.position;
        _rowPosition = _startRowPos;
        colliders = GetComponents<BoxCollider2D>();
        _startColPos1 = colliders[0].offset;
        _startColPos2 = colliders[1].offset;        
    }

    public void MoveRow(float dt)
    {
        _rowPosition.x += dt * Speed;
        transform.position = _rowPosition;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("WallBound"))
        {
            _rowPosition.y -= 0.15f;
            if (Speed > 0)
            {
                Speed = -Speed;
            }
            else
            {
                Speed = Mathf.Abs(Speed);
            }            
        }
    }

    // TODO::Improve the organization of the colliders.
    public void ReorderRightCollider()
    {
        Vector2 pos = colliders[0].offset;
        pos.x -= 1;
        colliders[0].offset = pos;
    }

    public void ReorderLeftCollider()
    {
        Vector2 pos = colliders[1].offset;
        pos.x += 1;
        colliders[1].offset = pos;
    }

    public void RestartRowState()
    {
        _rowPosition = _startRowPos;
        transform.position = _rowPosition;
        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].RestartEnemyState();
        }
    }
}
