using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Normal : MonoBehaviour
{
    // 그냥 아무런 효과가 없는 탄환
    // 충돌시 데미지
    Vector2 normalized_angle;
    float speed;
    int crash_damage;

    void Start()
    {
        normalized_angle = GV.GetVector2(transform.parent.GetComponent<ButtInfo>().angle).normalized;
        speed = GetComponent<BulletInfo>().speed;
        crash_damage = GetComponent<BulletInfo>().crash_damage;

        GetComponent<Rigidbody2D>().velocity = normalized_angle * speed;
    }

    void OnTriggerEnter2D(Collider2D col) // 기존 Mob info에 있던 충돌시 총알 삭제 관련 및 bullet info의 벽 충돌 관련 충돌처리를 가져옴
    {
        switch (col.gameObject.tag)
        {
            case "end_line":
                Destroy(gameObject);
                break;
            case "enemy":
                col.gameObject.GetComponent<Mob_info>().Attacked(crash_damage);
                Destroy(gameObject);
                break;
        }
    }
}
