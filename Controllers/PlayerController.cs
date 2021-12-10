using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : PlaneController
{
    #region CONST_VALUES
    private const int MIN_POWER_LEVEL = 1;
    private const int MAX_POWER_LEVEL = 2;
    private const float MUJEOK_TIME = 3.0f;
    private const float TIME_BETWEEN_DAMAGED = 0.3f;
    #endregion

    private Animator _animator;
    private int _powerLevel = MIN_POWER_LEVEL;
    private bool _isAttackPressed = false;
    private float _lastCollidedTime;

    [HideInInspector]
    public float lastRespawnedTime;

    private bool _isVulnerable
    {
        get
        {
            if (Time.time > _lastCollidedTime + TIME_BETWEEN_DAMAGED
                && Time.time > lastRespawnedTime + MUJEOK_TIME)
                return true;
            else return false;
        }
    }

    public override int HP
    {
        get { return _hp; }
        set
        {
            _hp = Mathf.Clamp(value, MIN_HP, _maxHp);
            InGameMenu.GameMenu.UpdateHealthBar(_hp, _maxHp);
        }
    }

    public int PowerLevel
    {
        get { return _powerLevel; }
        set
        {
            _powerLevel = Mathf.Clamp(value, MIN_POWER_LEVEL, MAX_POWER_LEVEL);
            if(_isAttackPressed == true)
            {
                StopAllCoroutines();
                StartCoroutine(GetAttackCorutineName());
            }
        }
    }

     void Awake()
    {
        Init();
    }

    public override void Init()
    {
        HP = _maxHp;
        _attackRate = 0.3f;
        _animator = GetComponent<Animator>();
        _bulletPower = 10.0f;
    }

    void Update()
    {
        Move();
        Attack();
    }

    public void ResetPlayer()
    {
        HP = _maxHp;
        _isDead = false;
        _powerLevel = MIN_POWER_LEVEL;
    }

    void Move()
    {
        float h = Input.GetAxisRaw(StringDefines.Strings.Horizontal);
        float v = Input.GetAxisRaw(StringDefines.Strings.Vertical);

        Vector3 currentPosition = transform.position;
        currentPosition += new Vector3(h, v, 0) * _speed * Time.deltaTime;
        transform.position = currentPosition;

        UpdateAnimation(h, v);

        //플레이어가 화면 밖으로 나갈 시 위치를 조정
        Vector3 viewPos = Camera.main.WorldToViewportPoint(transform.position);
        viewPos.x = Mathf.Clamp01(viewPos.x);
        viewPos.y = Mathf.Clamp01(viewPos.y);
        Vector3 worldPos = Camera.main.ViewportToWorldPoint(viewPos);

        transform.position = worldPos;
    }

    private void UpdateAnimation(float h, float v)
    {
        if(Input.GetButton(StringDefines.Strings.Horizontal) || Input.GetButtonUp(StringDefines.Strings.Horizontal))
        {
            _animator.SetInteger(StringDefines.Strings.Input, (int)h);
        }
    }

    protected override void Attack()
    {
        string coToRun = GetAttackCorutineName();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            _isAttackPressed = true;
            StartCoroutine(coToRun);
        }
        if(Input.GetKeyUp(KeyCode.Space))
        {
            _isAttackPressed = false;
            StopCoroutine(coToRun);
        }
        if(Input.GetKeyDown(KeyCode.B))
        {
            Boom();
        }
    }

    string GetAttackCorutineName()
    {
        string coToRun;
        switch (PowerLevel){
            case MIN_POWER_LEVEL:
                coToRun = StringDefines.Strings.DefaultAttackCoroutine;
                break;
            case MAX_POWER_LEVEL:
                coToRun = StringDefines.Strings.PowerAttackCoroutine;
                break;
            default:
                coToRun = StringDefines.Strings.DefaultAttackCoroutine;
                break;
        }

        return coToRun;
    }

    private IEnumerator CoAttack()
    {
        while (true)
        {
            GameObject bullet = GameManager.Object.AddBullet(CommonEnums.eBulletType.PlayerBullet, transform.position);
            Rigidbody2D rigidbody = bullet.GetComponent<Rigidbody2D>();
            rigidbody.AddForce(Vector2.up * _bulletPower, ForceMode2D.Impulse);

            yield return new WaitForSeconds(_attackRate);
        }
    }

    private IEnumerator CoPowerAttack()
    {
        while (true)
        {
            GameObject bulletR = GameManager.Object.AddBullet(
                CommonEnums.eBulletType.PlayerBullet,
                transform.position + Vector3.right * BULLET_OFFSET
                );
            GameObject bulletM = GameManager.Object.AddBullet(
                CommonEnums.eBulletType.PlayerBullet,
                transform.position
                );
            GameObject bulletL = GameManager.Object.AddBullet(
                CommonEnums.eBulletType.PlayerBullet,
                transform.position + Vector3.left * BULLET_OFFSET
                );
            
            Rigidbody2D rigidbodyR = bulletR.GetComponent<Rigidbody2D>();
            Rigidbody2D rigidbodyM = bulletM.GetComponent<Rigidbody2D>();
            Rigidbody2D rigidbodyL = bulletL.GetComponent<Rigidbody2D>();
            
            rigidbodyR.AddForce(Vector2.up * _bulletPower, ForceMode2D.Impulse);
            rigidbodyM.AddForce(Vector2.up * _bulletPower, ForceMode2D.Impulse);
            rigidbodyL.AddForce(Vector2.up * _bulletPower, ForceMode2D.Impulse);
           
            yield return new WaitForSeconds(_attackRate);
        }
    }

    private void Boom()
    {
        if (InGameManager.Game.BoomCount == 0) return;
        InGameManager.Game.onBoomEffect();
        GameObject[] Enemies = GameObject.FindGameObjectsWithTag(StringDefines.Strings.EnemyTag);
        GameObject[] Bullets = GameObject.FindGameObjectsWithTag(StringDefines.Strings.EnemyBullet);

        foreach (GameObject enemy in Enemies)
        {
            EnemyController enemyController = enemy.GetComponent<EnemyController>();
            enemyController.Die();
        }

        foreach (GameObject bullet in Bullets)
        {
            GameManager.Object.Remove(bullet);
        }
        InGameManager.Game.offBoomEffect();
        InGameManager.Game.BoomCount -= 1;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == StringDefines.Strings.EnemyBulletTag && _isVulnerable == true)
        {
            ApplyDamage(_damage);
            BulletController bulletController = collision.gameObject.GetComponent<BulletController>();
            bulletController.Remove();
        }   
        else if(collision.gameObject.tag == StringDefines.Strings.EnemyTag && _isVulnerable == true)
        {
            _lastCollidedTime = Time.time;
            ApplyDamage(_damage);
        }
        else if(collision.gameObject.tag == StringDefines.Strings.ItemTag)
        {
            IItem item = collision.gameObject.GetComponent<IItem>();
            item.Get();
        }
    }

    public override void ApplyDamage(int damage)
    {
        HP -= damage;
        if (HP == MIN_HP && _isDead == false)
        {
            _isDead = true;
            Die();
        }
    }

    public override void Die()
    {
        InGameManager.Game.HandleDeath();
    }
}
