using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    //������ źȯ
    //�浹�� ���� ���� ���� ������ ������
    //�ش� źȯ�� ź�η� ������ �������� ���� �� ���� Mob info�� �浹���� �Ʒ��� �浹�� �� ������ ������
    //���� ź�� ��ü�� crash_damage�δ� ������ �������� ���� �� �����Ƿ� BoomŬ���� ���� ���ĵ������� ������ �������� ��������
    public GameObject boom;

    float shot_angle;
    Vector2 normalized_angle;
    float speed;
    int crash_damage;

    void Start()
    {
        shot_angle = GetComponent<BulletInfo>().shot_angle;
        normalized_angle = GV.GetVector2(shot_angle).normalized;
        speed = GetComponent<BulletInfo>().speed;
        crash_damage = GetComponent<BulletInfo>().crash_damage;
        transform.rotation = Quaternion.Euler(0, 0, shot_angle - 90);

        GetComponent<Rigidbody2D>().velocity = normalized_angle * speed;
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
                Instantiate(boom, transform.position, Quaternion.identity);
                Destroy(gameObject);
                break;
        }
    }
}