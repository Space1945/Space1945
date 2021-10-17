using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    //폭발형 탄환
    //충돌시 일정 범위 내의 적에게 데미지
    //해당 탄환은 탄두로 적에게 데미지를 입힐 수 없음 Mob info의 충돌보다 아래의 충돌이 더 빠르기 때문에
    //따라서 탄두 자체의 crash_damage로는 적에게 데미지를 입힐 수 없으므로 Boom클래스 내의 폭파데미지로 적에게 데미지를 입혀야함
    public GameObject boom;

    float shot_angle;
    Vector2 normalized_angle;
    float speed;
    int crash_damage;

    void Start()
    {
        shot_angle = GetComponent<BulletInfo>().shot_angle;
        normalized_angle = GV.GetVector2(shot_angle).normalized;
        speed = GetComponent<BulletInfo>().speed;
        crash_damage = GetComponent<BulletInfo>().crash_damage;
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
                Instantiate(boom, transform.position, Quaternion.identity);
                Destroy(gameObject);
                break;
        }
    }
}