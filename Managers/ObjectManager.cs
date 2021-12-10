using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 오브젝트 별로 생성 삭제 관리
/// </summary>
public class ObjectManager
{
    public PlayerController Player { get; set; }
   
    public void AddPlayer(CommonEnums.ePlaneType type)
    {
        if(type == CommonEnums.ePlaneType.CommonPlane)
        {
            GameObject gameObject = GameManager.Resource.Instantiate(StringDefines.Strings.DefaultPlayerPrefab);
            gameObject.name = StringDefines.Strings.Player;
            gameObject.transform.position = InGameManager.Game.PlayerStartPos;

            Player = gameObject.GetComponent<PlayerController>();
        }
        else if(type == CommonEnums.ePlaneType.UnCommonPlane)
        {
            GameObject gameObject = GameManager.Resource.Instantiate(StringDefines.Strings.HeartPlayerPrefab);
            gameObject.name = StringDefines.Strings.Player;
            gameObject.transform.position = InGameManager.Game.PlayerStartPos;

            Player = gameObject.GetComponent<PlayerController>();
        }
    }

    public void AddEnemy(CommonEnums.eEnemyType type, Vector3 pos)
    {
        if(type == CommonEnums.eEnemyType.EnemyBig)
        {
            GameObject gameObject = GameManager.Resource.Instantiate(StringDefines.Strings.EnemyBigPrefab);
            gameObject.name = StringDefines.Strings.EnemyBig;
            gameObject.transform.position = pos;
            
            EnemyController EC = gameObject.GetComponent<EnemyController>();
            EC.Init();
        }
        else if(type == CommonEnums.eEnemyType.EnemyMiddle)
        {
            GameObject gameObject = GameManager.Resource.Instantiate(StringDefines.Strings.EnemyMiddlePreafab);
            gameObject.name = StringDefines.Strings.EnemyMiddle;
            gameObject.transform.position = pos;

            EnemyController EC = gameObject.GetComponent<EnemyController>();
            EC.Init();
        }
        else if(type == CommonEnums.eEnemyType.EnemySmall)
        {
            GameObject gameObject = GameManager.Resource.Instantiate(StringDefines.Strings.EnemySmallPrefab);
            gameObject.name = StringDefines.Strings.EnemySmall;
            gameObject.transform.position = pos;

            EnemyController EC = gameObject.GetComponent<EnemyController>();
            EC.Init();
        }
    }

    public GameObject AddBullet(CommonEnums.eBulletType type, Vector3 pos)
    {
        if(type == CommonEnums.eBulletType.EnemyBullet)
        {
            GameObject gameObject = GameManager.Resource.Instantiate(StringDefines.Strings.EnemyBulletPrefab);
            gameObject.name = StringDefines.Strings.EnemyBullet;
            gameObject.transform.position = pos;

            BulletController bulletController = gameObject.GetComponent<BulletController>();
            bulletController.init();

            return gameObject;
        }
        else if(type == CommonEnums.eBulletType.PlayerBullet)
        {
            GameObject gameObject = GameManager.Resource.Instantiate(StringDefines.Strings.PlayerBulletPrefab);
            gameObject.name = StringDefines.Strings.PlayerBullet;
            gameObject.transform.position = pos;

            BulletController bulletController = gameObject.GetComponent<BulletController>();
            bulletController.init();

            return gameObject;
        }
        else
        {   
            return null;
        }
    }

    public void AddItem(CommonEnums.eItemType type, Vector3 pos)
    {
        if(type == CommonEnums.eItemType.Boom)
        {
            GameObject gameObject = GameManager.Resource.Instantiate(StringDefines.Strings.BoomPrefab);
            gameObject.name = StringDefines.Strings.Boom;
            gameObject.transform.position = pos;
        }
        else if(type == CommonEnums.eItemType.Coin)
        {
            GameObject gameObject = GameManager.Resource.Instantiate(StringDefines.Strings.CoinPrefab);
            gameObject.name = StringDefines.Strings.Coin;
            gameObject.transform.position = pos;
        }
        else if(type == CommonEnums.eItemType.Power)
        {
            GameObject gameObject = GameManager.Resource.Instantiate(StringDefines.Strings.PowerPrefab);
            gameObject.name = StringDefines.Strings.Power;
            gameObject.transform.position = pos;
        }
    }

    public void Remove(GameObject gameObject)
    {
        GameManager.Resource.Destroy(gameObject);
    }

}
