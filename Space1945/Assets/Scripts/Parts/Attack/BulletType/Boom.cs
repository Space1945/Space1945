using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boom : MonoBehaviour
{
    public float max_range; // 폭발 최대 범위
    public float boom_damage; //충돌 데미지

    float range = 0; // 현재 폭발 범위
    float delta_range;
    HashSet<int> collided;

    void Start()
    {
        collided = new HashSet<int>();
        delta_range = max_range / 10;
    }
    private void FixedUpdate()
    {
        range += delta_range;
        transform.localScale = new Vector2(range, range); //폭파 범위를 설정

        if (range >= max_range)
            Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collided.Contains(collision.GetInstanceID()))
        {
            if (collision.tag == "enemy")
            {
                collided.Add(collision.GetInstanceID());
                if (range <= max_range / 2)
                    collision.GetComponent<Mob_info>().hp -= boom_damage;
                else
                    collision.GetComponent<Mob_info>().hp -= boom_damage / 2;
            }
        }
    }
}
