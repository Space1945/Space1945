using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalEnemy : MonoBehaviour
{
    // 그냥 아무런 효과가 없는 탄환
    // 충돌시 데미지
    float speed;
    float crash_damage;

    void Start()
    {
        speed = GetComponent<BulletInfo>().speed;
        crash_damage = GetComponent<BulletInfo>().crash_damage;

        SetAngleAndFire(false);
    }

    void SetAngleAndFire(bool destroy_when_parent_destroyed)
    {
        Vector2 angle = (transform.parent.position - transform.parent.parent.position).normalized;
        GetComponent<Rigidbody2D>().velocity = angle * speed;
        transform.rotation = Quaternion.Euler(angle);

        if (!destroy_when_parent_destroyed)
            transform.parent = null;
    }

    void OnTriggerEnter2D(Collider2D col) // 기존 Mob info에 있던 충돌시 총알 삭제 관련 및 bullet info의 벽 충돌 관련 충돌처리를 가져옴
    {
        switch (col.gameObject.tag)
        {
            case "end_line":
                Destroy(gameObject);
                break;
            case "player":
                float new_crash_damage = crash_damage - col.gameObject.GetComponent<AirframeScript>().basic_def;
                col.gameObject.GetComponent<Player>().Attacked(new_crash_damage > 0 ? new_crash_damage : 0);
                Destroy(gameObject);
                break;
        }
    }
}
