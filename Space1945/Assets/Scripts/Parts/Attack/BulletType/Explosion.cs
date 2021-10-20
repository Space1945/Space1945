using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    //폭발형 탄환
    //충돌시 일정 범위 내의 적에게 데미지
    // 탄두 자체로도 소량의 데미지 부여 가능
    public GameObject boom;
    float boom_damage;
    float boom_damage_p;

    float shot_angle;
    Vector2 normalized_angle;
    float speed;
    float crash_damage;

    void Start()
    {
        shot_angle = GetComponent<BulletInfo>().shot_angle;
        normalized_angle = GV.GetVector2(shot_angle).normalized;
        speed = GetComponent<BulletInfo>().speed;
        crash_damage = GetComponent<BulletInfo>().crash_damage;
        boom_damage_p = GetComponent<BulletInfo>().add_p;
        boom_damage = boom.GetComponent<Boom>().boom_damage * boom_damage_p;
        transform.rotation = Quaternion.Euler(0, 0, shot_angle - 90);

        GetComponent<Rigidbody2D>().velocity = normalized_angle * speed;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        switch (col.gameObject.tag)
        {
            case "end_line":
                Destroy(gameObject);
                break;
            case "enemy":
                col.gameObject.GetComponent<Mob_info>().Attacked(crash_damage);
                GameObject bc = Instantiate(boom, transform.position, Quaternion.identity);
                bc.GetComponent<Boom>().boom_damage = boom_damage;
                Destroy(gameObject);
                break;
        }
    }
}