using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public struct reinforce
{
    public string name;
    public float value;
}

public class ItemInfo : MonoBehaviour
{
    public float drop_rate;
    public float effect_time; // 효과 지속시간
    public float item_move_speed;
    public int bound_count;
    public reinforce[] reinforces;

    Rigidbody2D rigid;
    CircleCollider2D col;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        col = GetComponent<CircleCollider2D>();

        rigid.velocity = new Vector2(1, 1).normalized * item_move_speed;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    void OnCollisionEnter2D(Collision2D collision) // 벽충돌
    {
        GameObject col = collision.gameObject;

        if (col.tag == "game_line")
        {
            bound_count--;
            if (bound_count < 0)
                Destroy(gameObject);
                //gameObject.layer = 0;
        }
        else if (col.tag == "end_line")
            Destroy(gameObject);
        else if (col.tag == "player") // 곱연산 적용
        {
            for (int i = 0; i < reinforces.Length; i++)
                col.GetComponent<AirframeScript>().TemporaryReinforce(effect_time, reinforces[i].name, reinforces[i].value);
            Destroy(gameObject);
        }
    }
}
