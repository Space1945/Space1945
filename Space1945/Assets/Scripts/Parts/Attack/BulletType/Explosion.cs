using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    //폭발형 탄환
    //충돌시 일정 범위 내의 적에게 데미지
    // 탄두 자체로도 소량의 데미지 부여 가능
    public GameObject boom;

    float shot_angle;
    Vector2 normalized_angle;
    float speed;
    float crash_damage;
    ObjectPool ops;
    GameObject bullet_key;

    void Awake()
    {
        ops = Camera.main.GetComponent<ObjectPool>();
        bullet_key = DB_Manager.Instance.using_atk.GetComponent<BulletFire>().bullet;
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
                ops.ReturnBullet(bullet_key, gameObject);
                break;
            case "enemy":
                col.gameObject.GetComponent<Mob_info>().Attacked(crash_damage);
                Instantiate(boom, transform.position, Quaternion.identity).GetComponent<DerivedBulletInfo>().SetFromPlayer(
                    shot_angle, GetComponent<BulletInfo>().critical, GetComponent<BulletInfo>().crit_damage_p
                );
                ops.ReturnBullet(bullet_key, gameObject);
                break;
        }
    }
}