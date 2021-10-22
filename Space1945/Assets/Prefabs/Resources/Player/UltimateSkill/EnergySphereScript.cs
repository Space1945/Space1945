using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergySphereScript : MonoBehaviour
{
    public float crash_damage;
    public float speed;

    void Awake()
    {
        transform.position = new Vector2(0, -10);
        GetComponent<Rigidbody2D>().velocity = GV.GetVector2(90f).normalized * speed;
    }
    void Start()
    {

    }

    void OnTriggerStay2D(Collider2D col)
    {
        switch (col.gameObject.tag)
        {
            case "enemy":
                col.gameObject.GetComponent<Mob_info>().Attacked(crash_damage);
                break;
            case "enemy_bullet":
                Destroy(col.gameObject);
                break;
        }
    }
}
