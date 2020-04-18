using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Side
{
    LEFT = 0,
    RIGHT = 1
}

public class SideEnemyMovement : EnemyBehaviour
{
    public float Speed;

    [SerializeField]
    private float _speed;
    private Vector3 _position;
    [SerializeField]
    private Side _side;

    public void MoveEnemy(float dt)
    {
        _position.x += dt * _speed;
        transform.position = _position;
    }

    public bool IsOutOfBounds()
    {
        if (_side == Side.LEFT)
        {
            if (_position.x >= BoundsController.instance.GetOuterMaxLimitX())
                return true;
        }
        else
        {
            if (_position.x <= BoundsController.instance.GetOuterMinLimitX())
                return true;
        }
        return false;
    }

    public void StartEnemyData(Side side, Vector3 pos)
    {
        _position = pos;
        transform.position = _position;
        _side = side;
        if (side == Side.LEFT)
        {
            _speed = Speed;

        }
        else
        {
            _speed = -Speed;
        }
    }
}
