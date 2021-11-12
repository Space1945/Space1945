using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    //������ źȯ
    //�浹�� ���� ���� ���� ������ ������
    // ź�� ��ü�ε� �ҷ��� ������ �ο� ����
    public GameObject boom;

    float shot_angle;
    Vector2 normalized_angle;
    float speed;
    float crash_damage;
    ObjectPool ops;

    void Awake()
    {
        ops = Camera.main.GetComponent<ObjectPool>();
    }
    void OnEnable()
    {
        shot_angle = GetComponent<BulletInfo>().shot_angle;
        normalized_angle = GV.GetVector2(shot_angle).normalized;
        speed = GetComponent<BulletInfo>().speed;
        crash_damage = GetComponent<BulletInfo>().crash_damage;
        transform.rotation = Quaternion.Euler(0, 0, shot_angle - 90);

        GetComponent<Rigidbody2D>().velocity = normalized_angle * speed;

        transform.parent = null;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        switch (col.gameObject.tag)
        {
            case "end_line":
                ops.ReturnBullet(transform.parent.gameObject);
                break;
            case "enemy":
                col.gameObject.GetComponent<Mob_info>().Attacked(crash_damage);
                Instantiate(boom, transform.position, Quaternion.identity).GetComponent<DerivedBulletInfo>().SetFromPlayer(
                    shot_angle, GetComponent<BulletInfo>().critical, GetComponent<BulletInfo>().crit_damage_p
                );
                ops.ReturnBullet(transform.parent.gameObject);
                break;
        }
    }
}