using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalEnemy : MonoBehaviour
{
    // �׳� �ƹ��� ȿ���� ���� źȯ
    // �浹�� ������
    float speed;
    int crash_damage;

    void Start()
    {
        speed = GetComponent<BulletInfo>().speed;
        crash_damage = GetComponent<BulletInfo>().crash_damage;

        GetComponent<Rigidbody2D>().velocity = Vector2.up * speed;
    }

    void OnTriggerEnter2D(Collider2D col) // ���� Mob info�� �ִ� �浹�� �Ѿ� ���� ���� �� bullet info�� �� �浹 ���� �浹ó���� ������
    {
        switch (col.gameObject.tag)
        {
            case "end_line":
                Destroy(gameObject);
                break;
            case "player":
                col.gameObject.GetComponent<Player>().Attacked(crash_damage);
                Destroy(gameObject);
                break;
        }
    }
}
