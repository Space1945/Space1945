using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtkScript : MonoBehaviour
{
    public int gold;
    public string explanation; // 해당 기체 설명

    public GameObject bullet;
    public float fire_rate; // 초당
    public int fire_cnt_per_shot;
    public float max_angle;
    public float min_angle;

    public float crash_damage { get; set; }
    public float speed { get; set; }

    List<Transform> butts;

    void Start()
    {
        crash_damage = bullet.GetComponent<BulletInfo>().crash_damage;
        speed = bullet.GetComponent<BulletInfo>().speed;
    }
}