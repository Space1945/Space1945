using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Penetrate : MonoBehaviour
{
    //관통 탄환
    //충돌해도 계속 직진
    float shot_angle;
    Vector2 normalized_angle;
    float speed;
    float crash_damage;
    Vector3 player_pos;
    ObjectPool ops;
    GameObject bullet_key;

    void Awake()
    {
        ops = Camera.main.GetComponent<ObjectPool>();
        bullet_key = DB_Manager.Instance.using_atk.GetComponent<BulletFire>().bullet;
    }
    void OnEnable()
    {
        shot_angle = GetComponent<BulletInfo>().shot_angle;
        normalized_angle = GV.GetVector2(shot_angle).normalized;
        speed = GetComponent<BulletInfo>().speed;
        crash_damage = GetComponent<BulletInfo>().crash_damage;
        player_pos = Camera.main.GetComponent<Ingame_manager>().player_clone.transform.position;
        transform.rotation = Quaternion.Euler(0, 0, shot_angle - 90);
        transform.parent = null;

        StartCoroutine(Attack());
    }

    IEnumerator Attack()
    {
        Vector3 target_pos = gameObject.transform.position;

        target_pos.y = player_pos.y - 0.75f;

        while (transform.position != target_pos)
        {
            transform.position = Vector2.MoveTowards(transform.position, target_pos, 0.05f);

            yield return null;
        }

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
                ops.ReturnBullet(bullet_key, gameObject);
                break;
            case "enemy":
                col.gameObject.GetComponent<Mob_info>().Attacked(crash_damage);
                break;
        }
    }
}
