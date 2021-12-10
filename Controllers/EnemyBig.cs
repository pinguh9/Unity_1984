using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBig : EnemyController
{
    public override void Init()
    {
        base.Init();
        _maxHp = 200;
        HP = _maxHp;
        _speed = 2.0f;
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
            GameObject BulletR = GameManager.Object.AddBullet(
                CommonEnums.eBulletType.EnemyBullet,
                transform.position + Vector3.right * BULLET_OFFSET
                );
            GameObject BulletM = GameManager.Object.AddBullet(
                CommonEnums.eBulletType.EnemyBullet,
                transform.position
                );
            GameObject BulletL = GameManager.Object.AddBullet(
                CommonEnums.eBulletType.EnemyBullet,
                transform.position + Vector3.left * BULLET_OFFSET
                );

            Rigidbody2D rigidR = BulletR.GetComponent<Rigidbody2D>();
            Rigidbody2D rigidM = BulletM.GetComponent<Rigidbody2D>();
            Rigidbody2D rigidL = BulletL.GetComponent<Rigidbody2D>();

            Vector3 dirVecR = _player.transform.position - (transform.position + Vector3.right * BULLET_OFFSET);
            Vector3 dirVecM = _player.transform.position - (transform.position);
            Vector3 dirVecL = _player.transform.position - (transform.position + Vector3.left * BULLET_OFFSET);

            rigidL.AddForce(dirVecL.normalized * _bulletPower, ForceMode2D.Impulse);
            rigidM.AddForce(dirVecM.normalized * _bulletPower, ForceMode2D.Impulse);
            rigidR.AddForce(dirVecR.normalized * _bulletPower, ForceMode2D.Impulse);

            yield return new WaitForSeconds(_attackRate);
        }
    }

}
