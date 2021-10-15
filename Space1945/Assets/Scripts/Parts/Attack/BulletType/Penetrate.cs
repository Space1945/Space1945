using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Penetrate : MonoBehaviour
{
    //관통 탄환
    //충돌해도 계속 직진
    float speed;
    int crash_damage;
    Vector3 player_pos;
    Vector3 target_pos;
    bool complete;

    void Start()
    {
        speed = GetComponent<BulletInfo>().speed;
        crash_damage = GetComponent<BulletInfo>().crash_damage;
        player_pos = Camera.main.GetComponent<Ingame_manager>().player_clone.transform.position;

        complete = false;
        if (transform.parent.GetComponent<ButtInfo>().butt_idx == 0) // 왼쪽 총구
        {
            target_pos.x = player_pos.x - 1;
            target_pos.y = player_pos.y - 1;
        }
        else // 오른쪽 총구
        {
            target_pos.x = player_pos.x + 1;
            target_pos.y = player_pos.y - 1;
        }

        StartCoroutine(MoveForward());
    }

    IEnumerator MoveForward()
    {
        yield return new WaitWhile(() => !complete);

        GetComponent<Rigidbody2D>().velocity = Vector2.up * speed;
    }

    void FixedUpdate()
    {
        transform.position = Vector2.MoveTowards(transform.position, target_pos, 0.05f);
        if (transform.position == target_pos)
            complete = true;
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
    void OnTriggerStay2D(Collider2D col)
    {
        switch (col.gameObject.tag)
        {
            case "enemy":
                col.gameObject.GetComponent<Mob_info>().Attacked(crash_damage);
                break;
        }
    }
}
