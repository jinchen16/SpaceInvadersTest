using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float Speed;
    public float ShootTime = 3f;
    public GameObject BulletPrefab;

    [SerializeField]
    private Transform _bullSpawnerTrans;

    [SerializeField]
    private AudioSource _audioSource;

    private bool _hasShoot;

    private Rigidbody2D _playerRB;

    private Vector3 _startPos;

    // Start is called before the first frame update
    void Start()
    {
        _startPos = transform.position;
        _playerRB = GetComponent<Rigidbody2D>();                
        _hasShoot = false;        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MovePlayer();
    }

    private void Update()
    {
        Shoot();
    }

    /// <summary>
    /// Moving the player through basic physic component
    /// </summary>
    private void MovePlayer()
    {
        if (GameManager.instance.IsPlaying())
        {
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
            {
                float h = Input.GetAxis("Horizontal");
                Vector2 pos = transform.position;
                pos.x += h * Time.fixedDeltaTime * Speed;

                if (pos.x >= BoundsController.instance.GetMinLimitX() && pos.x <= BoundsController.instance.GetMaxLimitX())
                {
                    transform.position = pos;
                }
            }
        }        
    }

    /// <summary>
    /// Shoot behaviour
    /// </summary>
    private void Shoot()
    {
        if (GameManager.instance.IsPlaying())
        {
            if (Input.GetButton("Fire1"))
            {
                if (!_hasShoot)
                {
                    _audioSource.Play();
                    StartCoroutine(ShootRoutine());
                }
            }
        }
    }

    /// <summary>
    /// Coroutine to control Shooting state
    /// </summary>
    /// <returns></returns>
    private IEnumerator ShootRoutine()
    {
        _hasShoot = true;
        ObjectPoolerManager.instance.GetPoolableObjectById("PlayerBullet", _bullSpawnerTrans.position, Quaternion.identity);        
        yield return new WaitForSeconds(ShootTime);
        _hasShoot = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            GameManager.instance.OnPlayerKilled();
        }
    }

    public void ResetPlayer()
    {
        transform.position = _startPos;
    }
}
