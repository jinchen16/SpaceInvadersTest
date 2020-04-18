using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BullType
{
    PLAYER,
    ENEMY,
    NONE
}

public class BulletBehaviour : MonoBehaviour, IPoolComponent
{
    public float Speed;
    public BullType bulletType;

    private Vector3 _bulletPos;

    // Start is called before the first frame update
    void Start()
    {
        _bulletPos = transform.position;
    }

    private void FixedUpdate()
    {
        _bulletPos.y += Time.fixedDeltaTime * Speed;
        transform.position = _bulletPos;
    }

    public void SetStartBulletPos(Vector3 pos)
    {
        _bulletPos = pos;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bound"))
        {
            ObjectPoolerManager.instance.HideObject(gameObject);
        }
        else if (other.CompareTag("Enemy") && bulletType == BullType.PLAYER)
        {
            ObjectPoolerManager.instance.HideObject(gameObject);
            EnemyBehaviour enemyBehaviour = other.GetComponent<EnemyBehaviour>();
            if (enemyBehaviour != null)
            {
                enemyBehaviour.ReceiveShoot();                
            }
        }
        else if (other.CompareTag("Player") && bulletType == BullType.ENEMY)
        {
            ObjectPoolerManager.instance.HideObject(gameObject);            
            GameManager.instance.OnPlayerKilled();
        }
    }

    public void SetPositionRotation(Vector3 position, Quaternion quaternion)
    {
        _bulletPos = position;
        transform.SetPositionAndRotation(position, quaternion);
    }
}
