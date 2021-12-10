using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : PlaneController
{
    #region SERIALIZEFIELD
    [SerializeField]
    private Sprite[] _sprites;
    #endregion

    #region CONST_VALUES
    private const int DROP_PROBABILITY = 6;
    private const int MAX_PROBABILITY = 10;
    private const int MIN_PROBABILITY = 0;
    private const int DEFAULT_SPRITE_INDEX = 0;
    private const int ATTACKED_SPRITE_INDEX = 1;
    private const int SCORE_PER_ENEMY = 10;
    private const float SPRITE_RETURN_TIME = 0.1f;
    #endregion

    private SpriteRenderer _spriteRenderer;
    protected Rigidbody2D _rigidbody;
    protected PlayerController _player;

    public override int HP
    { 
        get => base.HP;
        set => base.HP = value;
    }

    public override void Init()
    {
        _isDead = false;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _attackRate = 2.0f;
        _player = GameManager.Object.Player;
        _bulletPower = 7.0f;
        Attack();
    }

    public override void ApplyDamage(int damage)
    {
        HP -= damage;
        if (HP <= MIN_HP && _isDead == false)
        {
            _isDead = true;
            Die();
        }

        _spriteRenderer.sprite = _sprites[ATTACKED_SPRITE_INDEX];
        Invoke(StringDefines.Strings.ReturnSprite, SPRITE_RETURN_TIME);
    }

    void ReturnSprite()
    {
        _spriteRenderer.sprite = _sprites[DEFAULT_SPRITE_INDEX];
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == StringDefines.Strings.PlayerBulletTag)
        {
            ApplyDamage(_damage);
            BulletController bulletController = collision.gameObject.GetComponent<BulletController>();
            bulletController.Remove();
        }
    }

    private void OnBecameInvisible()
    {
        GameManager.Resource.Destroy(gameObject);
    }

    public override void Die()
    {
        DropItem();
        InGameManager.Game.Score += SCORE_PER_ENEMY;
        GameManager.Resource.Destroy(gameObject);
    }

    protected override void Attack()
    {
        
    }

    private void DropItem()
    {
        int randNum = UnityEngine.Random.Range(MIN_PROBABILITY, MAX_PROBABILITY);
        if (randNum < DROP_PROBABILITY)
        {
            CommonEnums.eItemType type = InGameManager.Game.itemDrop.Get();
            GameManager.Object.AddItem(type, transform.position);
        }
    }

}
