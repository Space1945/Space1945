using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalEnemy : MonoBehaviour
{
    // �׳� �ƹ��� ȿ���� ���� źȯ
    // �浹�� ������
    float speed;
    int crash_damage;
    Vector2 angle;
    float rotate;

    void Start()
    {
        speed = GetComponent<BulletInfo>().speed;
        crash_damage = GetComponent<BulletInfo>().crash_damage;

        GetComponent<Rigidbody2D>().velocity = angle * speed;
        transform.rotation = Quaternion.Euler(0, 0, rotate);
    }

    public void SetBullet(Vector2 angle, float rotate)
    {
        this.angle = angle;
        this.rotate = rotate;
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
