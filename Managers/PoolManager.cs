using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager
{
    private const int DEFAULT_POOl_COUNT = 10;

    class Pool
    {
        public GameObject gameObject { get; private set; }
        public Transform Root { get; set; }
        Stack<Poolable> _stack = new Stack<Poolable>();

        public void Init(GameObject obj)
        {
            gameObject = obj;
            Root = new GameObject().transform;
            Root.name = $"{obj.name}{StringDefines.Strings.Root}";
        }

        public Poolable Create()
        {
            GameObject go = UnityEngine.Object.Instantiate<GameObject>(gameObject);
            go.name = gameObject.name;
            return go.GetComponent<Poolable>();
        }
        
        public void Push(Poolable poolable)
        {
            if (poolable == null) return;

            poolable.transform.parent = Root;
            poolable.gameObject.SetActive(false);
            poolable.isUsing = false;

            _stack.Push(poolable);
        }

        public Poolable Pop(Transform parent)
        {
            Poolable poolable;

            if (_stack.Count > 0)
                poolable = _stack.Pop();
            else
                poolable = Create();

            poolable.gameObject.SetActive(true);
            poolable.transform.parent = parent;
            poolable.isUsing = true;

            return poolable;
        }
    }

    Dictionary<string, Pool> _pool = new Dictionary<string, Pool>();
    Transform _root;

    public void Init()
    {
        if(_root == null)
        {
            _root = new GameObject { name = StringDefines.Strings.PoolRoot }.transform;
            UnityEngine.Object.DontDestroyOnLoad(_root);
            
            for (int i = 0; i < StringDefines.Strings.PoolablePaths.Length; i++)
            {
                string path = StringDefines.Strings.PoolablePaths[i];
                string name = path;
                int index = path.LastIndexOf('/');

                if (index >= 0)
                    name = name.Substring(index + 1);

                GameObject original = Resources.Load<GameObject>(path);
                CreatePool(original);

                for (int j = 0; j < DEFAULT_POOl_COUNT; j++)
                {
                    GameObject gameObject = UnityEngine.Object.Instantiate(original, _pool[name].Root);
                    gameObject.name = name;

                    Poolable poolable = gameObject.GetComponent<Poolable>();
                    Push(poolable);
                }
            }
        }
    }

  
    public void CreatePool(GameObject gameObject)
    {
        Pool pool = new Pool();
        pool.Init(gameObject);
        pool.Root.parent = _root;

        _pool.Add(gameObject.name, pool);
    }

    public void Push(Poolable poolable)
    {
        string name = poolable.gameObject.name;
        if(_pool.ContainsKey(name) == false)
        {
            GameObject.Destroy(poolable.gameObject);
            return;
        }

        _pool[name].Push(poolable);
    }

    public Poolable Pop(GameObject gameObject, Transform parent = null)
    {
        if (_pool.ContainsKey(gameObject.name) == false)
            CreatePool(gameObject);

        return _pool[gameObject.name].Pop(parent);
    }

    public GameObject GetOriginal(string name)
    {
        if (_pool.ContainsKey(name) == false)
            return null;
        return _pool[name].gameObject;
    }

    public void Clear()
    {
        foreach (Transform child in _root)
            GameObject.Destroy(child.gameObject);

        _pool.Clear();
    }

}
