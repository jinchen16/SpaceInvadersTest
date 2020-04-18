using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    OnStateStart,
    OnStatePlay,
    OnStateReload,
    OnStateEnd
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int hardEnemyPoints, mediumEnemyPoints, simpleEnemyPoints, sideEnemyPoints;

    public int totalScore;

    public Transform leftPosition, rightPosition;

    public int lives = 3;

    private float _sideEnemyTimer;
    [SerializeField]
    private float _sideEnemyTime; // Time limit for the side enemy to appear. // TODO::This can be set as a random value.
    private SideEnemyMovement _sideEnemy;
    private bool _isSideEnemy;

    [SerializeField]
    private GameState _currentState;

    [SerializeField]
    private PlayerController _playerCtrl;

    private bool _isPlayerDead;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        ChangeState(GameState.OnStateStart);
    }

    private void Update()
    {
        if (_currentState == GameState.OnStatePlay)
        {
            SideEnemyBehaviour();
        }
    }

    #region State functions
    public void ChangeState(GameState state)
    {
        _currentState = state;
        switch (state)
        {
            case GameState.OnStateStart:
                StartCoroutine(OnStateStart());
                break;
            case GameState.OnStatePlay:
                StartCoroutine(OnStatePlay());
                break;
            case GameState.OnStateReload:
                StartCoroutine(OnStateReload());
                break;
            case GameState.OnStateEnd:
                StartCoroutine(OnStateEnd());
                break;
            default:
                break;
        }
    }

    private IEnumerator OnStateStart()
    {
        _isPlayerDead = false;
        LivesPanelManager.instance.ShowAllLives();
        lives = 3;
        ChangeState(GameState.OnStatePlay);
        yield return null;
    }

    private IEnumerator OnStatePlay()
    {
        yield return null;
    }

    private IEnumerator OnStateReload()
    {
        EnemiesController.instance.RestartEnemies();
        RestartSideEnemy();
        ChangeState(GameState.OnStatePlay);
        yield return null;
    }

    private IEnumerator OnStateEnd()
    {
        int highest = PlayerDataManager.instance.GetScore();
        if (totalScore > highest)
        {
            highest = totalScore;
            PlayerDataManager.instance.SetScore(totalScore);
        }
        FinalScorePanelManager.instance.UpdateText(highest, totalScore);
        FinalScorePanelManager.instance.Show();
        yield return null;
    }

    #endregion

    public void UpdateScoreCounting(EnemyBehaviour enemy)
    {
        switch (enemy.enemyType)
        {
            case EnemyType.SIMPLE:
                totalScore += simpleEnemyPoints;
                break;
            case EnemyType.MEDIUM:
                totalScore += mediumEnemyPoints;
                break;
            case EnemyType.HIGH:
                totalScore += hardEnemyPoints;
                break;
            case EnemyType.SIDE:
                OnSideEnemyKilled();
                totalScore += sideEnemyPoints;
                break;
            default:
                break;
        }
        ScorePanelManager.instance.UpdateScoreText(totalScore);

        if (EnemiesController.instance.IsAllDead())
        {
            ChangeState(GameState.OnStateEnd);
        }
    }

    public void ResetScore()
    {
        totalScore = 0;
        ScorePanelManager.instance.UpdateScoreText(totalScore);
    }

    public void CreateSideEnemy()
    {
        int side = Random.Range(0, 2);
        GameObject sideEnemyObj = ObjectPoolerManager.instance.GetPoolableObjectById("SideEnemy");
        _sideEnemy = sideEnemyObj.GetComponent<SideEnemyMovement>();
        if (_sideEnemy != null)
        {
            if (side == 0)
            {
                _sideEnemy.StartEnemyData(Side.LEFT, leftPosition.position);
            }
            else
            {
                _sideEnemy.StartEnemyData(Side.RIGHT, rightPosition.position);
            }
        }
    }

    private void RestartSideEnemy()
    {
        if(_sideEnemy != null)
        {
            ObjectPoolerManager.instance.HideObject(_sideEnemy.gameObject);
            OnSideEnemyKilled();            
        }
    }

    private void OnSideEnemyKilled()
    {
        _isSideEnemy = false;
        _sideEnemy = null;
    }

    public void OnPlayerKilled()
    {
        if (!_isPlayerDead)
        {
            _isPlayerDead = true;
            lives--;
            LivesPanelManager.instance.LoseALive(lives);
            if (lives <= 0)
            {
                ChangeState(GameState.OnStateEnd);
            }
            else
            {
                ChangeState(GameState.OnStateReload);
            }
        }
    }

    public bool IsPlaying()
    {
        return _currentState == GameState.OnStatePlay;
    }

    private void SideEnemyBehaviour()
    {
        if (!_isSideEnemy)
        {
            _sideEnemyTimer += Time.deltaTime;
            if (_sideEnemyTimer >= _sideEnemyTime)
            {
                CreateSideEnemy();
                _isSideEnemy = true;
                _sideEnemyTimer = 0;
            }
        }
        else
        {
            if (_sideEnemy != null)
            {
                _sideEnemy.MoveEnemy(Time.deltaTime);
                if (_sideEnemy.IsOutOfBounds())
                {
                    ObjectPoolerManager.instance.HideObject(_sideEnemy.gameObject);
                    _isSideEnemy = false;
                    _sideEnemy = null;
                }
            }
        }
    }

    public void OnRestartingGame()
    {
        _playerCtrl.ResetPlayer();
        RestartSideEnemy();
        EnemiesController.instance.RestartEnemies();
        ChangeState(GameState.OnStateStart);
    }
}
