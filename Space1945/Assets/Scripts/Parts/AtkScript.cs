using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtkScript : MonoBehaviour
{
    public int gold;
    public string explanation; // �ش� ��ü ����

    public GameObject bullet;
    public float fire_rate; // �ʴ�
    public int fire_cnt_per_shot; // �ѹ��� �߻��ϴ� źȯ��
    public float max_angle;
    public float min_angle;

    public float crash_damage { get; set; }
    public float speed { get; set; }

    void Start()
    {
        crash_damage = bullet.GetComponent<BulletInfo>().crash_damage;
        speed = bullet.GetComponent<BulletInfo>().speed;
    }
}