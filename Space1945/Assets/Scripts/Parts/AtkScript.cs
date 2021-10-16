using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtkScript : MonoBehaviour
{
    public float fire_rate; // �ʴ�
    public int gold;
    public string explanation; // �ش� ��ü ����

    public GameObject bullet;
    public float[] angles;

    public float crash_damage { get; set; }
    public float speed { get; set; }

    void Start()
    {
        crash_damage = bullet.GetComponent<BulletInfo>().crash_damage;
        speed = bullet.GetComponent<BulletInfo>().speed;
    }
}