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

    Vector2 normalized_angle;
    float speed;
    int crash_damage;

    void Start()
    {
        normalized_angle = GV.GetVector2(transform.parent.GetComponent<ButtInfo>().angle).normalized;
        speed = GetComponent<BulletInfo>().speed;
        crash_damage = GetComponent<BulletInfo>().crash_damage;

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
                Instantiate(boom, gameObject.transform.position, Quaternion.identity, transform);
                Destroy(gameObject);
                break;
        }
    }
}