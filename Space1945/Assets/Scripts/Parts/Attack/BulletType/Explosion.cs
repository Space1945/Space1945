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

    float speed;
    int crash_damage;

    void Start()
    {
        speed = GetComponent<BulletInfo>().speed;
        crash_damage = GetComponent<BulletInfo>().crash_damage;

        GetComponent<Rigidbody2D>().velocity = Vector2.up * speed;
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
                Instantiate(boom, gameObject.transform.position, Quaternion.identity, gameObject.transform);
                Destroy(gameObject);
                break;
        }
    }
}