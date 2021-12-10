using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlaneController : MonoBehaviour
{
    protected int _hp;
    protected int _maxHp = 100;
    protected int _damage = 30;
    protected float _speed = 5.0f;
    protected float _attackRate;
    protected float _bulletPower;
    protected bool _isDead = false;

    #region CONST_VALUES
    protected const int MIN_HP = 0;
    protected const float BULLET_OFFSET = 0.3f;
    #endregion

    public virtual int HP { get; set; }
    public int MAXHP { get => _maxHp; set => _maxHp = value; }

    public abstract void Init();

    public abstract void ApplyDamage(int damage);

    public abstract void Die();

    protected abstract void Attack();

}
