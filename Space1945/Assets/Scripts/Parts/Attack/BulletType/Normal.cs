using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Normal : MonoBehaviour
{
    // 그냥 아무런 효과가 없는 탄환
    // 충돌시 데미지

    private void FixedUpdate()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.up * GetComponent<BulletInfo>().speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision) // 기존 Mob info에 있던 충돌시 총알 삭제 관련 및 bullet info의 벽 충돌 관련 충돌처리를 가져옴
    {
        if (collision.gameObject.tag == "end_line")
            Destroy(gameObject);
    }
}
