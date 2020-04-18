using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType
{
    SIMPLE,
    MEDIUM,
    HIGH,
    SIDE
}

public class EnemyBehaviour : MonoBehaviour
{
    public float ShootTime;
    public EnemyType enemyType;

    public int posID;

    public bool IsDead;

    [SerializeField]
    private Transform _bulletSpawner;
    private bool _hasShoot;

    public void Shoot()
    {
        if (!_hasShoot)
        {
            StartCoroutine(ShootRoutine());
        }
    }

    /// <summary>
    /// Coroutine to control Shooting state
    /// </summary>
    /// <returns></returns>
    private IEnumerator ShootRoutine()
    {
        _hasShoot = true;
        ObjectPoolerManager.instance.GetPoolableObjectById("EnemyBullet", _bulletSpawner.position, Quaternion.identity);
        yield return new WaitForSeconds(ShootTime);
        _hasShoot = false;
    }

    public void ReceiveShoot()
    {
        if(enemyType != EnemyType.SIDE)
            EnemiesController.instance.enemyCounter++;

        gameObject.SetActive(false);
        IsDead = true;        
        GameManager.instance.UpdateScoreCounting(this);
    }

    public void RestartEnemyState()
    {
        gameObject.SetActive(true);
        _hasShoot = false;
        IsDead = false;
    }
}
