using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lazer : MonoBehaviour
{
    public Vector2 shoot_angle;
    void FixedUpdate()
    {
        RaycastHit2D rayhit = Physics2D.Raycast(GetComponent<Rigidbody2D>().position, shoot_angle, 10, LayerMask.GetMask("Enemy")); 
        // 실질적인 레이, 이것이 충돌, 적과만 충돌 

        if (rayhit.collider != null)
        {
            rayhit.collider.GetComponent<Mob_info>().hp -= GetComponent<BulletInfo>().crash_damage; // 데미지
            Debug.DrawRay(GetComponent<Rigidbody2D>().position, shoot_angle * rayhit.distance, new Color(0, 1, 0)); 
            // 화면상에 레이 표시 총구 위치에서 충돌 적 디버그용

            this.transform.localScale = new Vector2(transform.localScale.x,rayhit.distance); // 레이저 이미지 표시
        }
        else
        {
            Debug.DrawRay(GetComponent<Rigidbody2D>().position, shoot_angle * 20, new Color(0, 1, 0));
            // 화면상에 레이 표시 총구 위치에서 충돌 적 디버그용

            this.transform.localScale = new Vector2(transform.localScale.x, 20); // 레이저 이미지 표시
        }
    }
}
