using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Normal : MonoBehaviour
{
    // �׳� �ƹ��� ȿ���� ���� źȯ
    // �浹�� ������
    float shot_angle;
    Vector2 normalized_angle;
    float crash_damage;
    float speed;
    ParticleSystem par_hit;
    void Awake()
    {
        par_hit = Resources.Load<ParticleSystem>("Particle/EnemyHit");
    }
    void Start()
    {
        shot_angle = GetComponent<BulletInfo>().shot_angle;
        normalized_angle = GV.GetVector2(shot_angle).normalized;
        speed = GetComponent<BulletInfo>().speed;
        crash_damage = GetComponent<BulletInfo>().crash_damage;
        transform.rotation = Quaternion.Euler(0, 0, shot_angle - 90);


        GetComponent<Rigidbody2D>().velocity = normalized_angle * speed;
    }

    void OnTriggerEnter2D(Collider2D col) // ���� Mob info�� �ִ� �浹�� �Ѿ� ���� ���� �� bullet info�� �� �浹 ���� �浹ó���� ������
    {
        switch (col.gameObject.tag)
        {
            case "end_line":
                Destroy(gameObject);
                break;
            case "enemy":
                ParticleSystem par = Instantiate(par_hit);
                par.transform.position = transform.position; 
                col.gameObject.GetComponent<Mob_info>().Attacked(crash_damage);
                Destroy(gameObject);
                break;
        }
    }
}
