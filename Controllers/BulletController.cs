using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private const float REMOVE_TIME = 3.0f;
    private void OnEnable()
    {
        Invoke(StringDefines.Strings.Remove, REMOVE_TIME);
    }

    public void init()
    {
        Rigidbody2D rigidbody = gameObject.GetComponent<Rigidbody2D>();
        rigidbody.velocity = Vector2.zero;
        rigidbody.angularVelocity = 0.0f;
    }

    public void Remove()
    {
        GameManager.Object.Remove(gameObject);
    }
}
