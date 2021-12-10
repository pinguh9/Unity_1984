using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
/// <summary>
/// 인게임 매니저 
/// </summary>
public class InGameManager : MonoBehaviour
{
    private static InGameManager _game;

    public static InGameManager Game
    {
        get
        {
            if (_game == null) _game = FindObjectOfType<InGameManager>();
            return _game;
        }
    }

    #region CONST_VALUES
    private const int LIFE_MAX = 3;
    private const int BOOM_MAX = 3;
    private const int ENEMY_TYPE_COUNT = 3;
    private const int SCORE_MAX = 999999;
    private const int MIN_AMOUNT = 0;
    private const int AMOUNT_PER_ONCE = 1;
    private const int ENEMY_SMALL = 0;
    private const int ENEMY_MIDDLE = 1;
    private const int ENEMY_BIG = 2;
    private const float TIME_BEFORE_SPAWN = 3.0f;
    private const float TIME_BETWEEN_SPAWN = 4.0f;
    private const float REVIVE_TIME = 3.0f;
    private const float BOOM_PROBABILITY = 10.0f;
    private const float COIN_PROBABILITY = 60.0f;
    private const float POWER_PROBABILITY = 30.0f;
    private const float BOOM_EFFECT_TIME = 2.0f;
    #endregion

    [HideInInspector]
    public Vector3 PlayerStartPos = new Vector3(0, -4, 0);
    private int _life = LIFE_MAX;
    private int _coin = MIN_AMOUNT;
    private int _score = MIN_AMOUNT;
    private int _boomCount = MIN_AMOUNT;

    [SerializeField]
    private Transform[] _spawnPoints;
    [SerializeField]
    private GameObject _boomEffect;
    [HideInInspector]
    public PlayerController player { get; set; }
    [HideInInspector]
    public WeightedDrop<CommonEnums.eItemType> itemDrop = new WeightedDrop<CommonEnums.eItemType>();
    [HideInInspector]
    public event Action OnPlayerDead;

    public void onBoomEffect() => _boomEffect.SetActive(true);
    public void offBoomEffect() => Invoke(StringDefines.Strings.DeactiveBoomEffect, BOOM_EFFECT_TIME);

    public int BoomCount
    {
        get { return _boomCount; }
        set
        {
            if (value > _boomCount)
            {
                _boomCount = Mathf.Clamp(value, MIN_AMOUNT, BOOM_MAX);
                InGameMenu.GameMenu.AddBoom();
            }
            else
            {
                InGameMenu.GameMenu.RemoveBoom();
                _boomCount = Mathf.Clamp(value, MIN_AMOUNT, BOOM_MAX);
            }
        }
    }

    public int Score
    {
        get { return _score; }
        set
        {
            _score = Mathf.Clamp(value, MIN_AMOUNT, SCORE_MAX);
            InGameMenu.GameMenu.UpdateScore();
        }
    }

    public int Coin
    {
        get { return _coin; }
        set { _coin = value; }
    }

    public int Life
    {
        get { return _life; }
        set { _life = Mathf.Clamp(value, MIN_AMOUNT, LIFE_MAX); }
    } 

    public void Init()
    {
        GameManager.Object.AddPlayer(GameManager.Data.PlaneType);
        player = GameManager.Object.Player;
        player.lastRespawnedTime = Time.time;

        itemDrop.Add(CommonEnums.eItemType.Boom, BOOM_PROBABILITY);
        itemDrop.Add(CommonEnums.eItemType.Coin, COIN_PROBABILITY);
        itemDrop.Add(CommonEnums.eItemType.Power, POWER_PROBABILITY);
        
        InGameMenu.GameMenu.UpdateHealthBar(player.HP, player.MAXHP);
        InGameMenu.GameMenu.UpdateScore();

        Invoke(StringDefines.Strings.StartSpawn, TIME_BEFORE_SPAWN);
    }

    private void Start()
    {
        Init();
    }

    private void StartSpawn()
    {
        StartCoroutine(StringDefines.Strings.SpawnEnemy);
    }

    private IEnumerator SpawnEnemy()
    {
        while (true)
        {
            Transform spawnPoint = _spawnPoints[UnityEngine.Random.Range(MIN_AMOUNT, _spawnPoints.Length)];
            int EnemyType = UnityEngine.Random.Range(MIN_AMOUNT, ENEMY_TYPE_COUNT);

            switch (EnemyType)
            {
                case ENEMY_SMALL:
                    GameManager.Object.AddEnemy(CommonEnums.eEnemyType.EnemySmall, spawnPoint.position);
                    break;
                case ENEMY_MIDDLE:
                    GameManager.Object.AddEnemy(CommonEnums.eEnemyType.EnemyMiddle, spawnPoint.position);
                    break;
                case ENEMY_BIG:
                    GameManager.Object.AddEnemy(CommonEnums.eEnemyType.EnemyBig, spawnPoint.position);
                    break;
            }

            yield return new WaitForSeconds(TIME_BETWEEN_SPAWN);
        }
    }

    public void HandleDeath()
    {
        if (Life > MIN_AMOUNT)
        {
            Life -= AMOUNT_PER_ONCE;

            if (OnPlayerDead != null) OnPlayerDead();

            player.gameObject.SetActive(false);
            if (Life == MIN_AMOUNT) GameOver();
            else
            {
                Invoke(StringDefines.Strings.RespawnPlayer, REVIVE_TIME);
            }
        }
    }

    public void RespawnPlayer()
    {
        player.lastRespawnedTime = Time.time;
        player.gameObject.transform.position = PlayerStartPos;
        player.gameObject.SetActive(true);
        player.ResetPlayer();
    }

    public void GameOver()
    {
        StopAllCoroutines();
        InGameMenu.GameMenu.SetGameOverMenu();
    }

    void DeactiveBoomEffect()
    {
        _boomEffect.SetActive(false);
    }
}
