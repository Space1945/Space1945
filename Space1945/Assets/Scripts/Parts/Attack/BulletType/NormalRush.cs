using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalRush : MonoBehaviour
{
    // ���� ����� ���� Ÿ������ ���� ����
    // �浹�� ������
    float speed;
    float crash_damage;
    GameObject target;

    void Start()
    {
        speed = GetComponent<BulletInfo>().speed;
        crash_damage = GetComponent<BulletInfo>().crash_damage;

        FindTarget();
        if (target != null)
        {
            transform.rotation = Quaternion.Euler(0, 0, GV.GetDegree(target.transform.position - gameObject.transform.position) - 90);
            GetComponent<Rigidbody2D>().velocity = (target.transform.position - gameObject.transform.position).normalized * speed;
        }
        else
            GetComponent<Rigidbody2D>().velocity = Vector2.up * speed;

    }

    void FindTarget()
    {
        float min = 999999999;
        Vector2 bullet_p = transform.position;
        foreach (GameObject enemy in Camera.main.GetComponent<Ingame_manager>().enemys)
            if (enemy != null)
            {
                Vector2 enemy_p = enemy.transform.position;
                float dis = Vector2.Distance(enemy_p, bullet_p);
                if (dis < min)
                {
                    target = enemy;
                    min = dis;
                }
            }
    }

    void OnTriggerEnter2D(Collider2D col) // ���� Mob info�� �ִ� �浹�� �Ѿ� ���� ���� �� bullet info�� �� �浹 ���� �浹ó���� ������
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
