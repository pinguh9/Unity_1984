using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySmall : EnemyController
{
    public override void Init()
    {
        base.Init();
        _maxHp = 50;
        HP = _maxHp;
        _speed = 5.0f;
        _rigidbody.velocity = Vector2.down * _speed;
    }

    protected override void Attack()
    {
        base.Attack();
        StartCoroutine(StringDefines.Strings.DefaultAttackCoroutine);
    }

    private IEnumerator CoAttack()
    {
        while (true)
        {
            GameObject Bullet = GameManager.Object.AddBullet(CommonEnums.eBulletType.EnemyBullet, transform.position);
            Rigidbody2D rigid = Bullet.GetComponent<Rigidbody2D>();
            Vector3 dirVec = _player.transform.position - transform.position;

            rigid.AddForce(dirVec.normalized * _bulletPower, ForceMode2D.Impulse);

            yield return new WaitForSeconds(_attackRate);
        }
    }


}
