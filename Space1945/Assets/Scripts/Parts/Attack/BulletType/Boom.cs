using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boom : MonoBehaviour
{
    public float max_range; // 폭발 최대 범위
    public float boom_damage; //충돌 데미지

    float range; // 현재 폭발 범위
    float delta_range;
    HashSet<int> collided;

    void Start()
    {
        Initiate();
    }

    void Initiate()
    {
        range = 0;
        delta_range = max_range / 10;
        collided = new HashSet<int>();
    }

    void FixedUpdate()
    {
        range += delta_range;
        transform.localScale = new Vector2(range, range); //폭파 범위를 설정

        if (range >= max_range)
            Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (!collided.Contains(col.GetInstanceID()))
        {
            switch (col.gameObject.tag)
            {
                case "enemy":
                    collided.Add(col.GetInstanceID());
                    if (range <= max_range / 2)
                        col.gameObject.GetComponent<Mob_info>().Attacked(boom_damage);
                    else
                        col.gameObject.GetComponent<Mob_info>().Attacked(boom_damage / 2);
                    break;
                case "player":
                    collided.Add(col.GetInstanceID());
                    if (range <= max_range / 2)
                        col.gameObject.GetComponent<Player>().Attacked(boom_damage);
                    else
                        col.gameObject.GetComponent<Player>().Attacked(boom_damage / 2);
                    break;
            }
        }
    }
}
