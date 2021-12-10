using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager 
{
    public T Load<T>(string path) where T : Object
    {
        if (typeof(T) == typeof(GameObject))
        {
            string name = path;
            int index = name.LastIndexOf('/');
            if (index >= 0)
                name = name.Substring(index + 1);

            GameObject gameObject = GameManager.Pool.GetOriginal(name);
            if (gameObject != null)
                return gameObject as T;
        }

        return Resources.Load<T>(path);
    }

    public GameObject Instantiate(string path, Transform parent = null)
    {
        GameObject original = Load<GameObject>($"{StringDefines.Strings.PrefabPath}{path}");
        if (original == null)
        {
            Debug.LogWarning($"{path} {StringDefines.Strings.NoPrefab}");
            return null;
        }

        if (original.GetComponent<Poolable>() != null)
            return GameManager.Pool.Pop(original, parent).gameObject;

        GameObject gameObject = Object.Instantiate(original, parent);
        gameObject.name = original.name;
        return gameObject;
    }

    public void Destroy(GameObject gameObject)
    {
        if (gameObject == null)
            return;

        Poolable poolable = gameObject.GetComponent<Poolable>();
        if (poolable != null)
        {
            GameManager.Pool.Push(poolable);
            return;
        }

        Object.Destroy(gameObject);
    }
}
