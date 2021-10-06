using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boom : MonoBehaviour
{
    public GameObject bullet; // 폭발할 총알

    float max_range; // 폭발 최대 범위
    float range; // 현재 폭발 범위
    float delta_range;
    public float boom_damage; //충돌 데미지
    public float percent; // 데미지 계수 ex) 75% 100%...
    HashSet<int> collided;

    void Start()
    {
        collided = new HashSet<int>();
        transform.position = bullet.transform.position; // 폭발 위치
        max_range = bullet.GetComponent<Explosion>().boom_range;
        boom_damage = bullet.GetComponent<Explosion>().boom_damage;
        delta_range = max_range / 10;
        range = 0;
        Invoke("Delete_boom", 0.2f); // 0.2초 후 삭제
    }
    void Delete_boom() // 생성했던 폭발을 삭제
    {
        Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        range += delta_range;
        transform.localScale = new Vector2(range, range); //폭파 범위를 설정
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
