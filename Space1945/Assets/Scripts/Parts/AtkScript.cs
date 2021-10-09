using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtkScript : MonoBehaviour
{
    public float crash_damage { get; set; }
    public float speed { get; set; }

    public float fire_rate; // √ ¥Á
    public int gold;

    public GameObject bullet;

    private void Start()
    {
        crash_damage = bullet.GetComponent<BulletInfo>().crash_damage;
        speed = bullet.GetComponent<BulletInfo>().speed;
    }
}