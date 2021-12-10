using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StringDefines
{
    public static readonly StringDefines Strings = new StringDefines();

    public const int num = 3;

    public readonly string[] PoolablePaths = { 
        "Prefabs/Planes/EnemyBig",
        "Prefabs/Planes/EnemyMiddle",
        "Prefabs/Planes/EnemySmall",
        "Prefabs/Bullets/EnemyBullet",
        "Prefabs/Bullets/PlayerBullet"
    };

    #region RESOURCE_PATHS
    public readonly string PrefabPath = "Prefabs/";
    public readonly string DefaultPlayerPrefab = "Planes/Player";
    public readonly string HeartPlayerPrefab = "Planes/Player_heart";
    public readonly string EnemyBigPrefab = "Planes/EnemyBig";
    public readonly string EnemyMiddlePreafab = "Planes/EnemyMiddle";
    public readonly string EnemySmallPrefab = "Planes/EnemySmall";
    public readonly string EnemyBulletPrefab = "Bullets/EnemyBullet";
    public readonly string PlayerBulletPrefab = "Bullets/PlayerBullet";
    public readonly string BoomPrefab = "Items/Boom";
    public readonly string CoinPrefab = "Items/Coin";
    public readonly string PowerPrefab = "Items/Power";
    #endregion

    #region OBJECT_NAMES
    public readonly string Player = "Player";
    public readonly string EnemySmall = "EnemySmall";
    public readonly string EnemyMiddle = "EnemyMiddle";
    public readonly string EnemyBig = "EnemyBig";
    public readonly string EnemyBullet = "EnemyBullet";
    public readonly string PlayerBullet = "PlayerBullet";
    public readonly string Managers = "@Managers";
    public readonly string PoolRoot = "@PoolRoot";
    public readonly string Root = "Root";
    #endregion

    #region ITEM_NAMES
    public readonly string Boom = "Boom";
    public readonly string Coin = "Coin";
    public readonly string Power = "Power";
    #endregion

    #region TAG_NAMES
    public readonly string PlayerBulletTag = "PlayerBullet";
    public readonly string EnemyTag = "Enemy";
    public readonly string EnemyBulletTag = "EnemyBullet";
    public readonly string PlayerTag = "Player";
    public readonly string ItemTag = "Item";
    #endregion

    #region SCENE_NAMES
    public readonly string BaseScene = "BaseScene";
    public readonly string InGameScene = "InGame";
    public readonly string MainMenuScene = "MainMenu";
    public readonly string ShopScene = "Shop";
    #endregion

    #region WARNINGS
    public readonly string NoPrefab = "해당 프리팹이 폴더에 없음";
    public readonly string NotPoolable = "Pool 가능한 오브젝트가 아님.";
    #endregion

    #region FUNCTION_NAMES
    public readonly string DefaultAttackCoroutine = "CoAttack";
    public readonly string PowerAttackCoroutine = "CoPowerAttack";
    public readonly string ReturnSprite = "ReturnSprite";
    public readonly string SpawnEnemy = "SpawnEnemy";
    public readonly string StartSpawn = "StartSpawn";
    public readonly string DeactiveBoomEffect = "DeactiveBoomEffect";
    public readonly string RespawnPlayer = "RespawnPlayer";
    public readonly string Remove = "Remove";
    #endregion

    #region AXIS_NAME
    public readonly string Horizontal = "Horizontal";
    public readonly string Vertical = "Vertical";
    #endregion

    #region ANIMATOR_PARAMETER
    public readonly string Input = "Input";
    #endregion

    #region UI_STRINGS
    public readonly string NotEnoughMoney = "소지금이 부족합니다";
    public readonly string BestScoreText = "최고 점수 : ";
    public readonly string MoneyAmount = "소지금 : ";
    public readonly string GainedCoin = "획득한 코인 : ";
    #endregion

    #region BUTTON_HANDLING
    public readonly string ButtonGruop = "Items";
    public readonly string CallBackFunctionName = "OnMsgButtonList";
    public readonly string ButtonMsg = "OnButtonMsg";
    #endregion
}
