using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lazer : MonoBehaviour
{
    float fire_rate;
    void FixedUpdate()
    {
        RaycastHit2D rayhit = Physics2D.Raycast(transform.parent.position, Vector2.up, 15, LayerMask.GetMask("Enemy"));
        // 실질적인 레이, 이것이 충돌, 적과만 충돌 

        if (rayhit.collider != null)
        {
            float distance = rayhit.distance;
            if (distance <= 5)
                rayhit.collider.GetComponent<Mob_info>().hp -= GetComponent<BulletInfo>().crash_damage / fire_rate; // 데미지
            else if (distance <= 10)
                rayhit.collider.GetComponent<Mob_info>().hp -= GetComponent<BulletInfo>().crash_damage / fire_rate * 0.6f;
            else if (distance <= 15)
                rayhit.collider.GetComponent<Mob_info>().hp -= GetComponent<BulletInfo>().crash_damage / fire_rate * 0.3f;
            transform.parent.GetChild(2).position = rayhit.point;
            this.transform.localScale = new Vector2(transform.localScale.x, distance * 5); // 레이저 이미지 표시
        }
        else
        {
            transform.parent.GetChild(2).position = new Vector2(1000, 1000);

            this.transform.localScale = new Vector2(transform.localScale.x, 75); // 레이저 이미지 표시
        }
    }
    public void Set(float fire_rate)
    {
        this.fire_rate = fire_rate;
    }
}
