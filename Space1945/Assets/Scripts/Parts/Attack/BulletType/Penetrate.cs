using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Penetrate : MonoBehaviour
{
    //관통 탄환
    //충돌해도 계속 직진
    public Vector2 abs_dis_from_body;
    public float backward_speed;

    Vector2 normalized_angle;
    float speed;
    int crash_damage;
    Vector3 player_pos;

    void Start()
    {
        normalized_angle = GV.GetVector2(transform.parent.GetComponent<ButtInfo>().angle).normalized;
        speed = GetComponent<BulletInfo>().speed;
        crash_damage = GetComponent<BulletInfo>().crash_damage;
        player_pos = Camera.main.GetComponent<Ingame_manager>().player_clone.transform.position;

        StartCoroutine(MoveBackward());
    }

    IEnumerator MoveBackward()
    {
        Vector3 target_pos = Vector3.zero;

        if (transform.parent.GetComponent<ButtInfo>().butt_idx == 0) // 왼쪽 총구
        {
            target_pos.x = player_pos.x - abs_dis_from_body.x;
            target_pos.y = player_pos.y - abs_dis_from_body.y;
        }
        else // 오른쪽 총구
        {
            target_pos.x = player_pos.x + abs_dis_from_body.x;
            target_pos.y = player_pos.y - abs_dis_from_body.y;
        }

        while (transform.position != target_pos)
        {
            transform.position = Vector2.MoveTowards(transform.position, target_pos, backward_speed);

            yield return null;
        }

        StartCoroutine(MoveForward());
    }
    IEnumerator MoveForward()
    {
        while (true)
        {
            GetComponent<Rigidbody2D>().AddForce(normalized_angle * speed);

            yield return null;
        }
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
                break;
        }
    }
}
