using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 총괄 매니저 클래스
/// </summary>
public class GameManager : MonoBehaviour
{
    static GameManager m_instance;
    static GameManager Instance { get { Init(); return m_instance; } }

    SceneManager_SM _scene = new SceneManager_SM();
    ObjectManager _object = new ObjectManager();
    PoolManager _pool = new PoolManager();
    ResourceManager _resource = new ResourceManager();
    DataManager _data = new DataManager();

    public static SceneManager_SM Scene { get { return Instance._scene; } }
    public static ObjectManager Object { get { return Instance._object; } }
    public static PoolManager Pool { get { return Instance._pool; } }
    public static ResourceManager Resource { get { return Instance._resource; } }
    public static DataManager Data { get { return Instance._data; } }

    // Start is called before the first frame update
    private void Start()
    {
        Init();
    }

    private static void Init()
    {
        if(m_instance == null)
        {
            GameObject ManagerObject = GameObject.Find(StringDefines.Strings.Managers);

            if(ManagerObject == null)
            {
                ManagerObject = new GameObject { name = StringDefines.Strings.Managers };
                ManagerObject.AddComponent<GameManager>();
            }

            DontDestroyOnLoad(ManagerObject);
            m_instance = ManagerObject.GetComponent<GameManager>();

            m_instance._pool.Init();
            m_instance._data.Init();
        }
    }

    public static void Clear()
    {
        GameManager.Pool.Clear();
    }

    private void OnApplicationQuit()
    {
        Clear();
    }
}
