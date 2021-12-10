using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBase : MonoBehaviour, IItem
{

    private Rigidbody2D _rigidbody;

    #region CONST_VALUES
    protected const int AMOUNT_PER_ONCE = 1;
    private const float SPEED = 1.2f;
    #endregion

    public void Remove() => GameManager.Resource.Destroy(gameObject);

    public virtual void Get()
    {
        Remove();
    }

    // Start is called before the first frame update
    void Awake()
    {
        Init();
    }

    void Init()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _rigidbody.velocity = Vector2.down * SPEED;
    }

    private void OnBecameInvisible()
    {
        Remove();
    }
}
