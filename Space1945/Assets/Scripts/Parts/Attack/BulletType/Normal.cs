using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Normal : MonoBehaviour, Bullet_Move
{
    // 그냥 아무런 효과가 없는 탄환
    // 충돌시 데미지
    float shot_angle;
    Vector2 normalized_angle;
    float crash_damage;
    float speed;

    void Start()
    {
        crash_damage = GetComponent<BulletInfo>().crash_damage;
        transform.rotation = Quaternion.Euler(0, 0, shot_angle - 90);
    }

    public void Move()
    {
        shot_angle = GetComponent<BulletInfo>().shot_angle;
        normalized_angle = GV.GetVector2(shot_angle).normalized;
        speed = GetComponent<BulletInfo>().speed;

        GetComponent<Rigidbody2D>().velocity = normalized_angle * speed;
    }

    void OnTriggerEnter2D(Collider2D col) // 기존 Mob info에 있던 충돌시 총알 삭제 관련 및 bullet info의 벽 충돌 관련 충돌처리를 가져옴
    {
        switch (col.gameObject.tag)
        {
            case "end_line":
                this.gameObject.SetActive(false);
                break;
            case "enemy":
                col.gameObject.GetComponent<Mob_info>().Attacked(crash_damage);
                this.gameObject.SetActive(false);
                break;
        }
    }
}
