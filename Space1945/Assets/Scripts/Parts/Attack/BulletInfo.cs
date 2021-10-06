using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletInfo : MonoBehaviour
{
    public int crash_damage;
    public float speed;

    Rigidbody2D rigid;
    PolygonCollider2D col;
   
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        col = GetComponent<PolygonCollider2D>();
    }

    public void SetBullet(int crash_damage, float speed)
    {
        this.crash_damage = crash_damage;
        this.speed = speed;
    }
}
