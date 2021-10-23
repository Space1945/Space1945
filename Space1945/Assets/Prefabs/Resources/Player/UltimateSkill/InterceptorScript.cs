using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterceptorScript : MonoBehaviour
{
    public float crash_damage;
    public GameObject boom;

    float speed;
    float scale;
    Vector2 position;

    void Awake()
    {

    }
    public void Set(float speed, float scale, Vector2 position)
    {
        this.speed = speed;
        this.scale = scale;
        this.position = position;
    }
    public void Set(float crash_damage, float speed, float scale, Vector2 position)
    {
        this.crash_damage = crash_damage;
        this.speed = speed;
        this.scale = scale;
        this.position = position;
    }
    void Start()
    {
        transform.localScale = new Vector2(scale, scale);
        GetComponent<Rigidbody2D>().velocity = Vector2.up * speed;
        transform.position = position;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        switch (col.gameObject.tag)
        {
            case "end_line":
                Destroy(gameObject);
                break;
        }
    }
    void OnTriggerStay2D(Collider2D col)
    {
        switch (col.gameObject.tag)
        {
            case "enemy":
                col.gameObject.GetComponent<Mob_info>().Attacked(crash_damage);
                Instantiate(boom, transform.position, Quaternion.identity).GetComponent<DerivedBulletInfo>().SetFromNone(200f, 0f, 0f, 0f, false);
                Destroy(gameObject);
                break;
        }
    }
}
